using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dbo;
using System.IO;
using System.Web.Hosting;

namespace Wcf_Medical
{
    // NOTE: If you change the class name "ServiceObservation" here, you must also update the reference to "ServiceObservation" in Web.config.
    public class ServiceObservation : IServiceObservation
    {

        /// <summary>
        /// crée une liste de 5 patients
        /// </summary>
        /// <returns>la liste de patients</returns>
        private List<Patient> CreateListPatient()
        {
            DataAccess.DataObservation data = new DataAccess.DataObservation();
            List<Dbo.Patient> res = new List<Dbo.Patient>();

            Dbo.Patient patient1 = new Dbo.Patient()
            {
                Firstname = "laurence",
                Name = "marshall",
                Id = 1,
                Birthday = new DateTime(1986, 06, 30),
                Observations = data.CreateListObservation()
            };


            Dbo.Patient patient2 = new Dbo.Patient()
            {
                Firstname = "laure",
                Name = "sagem",
                Id = 2,
                Birthday = new DateTime(1950, 06, 30),
                Observations = data.CreateListObservation()
            };

            Dbo.Patient patient3 = new Dbo.Patient()
            {
                Firstname = "phillip",
                Name = "logy",
                Id = 3,
                Birthday = new DateTime(1970, 10, 25),
                Observations = data.CreateListObservation()
            };

            Dbo.Patient patient4 = new Dbo.Patient()
            {
                Firstname = "sebastien",
                Name = "risette",
                Id = 4,
                Birthday = new DateTime(1965, 05, 01),
                Observations = data.CreateListObservation()
            };

            Dbo.Patient patient5 = new Dbo.Patient()
            {
                Firstname = "thomas",
                Name = "gallente",
                Id = 5,
                Birthday = new DateTime(1932, 12, 25),
                Observations = data.CreateListObservation()
            };

            res.Add(patient1);
            res.Add(patient2);
            res.Add(patient3);
            res.Add(patient4);
            res.Add(patient5);

            return res;
        }

        #region IServiceObservation Members

        /// <summary>
        /// permet d'ajouter une observation à un patient
        /// </summary>
        /// <param name="idPatient">l'id du patient</param>
        /// <param name="obs">l'observations à ajouter</param>
        /// <returns>true si ca s'est bien passé sinon false</returns>
        public bool AddObservation(int idPatient, Dbo.Observation obs)
        {
            try
            {
                if (!DataAccess.DaSingleton.GetInstance().ListPatient.Any())
                {
                    DataAccess.DaSingleton.GetInstance().ListPatient = CreateListPatient();
                }
                Dbo.Patient patient = DataAccess.DaSingleton.GetInstance().ListPatient.Where(x => x.Id == idPatient).FirstOrDefault();
                if (patient != null)
                {
                    patient.Observations.Add(obs);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
