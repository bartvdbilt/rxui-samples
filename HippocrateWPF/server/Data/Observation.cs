using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using System.IO;
using System.Web.Hosting;
using System.ComponentModel.Composition;

namespace Data
{
    [Export(typeof(Interfaces.IObservation))]
    [ExportMetadata("type", "student")]
    public class Observation : IObservation
    {
        #region constante
        private const string _txt = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
        private const string _shortTxt = "Lorem ipsum dolor";
        private string[] _listRadio = new string[6] { "radio1.jpg", "radio2.jpg", "radio3.jpg", "radio4.jpg", "radio5.jpg", "radio6.jpg" };
        #endregion

        /// <summary>
        /// crée un nombre d'observation aléatoire
        /// </summary>
        /// <returns>la liste des observations</returns>
        public List<Dbo.Observation> CreateListObservation()
        {
            StreamReader sr = null;
            BinaryReader read = null;

            Random rand = new Random((int)DateTime.Now.Ticks);

            int nb = rand.Next(0, 10);
            List<Dbo.Observation> res = new List<Dbo.Observation>();

            for (int i = 0; i < nb; i++)
            {
                int nbComment = rand.Next(1, 4);
                Dbo.Observation obs = new Dbo.Observation();
                for (int j = 0; j < nbComment; j++)
                {
                    obs.Comment += String.Format("{0} \n", _txt);
                }
                obs.Date = DateTime.Now.AddDays(nb).AddHours(nbComment * -1);

                obs.Prescription = new string[nbComment];
                for (int j = 0; j < nbComment; j++)
                {
                    obs.Prescription[j] = _shortTxt;
                }
                int nbPictures = rand.Next(0, 6);
                obs.Pictures = new Byte[nbPictures][];
                for (int j = 0; j < nbPictures; j++)
                {
                    int indexPicture = rand.Next(0, 5);

                    sr = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + "\\Pictures\\Radios\\" + _listRadio[indexPicture]);
                    read = new BinaryReader(sr.BaseStream);
                    obs.Pictures[j] = read.ReadBytes((int)sr.BaseStream.Length);

                }
                obs.BloodPressure = rand.Next(80, 100);
                obs.Weight = rand.Next(60, 120);

                res.Add(obs);
            }
            return res;

        }

    }
}
