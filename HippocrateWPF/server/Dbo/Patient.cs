using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dbo
{
    [DataContract]
    public class Patient
    {
        #region variables
        private string _name;
        private string _firstname;
        private DateTime _birthday;
        private int _id;
        private List<Observation> _observations; 
        #endregion


        #region getter / setter
        /// <summary>
        /// liste des observatiosn pour un patient
        /// </summary>
        [DataMember]
        public List<Observation> Observations
        {
            get { return _observations; }
            set { _observations = value; }
        }

        /// <summary>
        /// id du patient
        /// </summary>
        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Date de naissance
        /// </summary>
        [DataMember]
        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
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
        
        #endregion
    }
}
