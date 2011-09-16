
using System;
using Stump.Server.AuthServer.Database.Account;
using Stump.Server.AuthServer.Managers;
using Stump.Server.BaseServer.IPC;
using Stump.Server.BaseServer.IPC.Objects;

namespace Stump.Server.AuthServer.IPC
{
    public class IpcOperations : MarshalByRefObject, IRemoteOperationsAuth
    {
        public WorldServerManager Manager = WorldServerManager.Instance;

        #region IRemoteOperationsAuth Members

        public bool RegisterWorld(ref WorldServerData wsi, int channelPort)
        {
            return Manager.AddWorld(ref wsi, channelPort);
        }

        public void UnRegisterWorld(WorldServerData wsi)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return;

            Manager.RemoveWorld(wsi);
        }

        public void ChangeState(WorldServerData wsi, DofusProtocol.Enums.ServerStatusEnum state)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return;

            Manager.ChangeWorldState(wsi.Id, state);
        }

        public void UpdateConnectedChars(WorldServerData wsi, int value)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return;

            if (!Manager.Realmlist.ContainsKey(wsi.Id))
                return;

            var realm = Manager.Realmlist[wsi.Id];

            realm.CharsCount = value;

            if (realm.CharsCount >= realm.CharCapacity &&
                realm.Status == DofusProtocol.Enums.ServerStatusEnum.ONLINE)
            {
                Manager.ChangeWorldState(wsi.Id, DofusProtocol.Enums.ServerStatusEnum.FULL);
            }

            if (realm.CharsCount < realm.CharCapacity &&
                realm.Status == DofusProtocol.Enums.ServerStatusEnum.FULL)
            {
                Manager.ChangeWorldState(wsi.Id, DofusProtocol.Enums.ServerStatusEnum.ONLINE);
            }
        }

        public bool PingConnection(WorldServerData wsi)
        {
            // Do nothing. The framework will throw an exception on the remote end if we didn't pong.

            if (!Manager.CheckWorldAccess(wsi))
                return false;

            return Manager.DoPing(wsi.Id);
        }

        public AccountData GetAccountByTicket(WorldServerData wsi, string ticket)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return null;

            var account = AccountManager.Instance.FindRegisteredAccountByTicket(ticket);
            
            if (account == null)
                return null;

            return account.Serialize();
        }

        public AccountData GetAccountByNickname(WorldServerData wsi, string nickname)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return null;

            return Account.FindAccountByNickname(nickname.ToLower()).Serialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wsi"></param>
        /// <param name="name"></param>
        /// <param name="modifiedRecord"></param>
        /// <returns></returns>
        /// <remarks>It only considers password, secret question & answer and role</remarks>
        public bool ModifyAccountByNickname(WorldServerData wsi, string name, AccountData modifiedRecord)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return false;

            // todo
            Account account = Account.FindAccountByNickname(name.ToLower());

            if (account == null)
                return false;

            account.Password = modifiedRecord.Password;
            account.SecretQuestion = modifiedRecord.SecretQuestion;
            account.SecretAnswer = modifiedRecord.SecretAnswer;
            account.Role = modifiedRecord.Role;

            account.Update();

            return true;
        }

        public bool CreateAccount(WorldServerData wsi, AccountData accountData)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return false;

            var account = new Account
                              {
                                  Id = accountData.Id,
                                  Login = accountData.Login,
                                  Password = accountData.Password,
                                  Nickname = accountData.Nickname,
                                  Role = accountData.Role,
                                  AvailableBreeds = accountData.AvailableBreeds,
                                  Ticket = accountData.Ticket,
                                  SecretQuestion = accountData.SecretQuestion,
                                  SecretAnswer = accountData.SecretAnswer,
                                  Lang = accountData.Lang,
                                  Email = accountData.Email
                              };

            return AccountManager.Instance.CreateAccount(account);
        }

        public bool DeleteAccount(WorldServerData wsi, string accountname)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return false;

            Account account = Account.FindAccountByLogin(accountname);

            if (account == null)
                return false;

            AccountManager.Instance.DisconnectClientsUsingAccount(account);

            return AccountManager.Instance.DeleteAccount(account);
        }

        public bool AddAccountCharacter(WorldServerData wsi, uint accountId, uint characterId)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return false;

            var account = Account.FindAccountById(accountId);
            var world = Manager.GetWorldServer(wsi.Id);

            if (account == null || world == null)
                return false;

            return AccountManager.Instance.AddAccountCharacter(account, world, characterId);
        }

        public bool DeleteAccountCharacter(WorldServerData wsi, uint accountId, uint characterId)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return false;

            var account = Account.FindAccountById(accountId);
            var world = Manager.GetWorldServer(wsi.Id);

            if (account == null || world == null)
                return false;

            return AccountManager.Instance.DeleteAccountCharacter(account, world, characterId);
        }

        public bool BlamAccount(WorldServerData wsi, uint victimAccountId, uint bannerAccountId, TimeSpan duration, string reason)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return false;

            var victimAccount = Account.FindAccountById(victimAccountId);
            var bannerAccount = Account.FindAccountById(bannerAccountId);

            if (victimAccount == null || bannerAccount == null)
                return false;

            var record = new Sanction
            {
                Account = victimAccount,
                BannedBy = bannerAccount,
                BanReason = reason,
                Duration = duration
            };
            record.Create();

            // todo : check if it is necessary
            victimAccount.Sanctions.Add(record);
            victimAccount.Update();

            return true;
        }


        public bool BlamAccount(WorldServerData wsi, uint victimAccountId, TimeSpan duration, string reason)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return false;

            var victimAccount = Account.FindAccountById(victimAccountId);

            if (victimAccount == null)
                return false;

            var record = new Sanction
            {
                Account = victimAccount,
                BanReason = reason,
                Duration = duration
            };
            record.Create();

            // todo : check if it is necessary
            victimAccount.Sanctions.Add(record);
            victimAccount.Update();

            return true;
        }

        public bool BanIp(WorldServerData wsi, string ipToBan, uint bannerAccountId, TimeSpan duration, string reason)
        {
            if (!Manager.CheckWorldAccess(wsi))
                return false;

            var bannerAccount = Account.FindAccountById(bannerAccountId);

            if ( bannerAccount == null)
                return false;

            var record = new IpBan
            {
                Ip = ipToBan,
                BannedBy = bannerAccount,
                BanReason = reason,
                Duration = duration
            };
            record.Create();

            return true;
        }

        #endregion

        public override object InitializeLifetimeService()
        {
            // Mean it's illimited.
            return null;
        }
    }
}