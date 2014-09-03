using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using PinEverything.Entites;
using PinEverything.Services;

namespace PinEverything.Web.ajaxpage
{
    public class WebComm
    {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public static UserInfo currUser()
        {
            UserInfo userInfo = null;
            if (HttpContext.Current.Session["user"] != null)
            {
                userInfo = HttpContext.Current.Session["user"] as UserInfo;
            }

            userInfo = new PYTService().GetUser(userInfo.UserId);

            return userInfo;
        }
    }

    public partial class Index : System.Web.UI.Page
    {
        PYTService pytService = new PYTService();
        UserInfo currUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["op"] != null)
            {
                string op = Request["op"].ToString();
                currUser = WebComm.currUser();
                switch (op)
                {
                    case "LoadUserDetail":
                        LoadUserDetail();
                        break;
                    case "AddPublic":
                        AddPublic();
                        break;
                    case "LoadPublicList":
                        LoadPublicList();
                        break;
                    case "JoinPublic":
                        JoinPublic();
                        break;
                    case "LoadDialogMsg":
                        LoadDialogMsg();
                        break;
                    case "ContactOwner":
                        ContactOwner();
                        break;
                    case "HisPubList":
                        HisPubList();
                        break;
                    case "HisJoinList":
                        HisJoinList();
                        break;
                    case "UpdateLbs":
                        UpdateLbs();
                        break;
                    case "GetJoinMembers":
                        GetJoinMembers();
                        break;
                    case "ExitJoin":
                        ExitJoin();
                        break;
                    case "CanclePublic":
                        CanclePublic();
                        break;
                }
            }
        }

        /// <summary>
        /// 更新坐标
        /// </summary>
        private void UpdateLbs()
        {
            JavaScriptObject resultObj = new JavaScriptObject();

            //接受原始坐标
            double
                lat = Convert.ToDouble(Request["lat"]),
                lng = Convert.ToDouble(Request["lng"]),
                tLat, tLng;

            Common.LBS.Wgs84ToMgs.transform(lat, lng, out tLat, out tLng);

            //更新并返回
            pytService.UpdateUserLBS(this.currUser.UserId, lat.ToString(), lng.ToString());

            //获取全部发布信息实体（如有分页获取第一页数据）
            if (!string.IsNullOrWhiteSpace(Request["showAllPub"]))
            {

            }

            //获取不是附近的发布
            if (!string.IsNullOrWhiteSpace(Request["showNotNearPub"]))
            {
                List<PublishInfo> resultList = pytService.QueryAllPubInfoForLBS(2, currUser.UserId);

                JavaScriptArray arr = new JavaScriptArray();

                foreach (var item in resultList)
                {
                    JavaScriptObject jObj = new JavaScriptObject();

                    jObj.Add("Lat", item.Lat);
                    jObj.Add("Lng", item.Lng);
                    jObj.Add("PubTitle", item.PubTitle);
                    jObj.Add("PubContent", item.PubContent);
                    jObj.Add("PublishId", item.PublishId);
                    UserInfo pubUser = pytService.GetUser(item.UserId);
                    jObj.Add("UserName", pubUser.UserName);

                    arr.Add(jObj);
                }

                resultObj.Add("notNearbyPubList", arr);
            }

            //获取附近发布数据实体（如有分页获取第一页数据）
            if (!string.IsNullOrWhiteSpace(Request["showNearbyPub"]))
            {
                List<PublishInfo> resultList = pytService.QueryAllPubInfoForLBS(1, currUser.UserId);

                JavaScriptArray arr = new JavaScriptArray();

                foreach (var item in resultList)
                {
                    JavaScriptObject jObj = new JavaScriptObject();
                    UserInfo pubUser = pytService.GetUser(item.UserId);
                    JoinInfo joinInfo = pytService.GetJoinInfo(item.PublishId, currUser.UserId);
                    var joinType = "";
                    if (joinInfo != null)
                        joinType = "2";//已加入
                    else if (currUser.UserId.Equals(item.UserId))
                        joinType = "1";//发布人
                    else
                        joinType = "3";//未加入

                    jObj.Add("Lat", item.Lat);
                    jObj.Add("Lng", item.Lng);
                    jObj.Add("PubTitle", item.PubTitle);
                    jObj.Add("PubContent", item.PubContent);
                    jObj.Add("PublishId", item.PublishId);
                    jObj.Add("UserName", pubUser.UserName);

                    jObj.Add("StartPosition", item.StartPosition);
                    jObj.Add("EndPosition", item.EndPosition);
                    jObj.Add("StartTime", item.StarTime.ToString("yyyy-MM-dd"));
                    jObj.Add("CarType", item.CarType);
                    jObj.Add("CarColor", item.CarColor);

                    jObj.Add("UserId", item.UserId);
                    jObj.Add("Avatar", pubUser.Avatar);
                    jObj.Add("JoinType", joinType);
                    jObj.Add("CreateTime", item.CreateTime.ToString("yyyy-MM-dd"));

                    arr.Add(jObj);
                }

                resultObj.Add("nearbyPubList", arr);
            }

            resultObj.Add("lat", tLat);
            resultObj.Add("lng", tLng);
            PageResponse(resultObj);
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        private void LoadUserDetail()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                UserInfo userInfo = new UserInfo();
                userInfo = Session["user"] as UserInfo;
                JavaScriptObject userObj = new JavaScriptObject();
                userObj.Add("UserId", userInfo.UserId);
                userObj.Add("Avatar", userInfo.Avatar);
                userObj.Add("UserName", userInfo.UserName);

                resultObj.Add("MSG", "Y");
                resultObj.Add("Users", userObj);
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 发布信息
        /// </summary>
        private void AddPublic()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                string startPosition = Request["startPosition"];
                string endPosition = Request["endPosition"];
                int userLimCount = int.Parse(Request["userLimCount"].ToString());
                string startTimeStr = Request["startTime"];
                string carType = Request["carType"];
                string carColor = Request["carColor"];
                string ownerPhone = Request["ownerPhone"];
                string note = Request["note"];
                int pubType = int.Parse(Request["pubType"].ToString());

                DateTime startTime = DateTime.Parse(startTimeStr);

                string pubTitle = startPosition + '-' + endPosition;

                UserInfo userModel = pytService.GetUser(currUser.UserId);

                string lat = userModel.OrginLat;
                string lng = userModel.OrginLng;

                Guid publicID = Guid.NewGuid();
                bool flag = pytService.AddPublishInfo(publicID, currUser.ProjectId, currUser.UserId, pubType, 1,
                    pubTitle, note, lat, lng, userLimCount, startPosition, endPosition, carType, carColor, startTime);
                if (flag)
                {
                    resultObj.Add("MSG", "Y");
                    //发布动态
                    APIService APIService = new Services.APIService();
                    string access_token = currUser.MDToken;
                    string pMsg = pubTitle;
                    string title = pubTitle;
                    string postID = APIService.postUpdate(access_token, pMsg, title, publicID);
                    if (!string.IsNullOrEmpty(postID))
                        pytService.UpdatePubPostID(publicID, currUser.UserId, postID);
                }
                else
                    resultObj.Add("MSG", "N");

                if (!string.IsNullOrEmpty(note))
                {
                    //插入对话表
                    bool dialogFlag = pytService.AddDialogMsg(Guid.NewGuid(), publicID, Guid.NewGuid(), currUser.UserId, currUser.UserId, note, 1, lat, lng);
                }
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 获取发布列表
        /// </summary>
        private void LoadPublicList()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                int pageIndex = int.Parse(Request["pageIndex"].ToString());
                int pageSize = int.Parse(Request["pageSize"].ToString());

                UserInfo curUserInfo = new UserInfo();
                curUserInfo = Session["user"] as UserInfo;

                Entites.EntityList<PublishInfo> publishInfoList = new EntityList<PublishInfo>();
                publishInfoList = pytService.QueryPublishInfo(pageIndex, pageSize);
                List<PublishInfo> publicInfo = new List<PublishInfo>();
                publicInfo = publishInfoList.Table;

                JavaScriptArray publicArr = new JavaScriptArray();
                string joinType = string.Empty;
                foreach (PublishInfo publicItem in publicInfo)
                {
                    JavaScriptObject publicObj = new JavaScriptObject();
                    UserInfo userInfo = pytService.GetUser(Guid.Parse(publicItem.UserId.ToString()));
                    if (userInfo != null)
                    {
                        JoinInfo joinInfo = pytService.GetJoinInfo(publicItem.PublishId, curUserInfo.UserId);
                        if (joinInfo != null)
                            joinType = "2";//已加入
                        else if (curUserInfo.UserId.Equals(publicItem.UserId))
                            joinType = "1";//发布人
                        else
                            joinType = "3";//未加入

                        publicObj.Add("PublishId", publicItem.PublishId);
                        publicObj.Add("PubTitle", publicItem.PubTitle);
                        publicObj.Add("UserId", publicItem.UserId);
                        publicObj.Add("StartTime", publicItem.StarTime.ToString("yyyy-MM-dd"));
                        publicObj.Add("Avatar", userInfo.Avatar);
                        publicObj.Add("UserName", userInfo.UserName);
                        publicObj.Add("StartPosition", publicItem.StartPosition);
                        publicObj.Add("EndPosition", publicItem.EndPosition);
                        publicObj.Add("CarType", publicItem.CarType);
                        publicObj.Add("CarColor", publicItem.CarColor);
                        publicObj.Add("Lat", publicItem.Lat);
                        publicObj.Add("Lng", publicItem.Lng);
                        publicObj.Add("JoinType", joinType);
                        publicObj.Add("CreateTime", publicItem.CreateTime.ToString("yyyy-MM-dd"));
                        publicArr.Add(publicObj);
                    }
                }
                resultObj.Add("pubList", publicArr);
                resultObj.Add("pageIndex", publishInfoList.PageIndex);
                resultObj.Add("MSG", "Y");
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 加入
        /// </summary>
        private void JoinPublic()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                string publishId = Request["publishId"];
                if (!string.IsNullOrEmpty(publishId))
                {
                    int joinRole = 1;
                    string Lat = string.Empty;
                    string Lng = string.Empty;

                    UserInfo userInfo = new UserInfo();
                    userInfo = Session["user"] as UserInfo;

                    PublishInfo publicInfo = pytService.GetPublicInfo(Guid.Parse(publishId));
                    //是否已经加入
                    JoinInfo joinInfo = pytService.GetJoinInfo(Guid.Parse(publishId), userInfo.UserId);
                    Entites.EntityList<JoinInfo> joinMembers = pytService.QueryJoinMembers(Guid.Parse(publishId));
                    int memberCount = 0;
                    if (joinMembers != null)
                    {
                        memberCount = joinMembers.TotalCount;

                    }
                    if (joinInfo == null)
                    {
                        if (memberCount < publicInfo.UserLimCount)
                        {
                            bool flag = pytService.JoinPublishInfo(Guid.Parse(publishId), userInfo.UserId, joinRole, Lat, Lng, 1);
                            if (flag)
                            {
                                APIService APIService = new Services.APIService();
                                string toUser = publicInfo.UserId.ToString();
                                string msg = userInfo.UserName + "加入了您创建的" + publicInfo.PubTitle;
                                int dialogType = 1;
                                //插入对话表
                                bool dialogFlag = pytService.AddDialogMsg(Guid.NewGuid(), Guid.Parse(publishId), Guid.NewGuid(), userInfo.UserId, Guid.Parse(toUser), msg, dialogType, Lat, Lng);
                                //发送私信
                                if (dialogFlag)
                                    APIService.sendMsg(userInfo.MDToken, toUser, msg, "1", publicInfo.PostID);

                                JavaScriptObject msgObj = new JavaScriptObject();
                                msgObj.Add("userName", userInfo.UserName);
                                msgObj.Add("userAvatar", userInfo.Avatar);
                                msgObj.Add("creatTime", DateTime.Now.ToString("yyyy-MM-dd"));
                                msgObj.Add("msg", msg);

                                resultObj.Add("MSG", "Y");
                                resultObj.Add("msgObj", msgObj);
                            }
                            else
                                resultObj.Add("MSG", "N");
                        }
                        else
                            resultObj.Add("MSG", "M");
                    }
                    else
                        resultObj.Add("MSG", "S");
                }
                else
                    resultObj.Add("MSG", "N");
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 加载对话信息
        /// </summary>
        private void LoadDialogMsg()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                string publishId = Request["publishId"];
                int pageIndex = int.Parse(Request["pageIndex"].ToString());
                int pageSize = int.Parse(Request["pageSize"].ToString());
                if (!string.IsNullOrEmpty(publishId))
                {

                    //对话信息
                    Entites.EntityList<DialogueInfo> dialogList = new EntityList<DialogueInfo>();
                    dialogList = pytService.QueryDialogInfo(Guid.Parse(publishId), pageIndex, pageSize);

                    List<DialogueInfo> dialogInfo = new List<DialogueInfo>();
                    dialogInfo = dialogList.Table;

                    JavaScriptArray dialogArr = new JavaScriptArray();
                    foreach (DialogueInfo dialogItem in dialogInfo)
                    {
                        JavaScriptObject publicObj = new JavaScriptObject();
                        UserInfo fromuserInfo = pytService.GetUser(Guid.Parse(dialogItem.FromUserId.ToString()));
                        UserInfo touserInfo = pytService.GetUser(Guid.Parse(dialogItem.ToUserId.ToString()));
                        publicObj.Add("PublishId", dialogItem.PublishId);
                        publicObj.Add("Msg", dialogItem.Msg);
                        publicObj.Add("FromUserId", dialogItem.FromUserId);
                        publicObj.Add("ToUserId", dialogItem.ToUserId);
                        string createTime=dialogItem.CreateTime.ToString("yyyy-MM-dd");
                        publicObj.Add("CreatTime", createTime);
                        if (fromuserInfo != null)
                        {
                            publicObj.Add("FromUserName", fromuserInfo.UserName);
                            publicObj.Add("FromUserAvatar", fromuserInfo.Avatar);
                        }
                        if (touserInfo != null)
                        {
                            publicObj.Add("ToUserName", touserInfo.UserName);
                            publicObj.Add("toUserAvatar", touserInfo.Avatar);
                        }
                        dialogArr.Add(publicObj);
                    }
                    resultObj.Add("dialogList", dialogArr);

                    //加入的成员信息
                    Entites.EntityList<JoinInfo> joinMemberList = new EntityList<JoinInfo>();
                    joinMemberList = pytService.QueryJoinMembers(Guid.Parse(publishId));
                    List<JoinInfo> joinMemberInfo = new List<JoinInfo>();
                    joinMemberInfo = joinMemberList.Table;

                    JavaScriptArray joinMemberArr = new JavaScriptArray();
                    foreach (JoinInfo joinItem in joinMemberInfo)
                    {
                        JavaScriptObject joinObj = new JavaScriptObject();

                        Guid userID = joinItem.UserId;
                        UserInfo userInfo = pytService.GetUser(userID);
                        if (userInfo != null)
                        {
                            joinObj.Add("PublishId", joinItem.PublishId);
                            joinObj.Add("UserId", userInfo.UserId);
                            joinObj.Add("UserName", userInfo.UserName);
                            joinObj.Add("Avatar", userInfo.Avatar);
                            joinMemberArr.Add(joinObj);
                        }
                    }
                    resultObj.Add("joinMemberList", joinMemberArr);

                    resultObj.Add("MSG", "Y");
                }
                else
                    resultObj.Add("MSG", "N");
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 联系
        /// </summary>
        private void ContactOwner()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                string publishId = Request["publishId"];
                string contacText = Request["contacText"];
                if (!string.IsNullOrEmpty(publishId))
                {
                    string Lat = string.Empty;
                    string Lng = string.Empty;

                    UserInfo userInfo = new UserInfo();
                    userInfo = Session["user"] as UserInfo;

                    APIService APIService = new Services.APIService();
                    PublishInfo publicInfo = pytService.GetPublicInfo(Guid.Parse(publishId));
                    string toUser = publicInfo.UserId.ToString();
                    string msg = contacText;
                    int dialogType = 1;
                    //插入对话表
                    bool dialogFlag = pytService.AddDialogMsg(Guid.NewGuid(), Guid.Parse(publishId), Guid.NewGuid(), userInfo.UserId, Guid.Parse(toUser), msg, dialogType, Lat, Lng);
                    //发送私信
                    if (dialogFlag && !userInfo.UserId.Equals(Guid.Parse(toUser)))
                        APIService.sendMsg(userInfo.MDToken, toUser, msg, "1", publicInfo.PostID);

                    JavaScriptObject msgObj = new JavaScriptObject();
                    msgObj.Add("userName", userInfo.UserName);
                    msgObj.Add("userAvatar", userInfo.Avatar);
                    msgObj.Add("creatTime", DateTime.Now.ToString("yyyy-MM-dd"));
                    msgObj.Add("msg", msg);

                    resultObj.Add("MSG", "Y");
                    resultObj.Add("msgObj", msgObj);
                }
                else
                    resultObj.Add("MSG", "N");
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 获取历史发布列表
        /// </summary>
        private void HisPubList()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                int pageIndex = int.Parse(Request["pageIndex"].ToString());
                int pageSize = int.Parse(Request["pageSize"].ToString());


                UserInfo curUserInfo = new UserInfo();
                curUserInfo = Session["user"] as UserInfo;

                Entites.EntityList<PublishInfo> hisPubList = new EntityList<PublishInfo>();
                hisPubList = pytService.QueryHisPub(curUserInfo.UserId, pageIndex, pageSize);
                List<PublishInfo> hisPubInfo = new List<PublishInfo>();
                hisPubInfo = hisPubList.Table;

                JavaScriptArray hisPubArr = new JavaScriptArray();
                foreach (PublishInfo hisPubItem in hisPubInfo)
                {
                    JavaScriptObject publicObj = new JavaScriptObject();

                    publicObj.Add("PublishId", hisPubItem.PublishId);
                    publicObj.Add("PubTitle", hisPubItem.PubTitle);
                    string creatTime = hisPubItem.CreateTime.ToString("yyyyMMdd");
                    publicObj.Add("CreateTime", creatTime);

                    hisPubArr.Add(publicObj);
                }
                resultObj.Add("hisPubList", hisPubArr);
                resultObj.Add("MSG", "Y");
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 获取历史加入列表
        /// </summary>
        private void HisJoinList()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                int pageIndex = int.Parse(Request["pageIndex"].ToString());
                int pageSize = int.Parse(Request["pageSize"].ToString());

                UserInfo curUserInfo = new UserInfo();
                curUserInfo = Session["user"] as UserInfo;

                Entites.EntityList<JoinInfo> hisJoinList = new EntityList<JoinInfo>();
                hisJoinList = pytService.QueryHisJoin(curUserInfo.UserId, pageIndex, pageSize);
                List<JoinInfo> hisJoinInfo = new List<JoinInfo>();
                hisJoinInfo = hisJoinList.Table;

                JavaScriptArray hisJoinArr = new JavaScriptArray();
                foreach (JoinInfo joinItem in hisJoinInfo)
                {
                    JavaScriptObject publicObj = new JavaScriptObject();

                    Guid publisID = joinItem.PublishId;
                    PublishInfo publicInfo = pytService.GetPublicInfo(publisID);

                    if (publicInfo != null)
                    {
                        publicObj.Add("PublishId", joinItem.PublishId);
                        publicObj.Add("PubTitle", publicInfo.PubTitle);
                        string creatTime = publicInfo.CreateTime.ToString("yyyyMMdd");
                        publicObj.Add("CreateTime", creatTime);
                        //publicObj.Add("CreateTime", publicInfo.CreateTime);
                        hisJoinArr.Add(publicObj);
                    }
                }
                resultObj.Add("hisJoinList", hisJoinArr);
                resultObj.Add("MSG", "Y");
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 获取详情加入成员
        /// </summary>
        private void GetJoinMembers()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                string publishId = Request["publishId"];

                PYTService PYTService = new PYTService();
                UserInfo curUserInfo = new UserInfo();
                curUserInfo = Session["user"] as UserInfo;

                Entites.EntityList<JoinInfo> joinMemberList = new EntityList<JoinInfo>();
                joinMemberList = PYTService.QueryJoinMembers(Guid.Parse(publishId));
                List<JoinInfo> joinMemberInfo = new List<JoinInfo>();
                joinMemberInfo = joinMemberList.Table;

                JavaScriptArray joinMemberArr = new JavaScriptArray();
                foreach (JoinInfo joinItem in joinMemberInfo)
                {
                    JavaScriptObject joinObj = new JavaScriptObject();

                    Guid userID = joinItem.UserId;
                    UserInfo userInfo = PYTService.GetUser(userID);

                    joinObj.Add("PublishId", joinItem.PublishId);
                    joinObj.Add("UserId", userInfo.UserId);
                    joinObj.Add("UserName", userInfo.UserName);
                    joinObj.Add("Avatar", userInfo.Avatar);
                    joinMemberArr.Add(joinObj);
                }
                resultObj.Add("joinMemberList", joinMemberArr);
                resultObj.Add("MSG", "Y");
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void ExitJoin()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                string publishId = Request["publishId"];
                if (!string.IsNullOrEmpty(publishId))
                {
                    UserInfo userInfo = new UserInfo();
                    userInfo = Session["user"] as UserInfo;
                    bool flag = pytService.UpdateJoinStatus(Guid.Parse(publishId), userInfo.UserId, 0);
                    if (flag)
                    {
                        string Lat = string.Empty;
                        string Lng = string.Empty;
                        resultObj.Add("MSG", "Y");

                        APIService APIService = new Services.APIService();
                        PublishInfo publicInfo = pytService.GetPublicInfo(Guid.Parse(publishId));
                        string toUser = publicInfo.UserId.ToString();
                        string msg = userInfo.UserName + "退出了您创建的" + publicInfo.PubTitle;
                        int dialogType = 1;
                        //插入对话表
                        bool dialogFlag = pytService.AddDialogMsg(Guid.NewGuid(), Guid.Parse(publishId), Guid.NewGuid(), userInfo.UserId, Guid.Parse(toUser), msg, dialogType, Lat, Lng);
                        //发送私信
                        if (dialogFlag)
                            APIService.sendMsg(userInfo.MDToken, toUser, msg, "1", publicInfo.PostID);
                    }
                    else
                        resultObj.Add("MSG", "N");

                }
                else
                    resultObj.Add("MSG", "N");
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 取消发布
        /// </summary>
        private void CanclePublic()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                string publishId = Request["publishId"];
                if (!string.IsNullOrEmpty(publishId))
                {
                    UserInfo userInfo = new UserInfo();
                    userInfo = Session["user"] as UserInfo;
                    bool flag = pytService.UpdatePubStatus(Guid.Parse(publishId), userInfo.UserId, 0);
                    if (flag)
                        resultObj.Add("MSG", "Y");
                    else
                        resultObj.Add("MSG", "N");
                }
                else
                    resultObj.Add("MSG", "N");
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        private void PageResponse(JavaScriptObject resultObj)
        {
            Response.Clear();
            Response.AddHeader("Content-Type", "Application/json");
            string jsonStr = JavaScriptConvert.SerializeObject(resultObj);
            Response.Write(jsonStr);
            Response.End();
        }
    }
}