using System;
using System.Runtime.Serialization;

namespace Dbo
{
    [DataContract]
    public class User
    {
        #region variables
        private string _login;
        private string _pwd;
        private string _name;
        private string _firstname;
        private Byte[] _picture;
        private string _role;
        private bool _connected;        
        #endregion

        #region getter / setter
        /// <summary>
        /// indique si l'utilisateur est connecté
        /// </summary>
        [DataMember]
        public bool Connected
        {
            get { return _connected; }
            set { _connected = value; }
        }

        /// <summary>
        /// role de l'utilisateur
        /// </summary>
        [DataMember]
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        /// <summary>
        /// photos de l'utilisateur
        /// </summary>
        [DataMember]
        public Byte[] Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        /// <summary>
        /// prénom
        /// </summary>
        [DataMember]
        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        /// <summary>
        /// nom
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// mot de passe
        /// </summary>
        [DataMember]
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        /// <summary>
        /// login
        /// </summary>
        [DataMember]
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        } 
        #endregion
    }
}
