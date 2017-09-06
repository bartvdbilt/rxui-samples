using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;
using Dbo;
using System.Web;
using System.IO;
using System.ServiceModel.Activation;
using System.Web.Hosting;
using Wcf_Medical.DataAccess;

namespace Wcf_Medical
{
    // REMARQUE : si vous modifiez le nom de classe « ServiceUser » ici, vous devez également mettre à jour la référence à « ServiceUser » dans Web.config.
    public class ServiceUser : IServiceUser
    {

        private DataAccess.DataUser data = new DataUser();

        #region IServiceUser Membres
        /// <summary>
        /// retourne la liste de users
        /// </summary>
        /// <returns>une liste de users</returns>
        public List<Dbo.User> GetListUser()
        {
            if (!DaSingleton.GetInstance().ListUser.Any())                
            {

                DaSingleton.GetInstance().ListUser = data.CreateListUser();
            }
            return DaSingleton.GetInstance().ListUser;
        }

        /// <summary>
        /// récupère un user selon son login
        /// </summary>
        /// <param name="login">login du user</param>
        /// <returns>un user si il y a un problème une exception est lancée</returns>
        public Dbo.User GetUser(string login)
        {
            if (!DaSingleton.GetInstance().ListUser.Any())
            {
                DaSingleton.GetInstance().ListUser = data.CreateListUser();
            }
            try
            {
                return DaSingleton.GetInstance().ListUser.Where(x => x.Login == login).First();
            }
            catch (Exception ex)
            {                
                throw new Exception("Exception Security ;)", ex);
            }
        }

        /// <summary>
        /// ajout un user
        /// </summary>
        /// <param name="user">le user</param>
        /// <returns>true si tout s'est bien passé</returns>
        public bool AddUser(User user)
        {
            if (!DaSingleton.GetInstance().ListUser.Any())
            {
                DaSingleton.GetInstance().ListUser = data.CreateListUser();
            }
            DaSingleton.GetInstance().ListUser.Add(user);
            return true;
        }

        /// <summary>
        /// supprime un user selon son login
        /// </summary>
        /// <param name="login">login de la personne</param>
        /// <returns>true si ca s'est bien passé sinon risque d'avoir une exception</returns>
        public bool DeleteUser(string login)
        {
            if (!DaSingleton.GetInstance().ListUser.Any())
            {
                DaSingleton.GetInstance().ListUser = data.CreateListUser();
            }
            DaSingleton.GetInstance().ListUser.Remove(DaSingleton.GetInstance().ListUser.Where(x => x.Login == login).First());
            return true;
        }

        /// <summary>
        /// permet de connecter un user
        /// </summary>
        /// <param name="login">son login</param>
        /// <param name="pwd">son mot de passe</param>
        /// <returns>true si ca s'est bien passé sinon false ou une exception</returns>
        public bool Connect(string login, string pwd)
        {
            if (!DaSingleton.GetInstance().ListUser.Any())
            {
                DaSingleton.GetInstance().ListUser = data.CreateListUser();
            }
            try
            {
                Dbo.User tmpUser = DaSingleton.GetInstance().ListUser.Where(x => x.Login == login).FirstOrDefault();
                if (tmpUser == null)
                {
                    return false;
                }
                else
                {
                    tmpUser.Connected = tmpUser.Pwd == pwd;
                    return tmpUser.Pwd == pwd;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception Security ;)", ex);
            }
        }

        /// <summary>
        /// permet de déconnecter un utilisateur
        /// </summary>
        /// <param name="login">son login</param>
        public void Disconnect(string login)
        {
            if (!DaSingleton.GetInstance().ListUser.Any())
            {
                DaSingleton.GetInstance().ListUser = data.CreateListUser();
            }
            DaSingleton.GetInstance().ListUser.Where(x => x.Login == login).First().Connected = false;
        }

        /// <summary>
        /// permet d'obtenir le role d'un user selon son login
        /// </summary>
        /// <param name="login">le login de l'utilisateur</param>
        /// <returns>son role sous forme de chaine de caractére</returns>
        public string GetRole(string login)
        {
            if (!DaSingleton.GetInstance().ListUser.Any())
            {
                DaSingleton.GetInstance().ListUser = data.CreateListUser();
            }
            try
            {
                return DaSingleton.GetInstance().ListUser.Where(x => x.Login == login).First().Role;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Security ;)", ex);
            }
        }
        #endregion
    }
}
