// /*************************************************************************
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
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class IdentificationSuccessMessage : Message
	{
		public const uint protocolId = 22;
		internal Boolean _isInitialized = false;
		public String nickname = "";
		public uint accountId = 0;
		public uint communityId = 0;
		public Boolean hasRights = false;
		public String secretQuestion = "";
		public double remainingSubscriptionTime = 0;
		public Boolean wasAlreadyConnected = false;
		
		public IdentificationSuccessMessage()
		{
		}
		
		public IdentificationSuccessMessage(String arg1, uint arg2, uint arg3, Boolean arg4, String arg5, double arg6, Boolean arg7)
			: this()
		{
			initIdentificationSuccessMessage(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}
		
		public override uint getMessageId()
		{
			return 22;
		}
		
		public IdentificationSuccessMessage initIdentificationSuccessMessage(String arg1 = "", uint arg2 = 0, uint arg3 = 0, Boolean arg4 = false, String arg5 = "", double arg6 = 0, Boolean arg7 = false)
		{
			this.nickname = arg1;
			this.accountId = arg2;
			this.communityId = arg3;
			this.hasRights = arg4;
			this.secretQuestion = arg5;
			this.remainingSubscriptionTime = arg6;
			this.wasAlreadyConnected = arg7;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			this.nickname = "";
			this.accountId = 0;
			this.communityId = 0;
			this.hasRights = false;
			this.secretQuestion = "";
			this.remainingSubscriptionTime = 0;
			this.wasAlreadyConnected = false;
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
		
		public virtual void serialize(BigEndianWriter arg1)
		{
			this.serializeAs_IdentificationSuccessMessage(arg1);
		}
		
		public void serializeAs_IdentificationSuccessMessage(BigEndianWriter arg1)
		{
			var loc1 = 0;
			loc1 = BooleanByteWrapper.SetFlag(loc1, 0, this.hasRights);
			loc1 = BooleanByteWrapper.SetFlag(loc1, 1, this.wasAlreadyConnected);
			arg1.WriteByte((byte)loc1);
			arg1.WriteUTF((string)this.nickname);
			if ( this.accountId < 0 )
			{
				throw new Exception("Forbidden value (" + this.accountId + ") on element accountId.");
			}
			arg1.WriteInt((int)this.accountId);
			if ( this.communityId < 0 )
			{
				throw new Exception("Forbidden value (" + this.communityId + ") on element communityId.");
			}
			arg1.WriteByte((byte)this.communityId);
			arg1.WriteUTF((string)this.secretQuestion);
			if ( this.remainingSubscriptionTime < 0 )
			{
				throw new Exception("Forbidden value (" + this.remainingSubscriptionTime + ") on element remainingSubscriptionTime.");
			}
			arg1.WriteDouble(this.remainingSubscriptionTime);
		}
		
		public virtual void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_IdentificationSuccessMessage(arg1);
		}
		
		public void deserializeAs_IdentificationSuccessMessage(BigEndianReader arg1)
		{
			var loc1 = arg1.ReadByte();
			this.hasRights = (Boolean)BooleanByteWrapper.GetFlag(loc1, 0);
			this.wasAlreadyConnected = (Boolean)BooleanByteWrapper.GetFlag(loc1, 1);
			this.nickname = (String)arg1.ReadUTF();
			this.accountId = (uint)arg1.ReadInt();
			if ( this.accountId < 0 )
			{
				throw new Exception("Forbidden value (" + this.accountId + ") on element of IdentificationSuccessMessage.accountId.");
			}
			this.communityId = (uint)arg1.ReadByte();
			if ( this.communityId < 0 )
			{
				throw new Exception("Forbidden value (" + this.communityId + ") on element of IdentificationSuccessMessage.communityId.");
			}
			this.secretQuestion = (String)arg1.ReadUTF();
			this.remainingSubscriptionTime = (double)arg1.ReadDouble();
			if ( this.remainingSubscriptionTime < 0 )
			{
				throw new Exception("Forbidden value (" + this.remainingSubscriptionTime + ") on element of IdentificationSuccessMessage.remainingSubscriptionTime.");
			}
		}
		
	}
}
