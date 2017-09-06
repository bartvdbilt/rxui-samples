using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Hippocrate.DataAccess
{
    public class ServiceUserManager : ServiceUser.IServiceUser
    {
        private ServiceUser.ServiceUserClient _client;
        public ServiceUserManager()
        {
            this._client = new ServiceUser.ServiceUserClient();
        }

        public ServiceUser.User[] GetListUser()
        {
            return _client.GetListUser();
        }

        public Task<ServiceUser.User[]> GetListUserAsync()
        {
            return _client.GetListUserAsync();
        }

        public ServiceUser.User GetUser(string login)
        {
            return _client.GetUser(login);
        }

        public Task<ServiceUser.User> GetUserAsync(string login)
        {
            return _client.GetUserAsync(login);
        }

        public bool AddUser(ServiceUser.User user)
        {
            return _client.AddUser(user);
        }

        public Task<bool> AddUserAsync(ServiceUser.User user)
        {
            return _client.AddUserAsync(user);
        }

        public bool DeleteUser(string login)
        {
            return _client.DeleteUser(login);
        }

        public Task<bool> DeleteUserAsync(string login)
        {
            return _client.DeleteUserAsync(login);
        }

        public bool Connect(string login, string pwd)
        {
            return _client.Connect(login, pwd);
        }

        public Task<bool> ConnectAsync(string login, string pwd)
        {
            return _client.ConnectAsync(login, pwd);
        }

        public void Disconnect(string login)
        {
            _client.Disconnect(login);
        }

        public Task DisconnectAsync(string login)
        {
            return _client.DisconnectAsync(login);
        }

        public string GetRole(string login)
        {
            return _client.GetRole(login);
        }

        public Task<string> GetRoleAsync(string login)
        {
            return _client.GetRoleAsync(login);
        }
    }
}