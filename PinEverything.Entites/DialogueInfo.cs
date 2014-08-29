using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinEverything.Entites
{
    public class DialogueInfo:EntityBase
    {
        public DialogueInfo()
        { }
        #region Model
        private int _autoid;
        private Guid _dialogueid;
        private Guid _publishid;
        private Guid _preid;
        private Guid _fromuserid;
        private Guid _touserid;
        private string _msg;
        private int? _dialoguetype;
        private string _lat;
        private string _lng;
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
        public Guid DialogueId
        {
            set { _dialogueid = value; }
            get { return _dialogueid; }
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
        /// 所属对话ID
        /// </summary>
        public Guid PreId
        {
            set { _preid = value; }
            get { return _preid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid FromUserId
        {
            set { _fromuserid = value; }
            get { return _fromuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid ToUserId
        {
            set { _touserid = value; }
            get { return _touserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Msg
        {
            set { _msg = value; }
            get { return _msg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DialogueType
        {
            set { _dialoguetype = value; }
            get { return _dialoguetype; }
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
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model
    }
}
