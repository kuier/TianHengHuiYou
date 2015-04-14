using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TianHengHuiYou.ViewModels
{
    public class InstoreViewModel:NotificationObject
    {
        public InstoreViewModel()
        {
            this.AddItemCommand = new DelegateCommand(AddItem);
            this.AddInstoreCommand = new DelegateCommand(AddInstore);
            this.InstoreModel = new MODEL.InStore();
        }

       
        public DelegateCommand AddItemCommand { get; set; }
        public DelegateCommand AddInstoreCommand { get; set; }
        private BLL.BLLInstore bllInstore = new BLL.BLLInstore();
        private MODEL.InStore instoreModel;

        public MODEL.InStore InstoreModel
        {
            get { return instoreModel; }
            set
            {
                instoreModel = value;
                this.RaisePropertyChanged("InstoreModel");
            }
        }

        private void AddItem()
        {
            if (InstoreModel.ISId == null)
            {
                MessageBox.Show("请先添加订单");
                return;
            }
            InstoreGoodsInfoViewModel viewmodel = new InstoreGoodsInfoViewModel();
            viewmodel.IsOrderId = InstoreModel.ISId;
            InstoreGoodsInfoWindow window = new InstoreGoodsInfoWindow();
            window.DataContext = viewmodel;
            window.ShowDialog();
        }
        private void AddInstore()
        {
            if (bllInstore.Add(InstoreModel) > 0)
            {
                MessageBox.Show("入库成功");
                return;
            }
            MessageBox.Show("入库失败");
        }
    }
}
