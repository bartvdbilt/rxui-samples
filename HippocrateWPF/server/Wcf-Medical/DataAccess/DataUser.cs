using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace Wcf_Medical.DataAccess
{
    public class DataUser : BaseData
    {       
        [ImportMany]
        public Lazy<Interfaces.IUser, IDictionary<string, object>>[] LoadedDatas { get; set; }       

        public DataUser()
        {
            Compose();
        }

        public List<Dbo.User> CreateListUser()
        {
            if (LoadedDatas != null)
            {
                foreach (var item in LoadedDatas)
                {
                    if (item.Metadata["type"].ToString() == ConfigurationManager.AppSettings["type"])
                    {
                        return item.Value.CreateListUser();
                    }
                }
            }
            return new List<Dbo.User>();
        }
    }
}