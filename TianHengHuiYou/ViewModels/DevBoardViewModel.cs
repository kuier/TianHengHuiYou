using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows;
namespace TianHengHuiYou.ViewModels
{
    public class DevBoardViewModel:NotificationObject
    {
        public DevBoardViewModel()
        {
            this.AddDevBoardCommand = new DelegateCommand(AddDevBoard);
            this.SearchCommand = new DelegateCommand(Search);
            this.AddDevGoodsCommand = new DelegateCommand(AddDevGoods);
        }

        #region 2、命令属性和依赖属性
        private BLL.BLLDevBoard bllDevBoard = BLL.BLLDevBoard.GetBllGoodsInfo();
        private BLL.BLLDevBoardDetail bllDevBoardDetail = BLL.BLLDevBoardDetail.GetBLLDevBoardDetail();
        public DelegateCommand AddDevBoardCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand AddDevGoodsCommand { get; set; }
        private string tbdevName;
        /// <summary>
        /// 开发板名称
        /// </summary>
        public string TbDevName
        {
            get
            {
                return tbdevName;
            }
            set
            {
                tbdevName = value;
                this.RaisePropertyChanged("DevName");
            }
        }
        private string giEncoderSearch;
        /// <summary>
        /// 货品编码搜索
        /// </summary>
        public string GiEncoderSearch
        {
            get
            {
                return giEncoderSearch;
            }
            set
            {
                giEncoderSearch = value;
                this.RaisePropertyChanged("GiEncoderSearch");
            }
        }
        private string giNameSearch;
        /// <summary>
        /// 货品名称搜索
        /// </summary>
        public string GiNameSearch
        {
            get
            {
                return giNameSearch;
            }
            set
            {
                giNameSearch = value;
                this.RaisePropertyChanged("GiNameSearch");
            }
        }
        private int tbDGoodCount;

        public int TbDGoodCount
        {
            get
            {
                return tbDGoodCount;
            }
            set
            {
                tbDGoodCount = value;
                this.RaisePropertyChanged("TbDGoodCount");
            }
        }

        private string tBDRemark;

        public string TBDRemark
        {
            get
            {
                return tBDRemark;
            }
            set
            {
                tBDRemark = value;
                this.RaisePropertyChanged("TBDRemark");
            }
        }

        private List<MODEL.GoodsInfo> goodsList;
        /// <summary>
        /// 货品列表
        /// </summary>
        public List<MODEL.GoodsInfo> GoodsList
        {
            get
            {
                return goodsList;
            }
            set
            {
                goodsList = value;
                this.RaisePropertyChanged("GoodsList");
            }
        }
        private MODEL.GoodsInfo editModel;
        /// <summary>
        /// 当前选中列表
        /// </summary>
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
        private List<MODEL.GoodsInfo> devBoardGoodsInfo;
        public List<MODEL.GoodsInfo> DevBoardGoodsInfo
        {
            get
            {
                return devBoardGoodsInfo;
            }
            set
            {
                devBoardGoodsInfo = value;
                this.RaisePropertyChanged("DevBoardGoodsInfo");
            }
        }
        private string currentDevName;

        public string CurrentDevName
        {
            get
            {
                return currentDevName;
            }
            set
            {
                currentDevName = value;
                this.RaisePropertyChanged("CurrentDevName");
            }
        }
        private ICollection<MODEL.DevBoardDetail> devBoardDetailList;

        public ICollection<MODEL.DevBoardDetail> DevBoardDetailList
        {
            get
            {
                return devBoardDetailList;
            }
            set
            {
                devBoardDetailList = value;
                this.RaisePropertyChanged("DevBoardDetailList");
            }
        }
        private MODEL.DevBoard currentDevBoard;

        public MODEL.DevBoard CurrentDevBoard
        {
            get
            {
                return currentDevBoard;
            }
            set
            {
                currentDevBoard = value;
                this.RaisePropertyChanged("CurrentDevBoard");
            }
        }

        #endregion

        private void Search()
        {
            if (string.IsNullOrWhiteSpace(GiEncoderSearch)&&string.IsNullOrWhiteSpace(GiEncoderSearch))
            {
                MessageBox.Show("都是空的");
                this.GoodsList = BLL.BLLGoodsInfo.GetBllGoodsInfo().GetListAll();
            }
            else
            {
                MessageBox.Show("有数据");
            }
        }

        private void AddDevBoard()
        {
            if (string.IsNullOrWhiteSpace(TbDevName))
            {
                MessageBox.Show("请输入开发板名称");
                return;
            } 
            if (bllDevBoard.GetListBy(u => u.DBName == TbDevName).Count > 0)
            {
                MessageBox.Show("已经存在这个名称了，请继续编辑");
                this.CurrentDevName = TbDevName;
                this.CurrentDevBoard = bllDevBoard.GetListBy(u => u.DBName == TbDevName)[0];
                this.DevBoardDetailList = CurrentDevBoard.DevBoardDetail;
                return;
            }
            if (bllDevBoard.Add(new MODEL.DevBoard() { DBName = TbDevName }) <= 0)
            {
                MessageBox.Show("添加失败");
            }
            this.CurrentDevName = TbDevName;
        }

        private void AddDevGoods()
        {
            MODEL.DevBoardDetail devDeatail = new MODEL.DevBoardDetail() { DBDGoodId = EditModel.GIId, DBDGoodCount = TbDGoodCount, DBDDevId = CurrentDevBoard.DBId, DBDRemark = TBDRemark};
            if (bllDevBoardDetail.Add(devDeatail)<=0)
            {
                MessageBox.Show("请稍后重试");
                return;
            }
            DevBoardDetailList.Add(devDeatail);
            CurrentDevBoard = bllDevBoard.GetListBy(u => u.DBName == TbDevName)[0];
        }
    }
}
