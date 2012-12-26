﻿using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.BaseServer.IPC;

namespace Stump.Server.AuthServer.Database
{
    public class WorldServerRelator
    {
        public static string FetchQuery = "SELECT * FROM worlds";
    }

    [TableName("worlds")]
    public partial class WorldServer
    {
        public WorldServer()
        {
            Status = ServerStatusEnum.OFFLINE;
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool RequireSubscription
        {
            get;
            set;
        }

        public int Completion
        {
            get;
            set;
        }

        public bool ServerSelectable
        {
            get;
            set;
        }

        public int CharCapacity
        {
            get;
            set;
        }

        public int? CharsCount
        {
            get;
            set;
        }

        public RoleEnum RequiredRole
        {
            get;
            set;
        }

        #region Session

        [Ignore]
        public string SessionId
        {
            get;
            set;
        }

        [Ignore]
        public RemoteEndpointMessageProperty RemoteEndpoint
        {
            get;
            set;
        }

        [Ignore]
        public IContextChannel Channel
        {
            get;
            set;
        }

        [Ignore]
        public IRemoteWorldOperations RemoteOperations
        {
            get;
            set;
        }

        public void SetSession(IContextChannel channel, string sessionId, RemoteEndpointMessageProperty remoteEndpoint)
        {
            Channel = channel;
            SessionId = sessionId;
            RemoteEndpoint = remoteEndpoint;
        }

        public void CloseSession()
        {
            if (RemoteOperations == null)
                return;

            try
            {
                if (Channel != null)
                    Channel.Close();
            }
            catch
            {
            }

            try
            {
                //if (RemoteOperations != null)
                //  RemoteOperations.Close();
            }
            catch
            {
            }

            RemoteOperations = null;
            Channel = null;
            SessionId = null;
            RemoteEndpoint = null;
        }

        #endregion

        #region Status

        public ServerStatusEnum Status
        {
            get;
            set;
        }

        [Ignore]
        public bool Connected
        {
            get { return Status == ServerStatusEnum.ONLINE; }
        }

        [Ignore]
        public DateTime LastPing
        {
            get;
            set;
        }

        public ushort Port
        {
            get;
            private set;
        }

        public string Address
        {
            get;
            private set;
        }

        public void SetOnline(string address, ushort port)
        {
            Status = ServerStatusEnum.ONLINE;
            LastPing = DateTime.Now;
            Address = address;
            Port = port;
        }

        public void SetOffline()
        {
            Status = ServerStatusEnum.OFFLINE;
            CharsCount = 0;

            CloseSession();
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, Id);
        }
    }
}