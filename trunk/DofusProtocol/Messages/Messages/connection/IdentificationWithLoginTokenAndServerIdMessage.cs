// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IdentificationWithLoginTokenAndServerIdMessage.xml' the '22/08/2011 17:22:49'
using System;
using Stump.Core.IO;
using System.Collections.Generic;

namespace Stump.DofusProtocol.Messages
{
	public class IdentificationWithLoginTokenAndServerIdMessage : IdentificationWithLoginTokenMessage
	{
		public const uint Id = 6200;
		public override uint MessageId
		{
			get
			{
				return 6200;
			}
		}
		
		public short serverId;
		
		public IdentificationWithLoginTokenAndServerIdMessage()
		{
		}
		
		public IdentificationWithLoginTokenAndServerIdMessage(Types.Version version, string login, string password, IEnumerable<Types.TrustCertificate> certificate, bool autoconnect, string loginToken, short serverId)
			 : base(version, login, password, certificate, autoconnect, loginToken)
		{
			this.serverId = serverId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(serverId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			serverId = reader.ReadShort();
		}
	}
}
