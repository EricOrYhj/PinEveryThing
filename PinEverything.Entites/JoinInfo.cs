using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinEverything.Entites
{
    public class JoinInfo:EntityBase
    {
        public JoinInfo()
        { }
        #region Model
        private int _autoid;
        private Guid _publishid;
        private Guid _userid;
        private int _joinrole = 0;
        private string _lat;
        private string _lng;
        private DateTime _jointime;
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
        public Guid UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int JoinRole
        {
            set { _joinrole = value; }
            get { return _joinrole; }
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
        public DateTime JoinTime
        {
            set { _jointime = value; }
            get { return _jointime; }
        }
        public string OrginLat { get; set; }
        public string OrginLng { get; set; }

        public int Status { get; set; }
        #endregion Model
    }
}
