using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using MySpider;

namespace PinEverything.Data
{
    public class MDDbContext : DbContext
    {
        public MDDbContext()
        {
            this.Database.Connection.ConnectionString = ConfigHelper.GetConfigString("ConnectionString");
        }

        List<IMapping> entityMappinglist;
        // 添加实体映射注册
        public void AddEntityRegister(IMapping map)
        {
            if (entityMappinglist == null)
                entityMappinglist = new List<IMapping>();
            entityMappinglist.Add(map);
        }

        //public DbSet<Source> Customers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除EF映射默认给表名添加“s“或者“es”
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // 动态地加入实体
            if (entityMappinglist != null)
            {
                foreach (IMapping r in entityMappinglist)
                {
                    r.RegTo(modelBuilder.Configurations);
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
