using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TianHengHuiYou.Models
{
    /// <summary>
    /// 列出开发板的Item
    /// </summary>
   public class DevBoardItem
    {
        /// <summary>
        /// 开发板详情
        /// </summary>
        public MODEL.DevBoard devBoard { get; set; }
       /// <summary>
       /// 成本
       /// </summary>
        public double? Cost { get; set; }
    }
}
