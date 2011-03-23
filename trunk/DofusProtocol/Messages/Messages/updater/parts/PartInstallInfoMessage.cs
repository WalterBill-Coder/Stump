using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class PartInstallInfoMessage : PartInfoMessage
	{
		public const uint protocolId = 1509;
		internal Boolean _isInitialized = false;
		public uint installPercent = 0;
		
		public PartInstallInfoMessage()
		{
		}
		
		public PartInstallInfoMessage(ContentPart arg1, uint arg2)
			: this()
		{
			initPartInstallInfoMessage(arg1, arg2);
		}
		
		public override uint getMessageId()
		{
			return 1509;
		}
		
		public PartInstallInfoMessage initPartInstallInfoMessage(ContentPart arg1 = null, uint arg2 = 0)
		{
			base.initPartInfoMessage(arg1);
			this.installPercent = arg2;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			base.reset();
			this.installPercent = 0;
			this._isInitialized = false;
		}
		
		public override void pack(BigEndianWriter arg1)
		{
			this.serialize(arg1);
			WritePacket(arg1, this.getMessageId());
		}
		
		public override void unpack(BigEndianReader arg1, uint arg2)
		{
			this.deserialize(arg1);
		}
		
		public override void serialize(BigEndianWriter arg1)
		{
			this.serializeAs_PartInstallInfoMessage(arg1);
		}
		
		public void serializeAs_PartInstallInfoMessage(BigEndianWriter arg1)
		{
			base.serializeAs_PartInfoMessage(arg1);
			if ( this.installPercent < 0 )
			{
				throw new Exception("Forbidden value (" + this.installPercent + ") on element installPercent.");
			}
			arg1.WriteByte((byte)this.installPercent);
		}
		
		public override void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_PartInstallInfoMessage(arg1);
		}
		
		public void deserializeAs_PartInstallInfoMessage(BigEndianReader arg1)
		{
			base.deserialize(arg1);
			this.installPercent = (uint)arg1.ReadByte();
			if ( this.installPercent < 0 )
			{
				throw new Exception("Forbidden value (" + this.installPercent + ") on element of PartInstallInfoMessage.installPercent.");
			}
		}
		
	}
}