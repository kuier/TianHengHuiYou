using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows;
namespace TianHengHuiYou.ViewModels
{
    public class MainWindowViewModel:NotificationObject
    {
        public MainWindowViewModel()
        {
            //DataGrid加载信息
            RefreshGoodsInfo();
            this.RefreshGoodsInfoCommand = new DelegateCommand(RefreshGoodsInfo);
            this.AddGoodsInfoCommand = new DelegateCommand<MODEL.GoodsInfo>(AddGoodsInfo);
            this.EditGoodsInfoCommand = new DelegateCommand<MODEL.GoodsInfo>(EditGoodsInfo);
            this.DelGoodsInfoCommand = new DelegateCommand<MODEL.GoodsInfo>(DelGoodsInfo);
            this.AddDevBoardCommand = new DelegateCommand(AddDevBoard);
            HibernatingRhinos.Profiler.Appender.EntityFramework.
    EntityFrameworkProfiler.Initialize();
        }

        public DelegateCommand RefreshGoodsInfoCommand { get; set; }
        public DelegateCommand<MODEL.GoodsInfo> AddGoodsInfoCommand { get; set; }
        public DelegateCommand<MODEL.GoodsInfo> EditGoodsInfoCommand { get; set; }
        public DelegateCommand<MODEL.GoodsInfo> DelGoodsInfoCommand{get;set;}
        public DelegateCommand AddDevBoardCommand { get; set; }
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
        
        private List<MODEL.GoodsInfo> goodsInfoList;

        public List<MODEL.GoodsInfo> GoodsInfoList
        {
            get
            {
                return goodsInfoList;
            }
            set
            {
                goodsInfoList = value;
                this.RaisePropertyChanged("GoodsInfoList");
            }
        }
        #region 1、刷新信息--void RefreshGoodsInfo()

        private void RefreshGoodsInfo()
        {
            this.GoodsInfoList = BLL.BLLGoodsInfo.GetBllGoodsInfo().GetListAll();
        } 
        #endregion

        #region 2、添加信息--void AddGoodsInfo(MODEL.GoodsInfo model)
        private void AddGoodsInfo(MODEL.GoodsInfo model)
        {
            GoodsInfoDetailViewModel giDetailViewModel = new GoodsInfoDetailViewModel() { cmd = "Add", EditModel = model };
            Views.GoodsInfoDetailWindow giDetailWindow = new Views.GoodsInfoDetailWindow();
            //giDetailWindow.DataContext = giDetailViewModel;
            giDetailWindow.DataContext = giDetailViewModel;
            giDetailWindow.tbGiCount.Text = "0";
            giDetailWindow.ShowDialog();
        } 
        #endregion

        #region 3、编辑信息--void EditGoodsInfo(MODEL.GoodsInfo model)
        private void EditGoodsInfo(MODEL.GoodsInfo model)
        {
            GoodsInfoDetailViewModel gidDetailViewModel = new GoodsInfoDetailViewModel() { cmd = "Edit", EditModel = model };
            Views.GoodsInfoDetailWindow giDetailWindow = new Views.GoodsInfoDetailWindow();
            giDetailWindow.DataContext = gidDetailViewModel;
            giDetailWindow.tbGIEncoder.IsReadOnly = true;
            giDetailWindow.tbGiCount.IsReadOnly = true;
            giDetailWindow.ShowDialog();
        } 
        #endregion

        #region 4、删除货品信息--void DelGoodsInfo(MODEL.GoodsInfo model)
        private void DelGoodsInfo(MODEL.GoodsInfo model)
        {
            if (BLL.BLLGoodsInfo.GetBllGoodsInfo().Del(model) > 0)
            {
                MessageBox.Show("删除成功");
                return;
            }
            MessageBox.Show("删除失败");
        } 
        #endregion

        #region 5、添加开发板--void AddDevBoard()
        private void AddDevBoard()
        {
            Views.DevBoardWindow devBoardWindow = new Views.DevBoardWindow();
            devBoardWindow.ShowDialog();
        } 
        #endregion
    }
}
