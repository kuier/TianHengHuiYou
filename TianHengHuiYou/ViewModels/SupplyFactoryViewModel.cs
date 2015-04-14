using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace TianHengHuiYou.ViewModels
{
    public class SupplyFactoryViewModel:NotificationObject
    {
        public SupplyFactoryViewModel()
        {
            //this.EditModel = new MODEL.SupplyFactory() { SFAddress = "山东大学", SFBank = "中国银行" };
            this.AddSupplyFactoryCommand = new DelegateCommand(AddSupplyFactory);
        }
        public DelegateCommand AddSupplyFactoryCommand { get; set; }
        private BLL.BLLSupplyFactory bllSupplyFactory = new BLL.BLLSupplyFactory();
        private MODEL.SupplyFactory editModel;

        public MODEL.SupplyFactory EditModel
        {
            get
            {
                return editModel;
            }
            set
            {
                editModel = value;
                this.RaisePropertyChanged("editModel");
            }
        }
        private void AddSupplyFactory()
        {
            if (bllSupplyFactory.Add(EditModel)>0)
            {
                MessageBox.Show("添加成功");
                return;
            }
            MessageBox.Show("添加失败");
        }
    }
}
