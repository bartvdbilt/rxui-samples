using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Data
{
    [Export(typeof(Interfaces.IPatient))]
    [ExportMetadata("type", "student")]
    public class Patient : Interfaces.IPatient
    {
        /// <summary>
        /// crée une liste de 5 patients
        /// </summary>
        /// <returns>la liste de patients</returns>
        public List<Dbo.Patient> CreateListPatient()
        {
            Observation obs = new Observation();
            List<Dbo.Patient> res = new List<Dbo.Patient>();

            Dbo.Patient patient1 = new Dbo.Patient()
            {
                Firstname = "laurence",
                Name = "marshall",
                Id = 1,
                Birthday = new DateTime(1986, 06, 30),
                Observations = obs.CreateListObservation()
            };


            Dbo.Patient patient2 = new Dbo.Patient()
            {
                Firstname = "laure",
                Name = "sagem",
                Id = 2,
                Birthday = new DateTime(1950, 06, 30),
                Observations = obs.CreateListObservation()
            };

            Dbo.Patient patient3 = new Dbo.Patient()
            {
                Firstname = "phillip",
                Name = "logy",
                Id = 3,
                Birthday = new DateTime(1970, 10, 25),
                Observations = obs.CreateListObservation()
            };

            Dbo.Patient patient4 = new Dbo.Patient()
            {
                Firstname = "sebastien",
                Name = "risette",
                Id = 4,
                Birthday = new DateTime(1965, 05, 01),
                Observations = obs.CreateListObservation()
            };

            Dbo.Patient patient5 = new Dbo.Patient()
            {
                Firstname = "thomas",
                Name = "gallente",
                Id = 5,
                Birthday = new DateTime(1932, 12, 25),
                Observations = obs.CreateListObservation()
            };

            res.Add(patient1);
            res.Add(patient2);
            res.Add(patient3);
            res.Add(patient4);
            res.Add(patient5);

            return res;
        }

    }
}
