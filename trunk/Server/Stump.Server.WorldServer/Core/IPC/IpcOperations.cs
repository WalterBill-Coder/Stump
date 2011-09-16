using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.IPC;
using Stump.Server.WorldServer.Core.Network;

namespace Stump.Server.WorldServer.Core.IPC
{
    public class IpcOperations : MarshalByRefObject, IRemoteOperationsWorld
    {
        public bool DisconnectConnectedAccount(uint accountId)
        {
            IEnumerable<WorldClient> clients = WorldServer.Instance.FindClients(client => client.Account != null && client.Account.Id == accountId);

            foreach (WorldClient client in clients)
            {
                client.Disconnect();
            }

            return clients.Count() > 0;
        }
    }
}