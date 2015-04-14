using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TianHengHuiYou.ViewModels
{
    /// <summary>
    /// 这是列出开发板Item 的viewmodel
    /// </summary>
    public class DevBoardAndCostViewModel:NotificationObject
    {
        public MODEL.DevBoard devBoard { get; set; }
        public float cost { get; set; }
    }
}
