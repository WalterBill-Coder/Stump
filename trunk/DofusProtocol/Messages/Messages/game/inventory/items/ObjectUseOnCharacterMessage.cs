// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ObjectUseOnCharacterMessage.xml' the '22/08/2011 17:23:03'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ObjectUseOnCharacterMessage : ObjectUseMessage
	{
		public const uint Id = 3003;
		public override uint MessageId
		{
			get
			{
				return 3003;
			}
		}
		
		public int characterId;
		
		public ObjectUseOnCharacterMessage()
		{
		}
		
		public ObjectUseOnCharacterMessage(int objectUID, int characterId)
			 : base(objectUID)
		{
			this.characterId = characterId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(characterId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			characterId = reader.ReadInt();
			if ( characterId < 0 )
			{
				throw new Exception("Forbidden value on characterId = " + characterId + ", it doesn't respect the following condition : characterId < 0");
			}
		}
	}
}
