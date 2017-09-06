using Hippocrate.DataAccess;
using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

namespace Hippocrate.BusinessManagement
{
    public static class User
    {
        public static bool Connect(string login, string pass)
        {
            try
            {
                ServiceUserManager sum = new ServiceUserManager();
                return sum.Connect(login, pass);
            }
            catch (EndpointNotFoundException e)
            {
                MessageBox.Show("Le serveur ne répond pas.", "Erreur");
                throw e;
            }
        }

        public async static Task<bool> ConnectAsync(string login, string pass)
        {
            try
            {
                ServiceUserManager sum = new ServiceUserManager();
                return await sum.ConnectAsync(login, pass);
            }
            catch (EndpointNotFoundException e)
            {
                MessageBox.Show("Le serveur ne répond pas.", "Erreur");
                throw e;
            }
        }

        public static ServiceUser.User GetUser(string login)
        {
            try
            {
                ServiceUserManager sum = new ServiceUserManager();
                return sum.GetUser(login);
            }
            catch (EndpointNotFoundException e)
            {
                MessageBox.Show("Le serveur ne répond pas.", "Erreur");
                throw e;
            }
        }

        public async static Task<ServiceUser.User> GetUserAsync(string login)
        {
            try
            {
                ServiceUserManager sum = new ServiceUserManager();
                return await sum.GetUserAsync(login);
            }
            catch (EndpointNotFoundException e)
            {
                MessageBox.Show("Le serveur ne répond pas.", "Erreur");
                throw e;
            }
        }

        public static ServiceUser.User[] GetUserList()
        {
            try
            {
                ServiceUserManager sum = new ServiceUserManager();
                return sum.GetListUser();
            }
            catch (EndpointNotFoundException e)
            {
                MessageBox.Show("Le serveur ne répond pas.", "Erreur");
                throw e;
            }
        }

        public async static Task<ServiceUser.User[]> GetUserListAsync()
        {
            try
            {
                ServiceUserManager sum = new ServiceUserManager();
                return await sum.GetListUserAsync();
            }
            catch (EndpointNotFoundException e)
            {
                MessageBox.Show("Le serveur ne répond pas.", "Erreur");
                throw e;
            }
        }

        public static bool DeleteUser(string login)
        {
            try
            {
                ServiceUserManager sum = new ServiceUserManager();
                return sum.DeleteUser(login);
            }
            catch (EndpointNotFoundException e)
            {
                MessageBox.Show("Le serveur ne répond pas.", "Erreur");
                throw e;
            }
        }

        public static bool AddUser(ServiceUser.User user)
        {
            try
            {
                ServiceUserManager sum = new ServiceUserManager();
                return sum.AddUser(user);
            }
            catch (EndpointNotFoundException e)
            {
                MessageBox.Show("Le serveur ne répond pas.", "Erreur");
                throw e;
            }
        }
    }
}
