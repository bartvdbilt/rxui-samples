using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dbo;

namespace Wcf_Medical.DataAccess
{
    public class DaSingleton
    {
        private static DaSingleton instance = null;
        private static readonly object mylock = new object();

        private List<User> _listUser;
        private List<Patient> _listPatient;

        /// <summary>
        /// contient la liste des patients
        /// </summary>
        public List<Patient> ListPatient
        {
            get { return _listPatient; }
            set { _listPatient = value; }
        }

        /// <summary>
        /// contient la liste des users
        /// </summary>
        public List<User> ListUser
        {
            get { return _listUser; }
            set { _listUser = value; }
        }

        /// <summary>
        /// constructeur
        /// </summary>
        private DaSingleton()
        {
            _listUser = new List<User>();
            _listPatient = new List<Patient>();
        }

        public static DaSingleton GetInstance()
        {

            lock ((mylock))
            {
                if (instance == null)
                {
                    instance = new DaSingleton();
                }

                return instance;
            }

        }

    }
}