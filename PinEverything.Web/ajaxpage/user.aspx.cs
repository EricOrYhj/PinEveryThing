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
    public partial class Index : System.Web.UI.Page
    {
        PYTService pytService = new PYTService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["op"] != null)
            {
                string op = Request["op"].ToString();
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
                    case "GetJoinMembers":
                        GetJoinMembers();
                        break;
                }
            }
        }

        /// <summary>
        /// 更新坐标
        /// </summary>
        private void UpdateLbs()
        {
            //接受原始坐标
            double 
                lat = Convert.ToDouble(Request["lat"]),
                lng = Convert.ToDouble(Request["lng"]),
                tLat,tLng;

            //坐标转换
            Common.LBS.Wgs84ToMgs.transform(lat, lng, out tLat, out tLng);

            //更新并返回
            //pytService.UpdateUserLBS(this.User)
            
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
                string carType = Request["carType"];
                string carColor = Request["carColor"];
                string ownerPhone = Request["ownerPhone"];
                string note = Request["note"];
                int pubType = int.Parse(Request["pubType"].ToString());

                string pubTitle = startPosition + '-' + endPosition;
                string lat = string.Empty;
                string lng = string.Empty;
                int userLimCount = 4;

                UserInfo userInfo = new UserInfo();
                userInfo = Session["user"] as UserInfo;

                

                bool flag = pytService.AddPublishInfo(Guid.NewGuid(), userInfo.ProjectId, userInfo.UserId, pubType, 1,
                    pubTitle, note, lat, lng, userLimCount, startPosition, endPosition, carType, carColor);
                if (flag)
                {
                    resultObj.Add("MSG", "Y");
                    //发布动态
                    APIService APIService = new Services.APIService();
                    string access_token = userInfo.MDToken;
                    string pMsg = startPosition;
                    string title = startPosition;
                    bool postFlag = APIService.postUpdate(access_token, pMsg, title);
                }
                else
                    resultObj.Add("MSG", "N");
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

                

                Entites.EntityList<PublishInfo> publishInfoList = new EntityList<PublishInfo>();
                publishInfoList = pytService.QueryPublishInfo(pageIndex, pageSize);
                List<PublishInfo> publicInfo = new List<PublishInfo>();
                publicInfo = publishInfoList.Table;

                JavaScriptArray publicArr = new JavaScriptArray();
                foreach (PublishInfo publicItem in publicInfo)
                {
                    JavaScriptObject publicObj = new JavaScriptObject();
                    UserInfo userInfo = pytService.GetUser(Guid.Parse(publicItem.UserId.ToString()));
                    if (userInfo != null)
                    {
                        publicObj.Add("PublishId", publicItem.PublishId);
                        publicObj.Add("PubTitle", publicItem.PubTitle);
                        publicObj.Add("UserId", publicItem.UserId);
                        publicObj.Add("Avatar", userInfo.Avatar);
                        publicObj.Add("UserName", userInfo.UserName);
                        publicObj.Add("StartPosition", publicItem.StartPosition);
                        publicObj.Add("EndPosition", publicItem.EndPosition);
                        publicObj.Add("CarType", publicItem.CarType);
                        publicObj.Add("CarColor", publicItem.CarColor);
                        publicObj.Add("Lat", publicItem.Lat);
                        publicObj.Add("Lng", publicItem.Lng);
                        publicArr.Add(publicObj);
                    }
                }
                resultObj.Add("pubList", publicArr);
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

                    
                    //是否已经加入
                    JoinInfo joinInfo = pytService.GetJoinInfo(Guid.Parse(publishId), userInfo.UserId);
                    if (joinInfo == null)
                    {
                        bool flag = pytService.JoinPublishInfo(Guid.Parse(publishId), userInfo.UserId, joinRole, Lat, Lng);

                        if (flag)
                        {
                            APIService APIService = new Services.APIService();
                            PublishInfo publicInfo = pytService.GetPublicInfo(Guid.Parse(publishId));
                            string toUser = publicInfo.UserId.ToString();
                            string msg = "测试忽略";
                            int dialogType = 1;
                            //插入对话表
                            bool dialogFlag = pytService.AddDialogMsg(Guid.NewGuid(), Guid.Parse(publishId), Guid.NewGuid(), userInfo.UserId, Guid.Parse(toUser), msg, dialogType, Lat, Lng);
                            //发送私信
                            if (dialogFlag)
                                APIService.sendMsg(userInfo.MDToken, toUser, msg, "1");
                            resultObj.Add("MSG", "Y");
                        }
                        else
                            resultObj.Add("MSG", "N");
                    }else
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
                    joinMemberList = PYTService.QueryJoinMembers(Guid.Parse(publishId));
                    List<JoinInfo> joinMemberInfo = new List<JoinInfo>();
                    joinMemberInfo = joinMemberList.Table;

                    JavaScriptArray joinMemberArr = new JavaScriptArray();
                    foreach (JoinInfo joinItem in joinMemberInfo)
                    {
                        JavaScriptObject joinObj = new JavaScriptObject();

                        Guid userID = joinItem.UserId;
                        UserInfo userInfo = PYTService.GetUser(userID);
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
                    string msg = "测试忽略";
                    int dialogType = 1;
                    //插入对话表
                    bool dialogFlag = pytService.AddDialogMsg(Guid.NewGuid(), Guid.Parse(publishId), Guid.NewGuid(), userInfo.UserId, Guid.Parse(toUser), msg, dialogType, Lat, Lng);
                    //发送私信
                    if (dialogFlag && !userInfo.UserId.Equals(Guid.Parse(toUser)))
                        APIService.sendMsg(userInfo.MDToken, toUser, msg, "1");
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

                    publicObj.Add("PublishId", joinItem.PublishId);
                    publicObj.Add("PubTitle", publicInfo.PubTitle);
                    string creatTime = publicInfo.CreateTime.ToString("yyyyMMdd");
                    publicObj.Add("CreateTime", creatTime);
                    //publicObj.Add("CreateTime", publicInfo.CreateTime);

                    hisJoinArr.Add(publicObj);
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