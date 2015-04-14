using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows;
namespace TianHengHuiYou.ViewModels
{
    public class GoodsInfoDetailViewModel:NotificationObject
    {
        public GoodsInfoDetailViewModel()
        {
            this.SaveCommand = new DelegateCommand(Save);
        }
        //private BLL.BLLGoodsInfo bllGoodsInfo = new BLL.BLLGoodsInfo();
        public BLL.BLLGoodsInfo bllGoodsInfo { get; set; }
        public string cmd { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        private MODEL.GoodsInfo editModel;

        public MODEL.GoodsInfo EditModel
        {
            get
            {
                return editModel;
            }
            set
            {
                editModel = value;
                this.RaisePropertyChanged("EditModel");
            }
        }

        private void Save()
        {
            //MessageBox.Show(str.ToString());
            switch (cmd)
            {
                case "Add":

                    if (bllGoodsInfo.Add(EditModel) > 0)
                    {
                        MessageBox.Show("成功了");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("失败了");
                    }
                    break;
                case "Edit":
                    if (bllGoodsInfo.Modify(EditModel) > 0)
                    {
                        MessageBox.Show("保存成功");
                        return;
                    }
                    MessageBox.Show("保存失败");
                    break;
                default:
                    break;
            }
        }
    }
}
