// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ChallengeTargetsListRequestMessage.xml' the '22/08/2011 17:22:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ChallengeTargetsListRequestMessage : Message
	{
		public const uint Id = 5614;
		public override uint MessageId
		{
			get
			{
				return 5614;
			}
		}
		
		public sbyte challengeId;
		
		public ChallengeTargetsListRequestMessage()
		{
		}
		
		public ChallengeTargetsListRequestMessage(sbyte challengeId)
		{
			this.challengeId = challengeId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(challengeId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			challengeId = reader.ReadSByte();
			if ( challengeId < 0 )
			{
				throw new Exception("Forbidden value on challengeId = " + challengeId + ", it doesn't respect the following condition : challengeId < 0");
			}
		}
	}
}
