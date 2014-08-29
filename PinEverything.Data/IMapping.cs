using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace PinEverything.Data
{
    public interface IMapping
    {
        void RegTo(ConfigurationRegistrar confRegistrar);
    }
}
