// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SpellForgottenMessage.xml' the '22/08/2011 17:22:57'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class SpellForgottenMessage : Message
	{
		public const uint Id = 5834;
		public override uint MessageId
		{
			get
			{
				return 5834;
			}
		}
		
		public IEnumerable<short> spellsId;
		public short boostPoint;
		
		public SpellForgottenMessage()
		{
		}
		
		public SpellForgottenMessage(IEnumerable<short> spellsId, short boostPoint)
		{
			this.spellsId = spellsId;
			this.boostPoint = boostPoint;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)spellsId.Count());
			foreach (var entry in spellsId)
			{
				writer.WriteShort(entry);
			}
			writer.WriteShort(boostPoint);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			spellsId = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(spellsId as short[])[i] = reader.ReadShort();
			}
			boostPoint = reader.ReadShort();
			if ( boostPoint < 0 )
			{
				throw new Exception("Forbidden value on boostPoint = " + boostPoint + ", it doesn't respect the following condition : boostPoint < 0");
			}
		}
	}
}
