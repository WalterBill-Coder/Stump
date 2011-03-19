﻿// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.D2oClasses;

namespace Stump.Server.DataProvider.Data.D2oTool
{
    public class D2OReader
    {
        private const int NullIdentifier = unchecked((int)0xAAAAAAAA);

        /// <summary>
        /// Contains all assembly where the reader search d2o class
        /// </summary>
        public static List<Assembly> ClassesContainers = new List<Assembly>
                                                             {
                                                                 typeof (Breed).Assembly
                                                             };

        private readonly string m_filePath;


        private int m_classcount;
        private Dictionary<int, D2OClassDefinition> m_classes;
        private int m_headeroffset;
        private Dictionary<int, int> m_indextable = new Dictionary<int, int>();
        private int m_indextablelen;
        private BigEndianReader m_reader;

        /// <summary>
        ///   Create and initialise a new D2o file
        /// </summary>
        /// <param name = "filePath">Path of the file</param>
        public D2OReader(string filePath)
            : this(new MemoryStream(File.ReadAllBytes(filePath)))
        {
            m_filePath = filePath;
        }

        public D2OReader(Stream stream)
        {
            Initialize(stream);
        }

        public string FilePath
        {
            get
            {
                return m_filePath;
            }
        }

        public string FileName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FilePath);
            }
        }

        public int IndexCount
        {
            get
            {
                return m_indextable.Count;
            }
        }

        public Dictionary<int, D2OClassDefinition> Classes
        {
            get
            {
                return m_classes;
            }
        }

        public Dictionary<int, int> Indexes
        {
            get
            {
                return m_indextable;
            }
        }

        private void Initialize(Stream stream)
        {
            m_reader = new BigEndianReader(stream);

            string header = m_reader.ReadUTFBytes(3);

            if (header != "D2O")
            {
                throw new Exception("Header doesn't equal the string \'D2O\' : Corrupted file");
            }

            ReadIndexTable();
            ReadClassesTable();
        }

        private void ReadIndexTable()
        {
            m_headeroffset = m_reader.ReadInt();
            m_reader.Seek(m_headeroffset, SeekOrigin.Begin); // place the reader at the beginning of the indextable
            m_indextablelen = m_reader.ReadInt();

            // init table index
            m_indextable = new Dictionary<int, int>(m_indextablelen / 8);
            for (int i = 0; i < m_indextablelen; i += 8)
            {
                m_indextable.Add(m_reader.ReadInt(), m_reader.ReadInt());
            }
        }

        private void ReadClassesTable()
        {
            m_classcount = m_reader.ReadInt();
            m_classes = new Dictionary<int, D2OClassDefinition>(m_classcount);
            for (int i = 0; i < m_classcount; i++)
            {
                int classId = m_reader.ReadInt();

                string classMembername = m_reader.ReadUTF();
                string classPackagename = m_reader.ReadUTF();

                Type classType = FindType(classMembername);

                int fieldscount = m_reader.ReadInt();
                var fields = new List<D2OFieldDefinition>(fieldscount);
                for (int l = 0; l < fieldscount; l++)
                {
                    string fieldname = m_reader.ReadUTF();
                    int fieldtype = m_reader.ReadInt();

                    var vectorTypes = new List<int>();
                    if (fieldtype == -99)
                    {
                        m_reader.ReadUTF(); // name -> useless
                        vectorTypes.Add(m_reader.ReadInt());
                    }

                    FieldInfo field = classType.GetField(fieldname);

                    fields.Add(new D2OFieldDefinition(fieldname, fieldtype, field, m_reader.BaseStream.Position, vectorTypes.ToArray()));
                }

                m_classes.Add(classId,
                              new D2OClassDefinition(classId, classMembername, classPackagename, classType, fields,
                                                     m_reader.BaseStream.Position));
            }
        }

        private static Type FindType(string className)
        {
            IEnumerable<Type> correspondantTypes = from asm in ClassesContainers
                                                   let types = asm.GetTypes()
                                                   from type in types
                                                   where type.Name.Equals(className, StringComparison.InvariantCulture)
                                                   select type;

            return correspondantTypes.Single();
        }

        private bool IsTypeDefined(Type type)
        {
            return m_classes.Count(entry => entry.Value.ClassType == type) > 0;
        }

        /// <summary>
        ///   Get all objects that corresponding to T associated to his index
        /// </summary>
        /// <typeparam name = "T">Corresponding class</typeparam>
        /// <param name = "allownulled">True to adding null instead of throwing an exception</param>
        /// <returns></returns>
        public Dictionary<int, T> ReadObjects<T>(bool allownulled = false)
        {
            if (!IsTypeDefined(typeof(T)))
                throw new Exception("The file doesn't contain this class");

            var result = new Dictionary<int, T>(m_indextable.Count);

            using (BigEndianReader reader = CloneReader())
            {
                foreach (var index in m_indextable)
                {
                    reader.Seek(index.Value, SeekOrigin.Begin);

                    int classid = reader.ReadInt();

                    if (m_classes[classid].ClassType == typeof(T) ||
                        m_classes[classid].ClassType.IsSubclassOf(typeof(T)))
                    {
                        try
                        {
                            result.Add(index.Key, (T)BuildObject(m_classes[classid], reader));
                        }
                        catch
                        {
                            if (allownulled)
                                result.Add(index.Key, default(T));
                            else
                                throw;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        ///   Get all objects associated to his index
        /// </summary>
        /// <param name = "allownulled">True to adding null instead of throwing an exception</param>
        /// <returns></returns>
        public Dictionary<int, object> ReadObjects(bool allownulled = false)
        {
            var result = new Dictionary<int, object>(m_indextable.Count);

            using (BigEndianReader reader = CloneReader())
            {
                foreach (var index in m_indextable)
                {
                    reader.Seek(index.Value, SeekOrigin.Begin);

                    try
                    {
                        result.Add(index.Key, ReadObject(index.Key, reader));
                    }
                    catch
                    {
                        if (allownulled)
                            result.Add(index.Key, null);
                        else
                            throw;
                    }
                }
            }
            return result;
        }

        /// <summary>
        ///   Get an object from his index
        /// </summary>
        /// <param name = "index"></param>
        /// <returns></returns>
        public object ReadObject(int index)
        {
            using (BigEndianReader reader = CloneReader())
            {
                return ReadObject(index, reader);
            }
        }

        private object ReadObject(int index, BigEndianReader reader)
        {
            int offset = m_indextable[index];
            reader.Seek(offset, SeekOrigin.Begin);

            int classid = reader.ReadInt();

            object result = BuildObject(m_classes[classid], reader);

            return result;
        }

        private object BuildObject(D2OClassDefinition classDefinition, BigEndianReader reader)
        {
            object obj = Activator.CreateInstance(classDefinition.ClassType);

            foreach (D2OFieldDefinition field in classDefinition.Fields.Values)
            {
                object fieldValue = ReadField(reader, field, field.TypeId);

                if (field.FieldType.IsAssignableFrom(fieldValue.GetType()))
                    field.FieldInfo.SetValue(obj, fieldValue);
                else if (fieldValue is IConvertible &&
                         field.FieldType.GetInterface("IConvertible") != null)
                    field.FieldInfo.SetValue(obj, Convert.ChangeType(fieldValue, field.FieldType));
                else
                {
                    throw new Exception(string.Format("Field '{0}.{1}' is not of type '{2}'", classDefinition.Name,
                                                      field.Name, fieldValue.GetType()));
                }

                field.FieldInfo.SetValue(obj, fieldValue);
            }

            return obj;
        }

        public object ReadObject<T>(int index)
        {
            using (BigEndianReader reader = CloneReader())
            {
                return ReadObject<T>(index, reader);
            }
        }

        private T ReadObject<T>(int index, BigEndianReader reader)
        {
            if (!IsTypeDefined(typeof(T)))
                throw new Exception("The file doesn't contain this class");

            int offset = m_indextable[index];
            reader.Seek(offset, SeekOrigin.Begin);

            int classid = reader.ReadInt();

            if (m_classes[classid].ClassType != typeof(T) && !m_classes[classid].ClassType.IsSubclassOf(typeof(T)))
                throw new Exception(string.Format("Wrong type, try to read object with {1} instead of {0}",
                                                  typeof(T).Name, m_classes[classid].ClassType.Name));

            return (T)BuildObject(m_classes[classid], reader);
        }

        public Dictionary<int, D2OClassDefinition> GetClasses()
        {
            return m_indextable.ToDictionary(index => index.Key, index => GetClass(index.Key));
        }


        /// <summary>
        /// Get the class corresponding to the object at the given index
        /// </summary>
        public D2OClassDefinition GetClass(int index)
        {
            using (BigEndianReader reader = CloneReader())
            {
                int offset = m_indextable[index];
                reader.Seek(offset, SeekOrigin.Begin);

                int classid = reader.ReadInt();

                return m_classes[classid];
            }
        }

        private object ReadField(BigEndianReader reader, D2OFieldDefinition field, int typeId, int vectorDimension = 0)
        {
            switch (typeId)
            {
                case -1:
                    return ReadFieldInt(reader);
                case -2:
                    return ReadFieldBool(reader);
                case -3:
                    return ReadFieldUTF(reader);
                case -4:
                    return ReadFieldDouble(reader);
                case -5:
                    return ReadFieldI18n(reader);
                case -6:
                    return ReadFieldUInt(reader);
                case -99:
                    return ReadFieldVector(reader, field, vectorDimension);
                default:
                    return ReadFieldObject(reader);
            }
        }


        private object ReadFieldVector(BigEndianReader reader, D2OFieldDefinition field, int vectorDimension)
        {
            int count = reader.ReadInt();
            var result = (IList)Activator.CreateInstance(field.FieldType);

            for (int i = 0; i < count; i++)
            {
                result.Add(ReadField(reader, field, field.VectorTypesId[vectorDimension], ++vectorDimension));
            }

            return result;
        }

        private object ReadFieldObject(BigEndianReader reader)
        {
            int classid = reader.ReadInt();

            if (classid == NullIdentifier)
                return null;

            if (Classes.Keys.Contains(classid))
                return BuildObject(Classes[classid], reader);

            return null;
        }

        private static int ReadFieldInt(BigEndianReader reader)
        {
            return reader.ReadInt();
        }

        private static uint ReadFieldUInt(BigEndianReader reader)
        {
            return reader.ReadUInt();
        }

        private static bool ReadFieldBool(BigEndianReader reader)
        {
            return reader.ReadByte() != 0;
        }

        private static string ReadFieldUTF(BigEndianReader reader)
        {
            return reader.ReadUTF();
        }

        private static double ReadFieldDouble(BigEndianReader reader)
        {
            return reader.ReadDouble();
        }

        private static int ReadFieldI18n(BigEndianReader reader)
        {
            return reader.ReadInt();
        }

        internal BigEndianReader CloneReader()
        {
            Stream streamClone = null;
            m_reader.BaseStream.CopyTo(streamClone);

            return new BigEndianReader(streamClone);
        }
    }
}