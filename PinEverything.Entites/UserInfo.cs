using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinEverything.Entites
{
    public class UserInfo:EntityBase
    {
        public UserInfo()
        { }
        #region Model
        private int _autoid;
        private Guid _projectid;
        private Guid _userid;
        private string _username;
        private string _email;
        private string _phone;
        private string _currlat;
        private string _currlng;
        private string _avatar;
        private string _lastloginip;
        private DateTime _lastlogintime;
        private int? _logincount;
        /// <summary>
        /// 
        /// </summary>
        public int AutoId
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid ProjectId
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CurrLat
        {
            set { _currlat = value; }
            get { return _currlat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CurrLng
        {
            set { _currlng = value; }
            get { return _currlng; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Avatar
        {
            set { _avatar = value; }
            get { return _avatar; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastLoginIp
        {
            set { _lastloginip = value; }
            get { return _lastloginip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? LoginCount
        {
            set { _logincount = value; }
            get { return _logincount; }
        }
        public string MDToken { get; set; }
        #endregion Model
    }
}
