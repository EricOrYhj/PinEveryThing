using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinEverything.Entites
{
    public class PublishInfo:EntityBase
    {
        public PublishInfo()
        { }
        #region Model
        private int _autoid;
        private Guid _publishid;
        private Guid _projectid;
        private Guid _userid;
        private int? _pubtype;
        private int? _status;
        private string _pubtitle;
        private string _pubcontent;
        private string _lat;
        private string _lng;
        private int? _userlimcount;
        private DateTime _createtime;
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
        public Guid PublishId
        {
            set { _publishid = value; }
            get { return _publishid; }
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
        public int? PubType
        {
            set { _pubtype = value; }
            get { return _pubtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PubTitle
        {
            set { _pubtitle = value; }
            get { return _pubtitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PubContent
        {
            set { _pubcontent = value; }
            get { return _pubcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lng
        {
            set { _lng = value; }
            get { return _lng; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserLimCount
        {
            set { _userlimcount = value; }
            get { return _userlimcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model
    }
}
