using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using PinEverything.Entites;

namespace PinEverything.Data.Mapping
{
    public class PublishMap:IMapping
    {
        public void RegTo(ConfigurationRegistrar confRegistrar)
        {
            var r = new EntityTypeConfiguration<PublishInfo>();
            r.ToTable("PublishInfo");
            r.HasKey(p => p.AutoId);
            confRegistrar.Add<PublishInfo>(r);
        }
    }
}
