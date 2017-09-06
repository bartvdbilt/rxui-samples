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
    [Export(typeof(Interfaces.IUser))]
    [ExportMetadata("type", "student")]
    public class User : IUser
    {
        /// <summary>
        /// crée la liste des 4 users
        /// </summary>
        /// <returns>retourne la liste de users</returns>
        public List<Dbo.User> CreateListUser()
        {
            StreamReader sr = null;
            BinaryReader read = null;

            List<Dbo.User> res = new List<Dbo.User>();


            Dbo.User user1 = new Dbo.User()
            {
                Firstname = "gregory",
                Login = "greg",
                Name = "house",
                Pwd = "greg",
                Role = "Medecin",
                Connected = false
            };
            
            sr = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + "\\Pictures\\Users\\medecin.jpg");
            read = new BinaryReader(sr.BaseStream);
            user1.Picture = read.ReadBytes((int)sr.BaseStream.Length);

            Dbo.User user2 = new Dbo.User()
            {
                Firstname = "fréderic",
                Login = "fred",
                Name = "ducelier",
                Pwd = "fred",
                Role = "Chirurgien",
                Connected = false
            };

            sr = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + "\\Pictures\\Users\\chirurgien.jpg");
            read = new BinaryReader(sr.BaseStream);
            user2.Picture = read.ReadBytes((int)sr.BaseStream.Length);

            Dbo.User user3 = new Dbo.User()
            {
                Firstname = "Laura",
                Login = "laura",
                Name = "dupont",
                Pwd = "laura",
                Role = "Infirmière",
                Connected = false
            };

            sr = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + "\\Pictures\\Users\\infirmiere.jpg");
            read = new BinaryReader(sr.BaseStream);
            user3.Picture = read.ReadBytes((int)sr.BaseStream.Length);

            Dbo.User user4 = new Dbo.User()
            {
                Firstname = "Albert",
                Login = "albert",
                Name = "Einstein",
                Pwd = "albert",
                Role = "Radiologue",
                Connected = false
            };

            sr = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + "\\Pictures\\Users\\radiologue.jpg");
            read = new BinaryReader(sr.BaseStream);
            user4.Picture = read.ReadBytes((int)sr.BaseStream.Length);

            res.Add(user1);
            res.Add(user2);
            res.Add(user3);
            res.Add(user4);

            return res;
        }

    }
}
