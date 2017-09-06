using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dbo;
using System.Drawing;
using System.Web;
using System.IO;
using System.Web.Hosting;

namespace Wcf_Medical
{
    // NOTE: If you change the class name "ServicePatient" here, you must also update the reference to "ServicePatient" in Web.config.
    public class ServicePatient : IServicePatient
    {
        private DataAccess.DataPatient data = new DataAccess.DataPatient();

     
    

        #region IServicePatient Members

        /// <summary>
        /// retourne la liste des patients connus dans le systéme
        /// </summary>
        /// <returns>la liste des patients</returns>
        public List<Dbo.Patient> GetListPatient()
        {
            if (!DataAccess.DaSingleton.GetInstance().ListPatient.Any())
            {
                DataAccess.DaSingleton.GetInstance().ListPatient = data.CreateListPatient();
            }
            return DataAccess.DaSingleton.GetInstance().ListPatient;
        }

        /// <summary>
        /// retourne un patient contenue dans le systéme
        /// </summary>
        /// <param name="id">id du patient</param>
        /// <returns>le patient s'il existe sinon null</returns>
        public Dbo.Patient GetPatient(int id)
        {
            if (!DataAccess.DaSingleton.GetInstance().ListPatient.Any())
            {
                DataAccess.DaSingleton.GetInstance().ListPatient = data.CreateListPatient();
            }
            return DataAccess.DaSingleton.GetInstance().ListPatient.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// ajout un patient à la liste
        /// </summary>
        /// <param name="patient">le patient</param>
        /// <returns>si cela c'est bien passé retourne true</returns>
        public bool AddPatient(Dbo.Patient patient)
        {
            if (!DataAccess.DaSingleton.GetInstance().ListPatient.Any())
            {
                DataAccess.DaSingleton.GetInstance().ListPatient = data.CreateListPatient();
            }
            //=> Bug il faut prendre l'ID max.
            patient.Id = DataAccess.DaSingleton.GetInstance().ListPatient.OrderBy(x=>x.Id).Last().Id + 1;
            DataAccess.DaSingleton.GetInstance().ListPatient.Add(patient);
            return true;
        }

        /// <summary>
        /// supprime un patient
        /// </summary>
        /// <param name="id">id du patient</param>
        /// <returns>true si bien supprimé sinon false</returns>
        public bool DeletePatient(int id)
        {
            if (!DataAccess.DaSingleton.GetInstance().ListPatient.Any())
            {
                DataAccess.DaSingleton.GetInstance().ListPatient = data.CreateListPatient();
            }
            try
            {
                DataAccess.DaSingleton.GetInstance().ListPatient.Remove(DataAccess.DaSingleton.GetInstance().ListPatient.Where(x => x.Id == id).First());
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        #endregion
    }
}
