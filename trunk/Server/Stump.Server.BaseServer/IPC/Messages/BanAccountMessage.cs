﻿#region License GNU GPL
// BanAccountMessage.cs
// 
// Copyright (C) 2013 - BehaviorIsManaged
// 
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation;
// either version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details. 
// You should have received a copy of the GNU General Public License along with this program; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
#endregion

using System;
using ProtoBuf;

namespace Stump.Server.BaseServer.IPC.Messages
{
    [ProtoContract]
    public class BanAccountMessage : IPCMessage
    {
        public BanAccountMessage()
        {
            
        }

        [ProtoMember(2)]
        public int? AccountId
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public string AccountName
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public DateTime? BanEndDate
        {
            get;
            set;
        }

        [ProtoMember(5)]
        public string BanReason
        {
            get;
            set;
        }

        [ProtoMember(6)]
        public int? BannerAccountId
        {
            get;
            set;
        }

        [ProtoMember(7)]
        public bool Jailed
        {
            get;
            set;
        }
    }
}