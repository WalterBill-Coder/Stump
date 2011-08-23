// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MountRenameRequestMessage.xml' the '22/08/2011 17:22:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MountRenameRequestMessage : Message
	{
		public const uint Id = 5987;
		public override uint MessageId
		{
			get
			{
				return 5987;
			}
		}
		
		public string name;
		public double mountId;
		
		public MountRenameRequestMessage()
		{
		}
		
		public MountRenameRequestMessage(string name, double mountId)
		{
			this.name = name;
			this.mountId = mountId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(name);
			writer.WriteDouble(mountId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			name = reader.ReadUTF();
			mountId = reader.ReadDouble();
		}
	}
}
