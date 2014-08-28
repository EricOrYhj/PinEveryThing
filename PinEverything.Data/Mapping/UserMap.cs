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
    public class UserMap : IMapping
    {
        public void RegTo(ConfigurationRegistrar confRegistrar)
        {
            var r = new EntityTypeConfiguration<UserInfo>();
            r.ToTable("UserInfo");
            r.HasKey(p => p.AutoId);
            confRegistrar.Add<UserInfo>(r);
        }
    }
}
