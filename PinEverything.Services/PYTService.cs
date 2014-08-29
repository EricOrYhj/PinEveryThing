using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PinEverything.Entites;

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

        public EntityList<PublishInfo> QueryPublishInfo(int pageIndex = 1,int pageSize = int.MaxValue)
        {
            EntityList<PublishInfo> result = new EntityList<PublishInfo>();

            result.Table = this.db.Set<PublishInfo>().OrderByDescending(
                    p => p.AutoId
                ).Skip(
                    (pageIndex - 1) * pageSize
                ).Take(
                    pageSize
                ).ToList();

            result.TotalCount = this.db.Set<PublishInfo>().Count();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;

            return result;
        }

        public bool AddPublishInfo(
            Guid publishId, 
            Guid projectId, 
            Guid userId, 
            int pubType,
            int status,
            string pubTitle,
            string pubContent,
            string lat,
            string lng,
            int userLimCount
            ) 
        {
            PublishInfo model = new PublishInfo() {
                PublishId = publishId,
                ProjectId = projectId,
                UserId = userId,
                PubType = pubType,
                Status = status,
                PubTitle = pubTitle,
                PubContent = pubContent,
                Lat = lat,
                Lng = lng,
                UserLimCount = userLimCount,
                CreateTime = DateTime.Now
            };

            this.db.Set<PublishInfo>().Add(model);

            return this.db.SaveChanges().Equals(1);
        }

    }
}
