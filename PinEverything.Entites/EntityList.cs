using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinEverything.Entites
{
    [Serializable]
    public class EntityList<T>
    {
        public List<T> Table { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总数量：分页时表示所有分页数据的总和
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数：自动根据总数和页大小计算得出
        /// </summary>
        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling((double)TotalCount / PageSize);
            }
        }

    }
}
