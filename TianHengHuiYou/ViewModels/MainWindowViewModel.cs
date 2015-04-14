using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows;
using TianHengHuiYou.Models;
namespace TianHengHuiYou.ViewModels
{
    public class MainWindowViewModel:NotificationObject
    {
        public MainWindowViewModel()
        {
            RefreshGoodsInfo();
            //DataGrid加载信息
            this.RefreshGoodsInfoCommand = new DelegateCommand(RefreshGoodsInfo);
            this.AddGoodsInfoCommand = new DelegateCommand<MODEL.GoodsInfo>(AddGoodsInfo);
            this.EditGoodsInfoCommand = new DelegateCommand<MODEL.GoodsInfo>(EditGoodsInfo);
            this.DelGoodsInfoCommand = new DelegateCommand<MODEL.GoodsInfo>(DelGoodsInfo);
            this.AddDevBoardCommand = new DelegateCommand(AddDevBoard);
            this.DgGoodsListVisibility = Visibility.Visible;
            this.ListDevBoardCommand = new DelegateCommand(ListDevBoard);
            this.AddSupplyFactoryCommand = new DelegateCommand(AddSupplyFactory);
            this.InstoreCommand = new DelegateCommand(InStore);
        }
        private BLL.BLLGoodsInfo bllGoodsInfo = new BLL.BLLGoodsInfo();
        private BLL.BLLDevBoard bllDevBoard = new BLL.BLLDevBoard();
        public DelegateCommand RefreshGoodsInfoCommand { get; set; }
        public DelegateCommand<MODEL.GoodsInfo> AddGoodsInfoCommand { get; set; }
        public DelegateCommand<MODEL.GoodsInfo> EditGoodsInfoCommand { get; set; }
        public DelegateCommand<MODEL.GoodsInfo> DelGoodsInfoCommand{get;set;}
        public DelegateCommand AddDevBoardCommand { get; set; }
        public DelegateCommand ListDevBoardCommand { get; set; }
        public DelegateCommand AddSupplyFactoryCommand { get; set; }
        public DelegateCommand InstoreCommand { get; set; }
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

        private Visibility dgGoodsListVisibility;

        public Visibility DgGoodsListVisibility
        {
            get
            {
                return dgGoodsListVisibility;
            }
            set
            {
                dgGoodsListVisibility = value;
                this.RaisePropertyChanged("dgGoodsListVisibility");
            }
        }

        private Visibility dgDevBoardListVisibility;

        public Visibility DgDevBoardListVisibility
        {
            get
            {
                return dgDevBoardListVisibility;
            }
            set
            {
                dgDevBoardListVisibility = value;
                this.RaisePropertyChanged("dgDevBoardListVisibility");
            }
        }

        private List<DevBoardAndCostViewModel> dbCostViewModel;

        public List<DevBoardAndCostViewModel> DbCostViewModel
        {
            get { return dbCostViewModel; }
            set
            {
                dbCostViewModel = value;
                this.RaisePropertyChanged("DbCostViewModel");
            }
        }
        private List<DevBoardItem> listDevBoardItem;

        public List<DevBoardItem> ListDevBoardItem
        {
            get { return listDevBoardItem; }
            set
            {
                listDevBoardItem = value;
                this.RaisePropertyChanged("ListDevBoardItem");
            }
        }
        

        #region 1、刷新信息--void RefreshGoodsInfo()

        private void RefreshGoodsInfo()
        {
            this.DgDevBoardListVisibility = Visibility.Collapsed;
            this.DgGoodsListVisibility = Visibility.Visible;
            this.GoodsInfoList = bllGoodsInfo.GetListAll();
        } 
        #endregion

        #region 2、添加信息--void AddGoodsInfo(MODEL.GoodsInfo model)
        private void AddGoodsInfo(MODEL.GoodsInfo model)
        {
            this.DgDevBoardListVisibility = Visibility.Collapsed;
            this.DgGoodsListVisibility = Visibility.Visible;
            GoodsInfoDetailViewModel giDetailViewModel = new GoodsInfoDetailViewModel() { cmd = "Add" };
            giDetailViewModel.bllGoodsInfo = bllGoodsInfo;
            if (model ==null)
            {
                giDetailViewModel.EditModel = new MODEL.GoodsInfo();
            }
            else
            {
                giDetailViewModel.EditModel = model;
            }
            Views.GoodsInfoDetailWindow giDetailWindow = new Views.GoodsInfoDetailWindow();
            //giDetailWindow.DataContext = giDetailViewModel;
            giDetailWindow.DataContext = giDetailViewModel;
            giDetailWindow.tbGiCount.Text = "0";
            giDetailWindow.ShowDialog();
            RefreshGoodsInfo();
        } 
        #endregion

        #region 3、编辑信息--void EditGoodsInfo(MODEL.GoodsInfo model)
        private void EditGoodsInfo(MODEL.GoodsInfo model)
        {
            if (model == null)
            {
                MessageBox.Show("请先选择一行进行编辑");
                return;
            }
            this.DgDevBoardListVisibility = Visibility.Collapsed;
            this.DgGoodsListVisibility = Visibility.Visible;
            GoodsInfoDetailViewModel gidDetailViewModel = new GoodsInfoDetailViewModel() { cmd = "Edit", EditModel = model };
            gidDetailViewModel.bllGoodsInfo = bllGoodsInfo;
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
            if (model == null)
            {
                MessageBox.Show("请选择一列进行删除");
                return;
            }
            this.DgDevBoardListVisibility = Visibility.Collapsed;
            this.DgGoodsListVisibility = Visibility.Visible;
            if (bllGoodsInfo.Del(model) > 0)
            {
                MessageBox.Show("删除成功");
                RefreshGoodsInfo();
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

        #region 6、列出开发板--void ListDevBoard()
        private void ListDevBoard()
        {
            ListDevBoardItem = new List<DevBoardItem>();
            this.DgGoodsListVisibility = Visibility.Collapsed;
            this.DgDevBoardListVisibility = Visibility.Visible;
            bllDevBoard.GetListAll().ForEach(u => ListDevBoardItem.Add(AddDevBoardItem(u)));
        }
        /// <summary>
        /// 穿进去一个DevBoard返回一个DevBoardItem
        /// </summary>
        /// <returns></returns>
        private DevBoardItem AddDevBoardItem(MODEL.DevBoard devBoard)
        {
            DevBoardItem model = new DevBoardItem();
            model.devBoard = devBoard;
            model.Cost = CalCost(devBoard);
            return model;
        }
        /// <summary>
        /// 传递一个开发板计算成本：
        /// </summary>
        /// <returns></returns>
        private double? CalCost(MODEL.DevBoard model)
        {
           return model.DevBoardDetail.Sum(x => x.GoodsInfo.GIInPrice * x.DBDGoodCount);
        }
        #endregion

        #region 7、添加供应商
        private void AddSupplyFactory()
        {
            ViewModels.SupplyFactoryViewModel viewmodel = new SupplyFactoryViewModel();
            viewmodel.EditModel = new MODEL.SupplyFactory();
            Views.SupplyFactoryWindow view = new Views.SupplyFactoryWindow();
            view.DataContext = viewmodel;
            view.ShowDialog();
        }
        #endregion

        #region 8、入库
        private void InStore()
        {
            Views.InstoreWindow window = new Views.InstoreWindow();
            window.ShowDialog();
        }
        #endregion
    }
}
