using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace PinEverything.Services
{
    public class PYTService : IDisposable
    {

        private Data.MDDbContext db;

        public PYTService()
        {
            db = new Data.MDDbContext();

            //注册该类下用到的数据库映射
            db.AddEntityRegister(new Data.Mapping.DialogueMap());
            db.AddEntityRegister(new Data.Mapping.JoinMap());
            db.AddEntityRegister(new Data.Mapping.NoticeMap());
            db.AddEntityRegister(new Data.Mapping.PublishMap());
            db.AddEntityRegister(new Data.Mapping.UserMap());

        }
        ~PYTService()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }



    }
}
