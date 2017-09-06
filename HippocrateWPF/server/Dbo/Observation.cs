using System;
using System.Runtime.Serialization;

namespace Dbo
{
    [DataContract]
    public class Observation
    {
        #region variables
        private DateTime _date;
        private string _comment;
        private string[] _prescription;
        private Byte[][] _pictures;
        private int _weight;

        private int _bloodPressure;

        
        #endregion

        #region getter / setter
        /// <summary>
        /// poids du patient
        /// </summary>
        [DataMember]
        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        /// <summary>
        /// pression artérielle du patient
        /// </summary>
        [DataMember]
        public int BloodPressure
        {
            get { return _bloodPressure; }
            set { _bloodPressure = value; }
        }

        /// <summary>
        /// images associées à l'observations
        /// </summary>
        [DataMember]
        public Byte[][] Pictures
        {
            get { return _pictures; }
            set { _pictures = value; }
        }

        /// <summary>
        /// liste des prescriptions pour l'observation
        /// </summary>
        [DataMember]
        public string[] Prescription
        {
            get { return _prescription; }
            set { _prescription = value; }
        }

        /// <summary>
        /// commentaire
        /// </summary>
        [DataMember]
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        /// <summary>
        /// date de l'observation
        /// </summary>
        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        } 
        #endregion
    }
}
