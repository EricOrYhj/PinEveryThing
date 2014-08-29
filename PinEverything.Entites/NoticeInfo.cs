using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinEverything.Entites
{
    public class NoticeInfo:EntityBase
    {
        public NoticeInfo()
        { }
        #region Model
        private int _autoid;
        private Guid _userid;
        private string _msg;
        private DateTime _sendtime;
        private int? _noticetype;
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
        public Guid UserId
        {
            set { _userid = value; }
            get { return _userid; }
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
        public DateTime SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NoticeType
        {
            set { _noticetype = value; }
            get { return _noticetype; }
        }
        #endregion Model
    }
}
