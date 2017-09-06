using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Web.Hosting;

namespace Wcf_Medical.DataAccess
{
    public class BaseData
    {
        private DirectoryCatalog _directoryCatalog;
     
        protected void Compose()
        {            
            string currentDirectory = new FileInfo( HostingEnvironment.ApplicationPhysicalPath + "\\bin\\").Directory.ToString();
            _directoryCatalog = new DirectoryCatalog(currentDirectory);
            AggregateCatalog catalog = new AggregateCatalog(
                new AssemblyCatalog(Assembly.GetExecutingAssembly()), _directoryCatalog);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}