using Newtonsoft.Json;
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

        public EntityList<PublishInfo> QueryPublishInfo(int pageIndex = 1, int pageSize = int.MaxValue)
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
            int userLimCount,
            string startPosition,
            string endPosition,
            string carType,
            string carColor
            )
        {
            PublishInfo model = new PublishInfo()
            {
                PublishId = publishId,
                ProjectId = projectId,
                UserId = userId,
                PubType = pubType,
                Status = status,
                PubTitle = pubTitle,
                PubContent = pubContent,
                OrginLat = lat,
                OrginLng = lng,
                UserLimCount = userLimCount,
                CreateTime = DateTime.Now,
                StartPosition = startPosition,
                EndPosition = endPosition,
                CarType = carType,
                CarColor = carColor
            };

            if (!string.IsNullOrWhiteSpace(lat) && !string.IsNullOrWhiteSpace(lng))
            {
                double tLat, tLng;
                Common.LBS.Wgs84ToMgs.transform(
                        double.Parse(lat),
                        double.Parse(lng),
                        out tLat,
                        out tLng
                    );
                model.Lat = tLng.ToString();
                model.Lng = tLat.ToString();
            }

            this.db.Set<PublishInfo>().Add(model);

            return this.db.SaveChanges().Equals(1);
        }

        public PublishInfo GetPublicInfo(Guid publishId)
        {
            return this.db.Set<PublishInfo>().FirstOrDefault(p => p.PublishId.Equals(publishId));
        }

        public bool AddUser(
            Guid projectId,
            Guid userId,
            string mdToken,
            string userName,
            string email,
            string phone,
            string currLat,
            string currLng,
            string avatar
            )
        {
            UserInfo model = new UserInfo()
            {
                ProjectId = projectId,
                UserId = userId,
                MDToken = mdToken,
                UserName = userName,
                Email = email,
                Phone = phone,
                CurrLat = currLat,
                CurrLng = currLng,
                OrginLat = currLat,
                OrginLng = currLng,
                Avatar = avatar,
                LastLoginIp = MySpider.MySpider.GetClientIP(),
                LastLoginTime = DateTime.Now,
                LoginCount = 1
            };
            if (!string.IsNullOrWhiteSpace(currLat) && !string.IsNullOrWhiteSpace(currLng))
            {
                double tLat, tLng;
                Common.LBS.Wgs84ToMgs.transform(
                        double.Parse(currLat),
                        double.Parse(currLng),
                        out tLat,
                        out tLng
                    );
                model.CurrLng = tLng.ToString();
                model.CurrLat = tLat.ToString();
            }
            

            this.db.Set<UserInfo>().Add(model);
            return this.db.SaveChanges().Equals(1);
        }

        public bool UpdateUserLBS(
                Guid userId,
                string currLat,
                string currLng
            )
        {
            UserInfo model = this.db.Set<UserInfo>().FirstOrDefault(p => p.UserId.Equals(userId));

            model.OrginLat = currLat;
            model.OrginLng = currLng;

            if (!string.IsNullOrWhiteSpace(currLat) && !string.IsNullOrWhiteSpace(currLng))
            {
                double tLat, tLng;
                Common.LBS.Wgs84ToMgs.transform(
                        double.Parse(currLat),
                        double.Parse(currLng),
                        out tLat,
                        out tLng
                    );
                model.CurrLng = tLng.ToString();
                model.CurrLat = tLat.ToString();
            }

            return this.db.SaveChanges().Equals(1);
        }

        public bool UpdateUserMDToken(
                Guid userId,
                string mdToken
            )
        {
            UserInfo model = this.db.Set<UserInfo>().FirstOrDefault(p => p.UserId.Equals(userId));
            model.MDToken = mdToken;

            return this.db.SaveChanges().Equals(1);
        }

        public UserInfo GetUser(Guid userId)
        {
            return this.db.Set<UserInfo>().FirstOrDefault(p => p.UserId.Equals(userId));
        }

        public bool JoinPublishInfo(
                Guid publishId,
                Guid userId,
                int joinRole,
                string lat,
                string lng
            )
        {
            JoinInfo model = new JoinInfo()
            {
                PublishId = publishId,
                UserId = userId,
                JoinRole = joinRole,
                OrginLat = lng,
                OrginLng = lng,
                JoinTime = DateTime.Now
            };

            if (!string.IsNullOrWhiteSpace(lat) && !string.IsNullOrWhiteSpace(lng))
            {
                double tLat, tLng;
                Common.LBS.Wgs84ToMgs.transform(
                        double.Parse(lat),
                        double.Parse(lng),
                        out tLat,
                        out tLng
                    );
                model.Lat = tLng.ToString();
                model.Lng = tLat.ToString();
            }

            this.db.Set<JoinInfo>().Add(model);
            return this.db.SaveChanges().Equals(1);
        }

        public bool AddDialogMsg(
               Guid dialogueId,
               Guid publishId,
               Guid preId,
               Guid fromUserId,
               Guid toUserId,
               string msg,
               int dialogueType,
               string lat,
               string lng

           )
        {
            DialogueInfo model = new DialogueInfo()
            {
                DialogueId = dialogueId,
                PublishId = publishId,
                PreId = preId,
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Msg = msg,
                DialogueType = dialogueType,
                OrginLat = lat,
                OrginLng = lng,
                CreateTime = DateTime.Now
            };

            if (!string.IsNullOrWhiteSpace(lat) && !string.IsNullOrWhiteSpace(lng))
            {
                double tLat, tLng;
                Common.LBS.Wgs84ToMgs.transform(
                        double.Parse(lat),
                        double.Parse(lng),
                        out tLat,
                        out tLng
                    );
                model.Lat = tLng.ToString();
                model.Lng = tLat.ToString();
            }

            this.db.Set<DialogueInfo>().Add(model);
            return this.db.SaveChanges().Equals(1);
        }


        public EntityList<DialogueInfo> QueryDialogInfo(Guid publishId,int pageIndex = 1, int pageSize = int.MaxValue)
        {
            EntityList<DialogueInfo> result = new EntityList<DialogueInfo>();

            result.Table = this.db.Set<DialogueInfo>().Where(p => p.PublishId.Equals(publishId)).OrderByDescending(
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

        public EntityList<PublishInfo> QueryHisPub(Guid userId,int pageIndex = 1, int pageSize = int.MaxValue)
        {
            EntityList<PublishInfo> result = new EntityList<PublishInfo>();

            result.Table = this.db.Set<PublishInfo>().Where(p => p.UserId.Equals(userId)).OrderByDescending(
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

        public EntityList<JoinInfo> QueryHisJoin(Guid userId, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            EntityList<JoinInfo> result = new EntityList<JoinInfo>();

            result.Table = this.db.Set<JoinInfo>().Where(p => p.UserId.Equals(userId)).OrderByDescending(
                    p => p.AutoId
                ).Skip(
                    (pageIndex - 1) * pageSize
                ).Take(
                    pageSize
                ).ToList();

            result.TotalCount = this.db.Set<JoinInfo>().Count();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;

            return result;
        }

        public JoinInfo GetJoinInfo(Guid publishId, Guid userId)
        {
            return this.db.Set<JoinInfo>().Where(p => p.PublishId.Equals(publishId)).FirstOrDefault(p => p.UserId.Equals(userId));
        }

        public EntityList<JoinInfo> QueryJoinMembers(Guid publishId)
        {
            EntityList<JoinInfo> result = new EntityList<JoinInfo>();

            result.Table = this.db.Set<JoinInfo>().Where(p => p.PublishId.Equals(publishId)).OrderByDescending(
                    p => p.AutoId
                ).ToList();

            result.TotalCount = this.db.Set<JoinInfo>().Count();
            return result;
        }
    }
}