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
	
	public class IdentificationWithLoginTokenAndServerIdMessage : IdentificationWithLoginTokenMessage
	{
		public const uint protocolId = 6200;
		internal Boolean _isInitialized = false;
		public int serverId = 0;
		
		public IdentificationWithLoginTokenAndServerIdMessage()
		{
		}
		
		public IdentificationWithLoginTokenAndServerIdMessage(Stump.DofusProtocol.Classes.Version arg1, String arg2, String arg3, Boolean arg4, String arg5, int arg6)
			: this()
		{
			initIdentificationWithLoginTokenAndServerIdMessage(arg1, arg2, arg3, arg4, arg5, arg6);
		}
		
		public override uint getMessageId()
		{
			return 6200;
		}
		
		public IdentificationWithLoginTokenAndServerIdMessage initIdentificationWithLoginTokenAndServerIdMessage(Stump.DofusProtocol.Classes.Version arg1 = null, String arg2 = "", String arg3 = "", Boolean arg4 = false, String arg5 = "", int arg6 = 0)
		{
			base.initIdentificationWithLoginTokenMessage(arg1, arg2, arg3, arg4, arg5);
			this.serverId = arg6;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			base.reset();
			this.serverId = 0;
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
			this.serializeAs_IdentificationWithLoginTokenAndServerIdMessage(arg1);
		}
		
		public void serializeAs_IdentificationWithLoginTokenAndServerIdMessage(BigEndianWriter arg1)
		{
			base.serializeAs_IdentificationWithLoginTokenMessage(arg1);
			arg1.WriteShort((short)this.serverId);
		}
		
		public override void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_IdentificationWithLoginTokenAndServerIdMessage(arg1);
		}
		
		public void deserializeAs_IdentificationWithLoginTokenAndServerIdMessage(BigEndianReader arg1)
		{
			base.deserialize(arg1);
			this.serverId = (int)arg1.ReadShort();
		}
		
	}
}
