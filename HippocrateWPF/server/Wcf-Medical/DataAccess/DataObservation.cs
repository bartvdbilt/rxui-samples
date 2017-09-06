using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using System.Configuration;

namespace Wcf_Medical.DataAccess
{
    public class DataObservation : BaseData
    {
        [ImportMany]
        public Lazy<Interfaces.IObservation, IDictionary<string, object>>[] LoadedDatas { get; set; }    
        
        public DataObservation()
        {
            Compose();
        }

        public List<Dbo.Observation> CreateListObservation()
        {
            if (LoadedDatas != null)
            {
                foreach (var item in LoadedDatas)
                {
                    if (item.Metadata["type"].ToString() == ConfigurationManager.AppSettings["type"])
                    {
                        return item.Value.CreateListObservation();
                    }
                }
            }
            return new List<Dbo.Observation>();
        }
    }
}