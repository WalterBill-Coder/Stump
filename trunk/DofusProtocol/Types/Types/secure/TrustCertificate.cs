// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TrustCertificate.xml' the '22/08/2011 17:23:07'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class TrustCertificate
	{
		public const uint Id = 377;
		public virtual short TypeId
		{
			get
			{
				return 377;
			}
		}
		
		public int id;
		public string hash;
		
		public TrustCertificate()
		{
		}
		
		public TrustCertificate(int id, string hash)
		{
			this.id = id;
			this.hash = hash;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(id);
			writer.WriteUTF(hash);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			id = reader.ReadInt();
			if ( id < 0 )
			{
				throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
			}
			hash = reader.ReadUTF();
		}
	}
}
