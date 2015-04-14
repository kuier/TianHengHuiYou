using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TianHengHuiYou.Tools;
using System.Linq.Expressions;
using System.Windows;

namespace TianHengHuiYou.ViewModels
{
    public class InstoreGoodsInfoViewModel:NotificationObject
    {
        public InstoreGoodsInfoViewModel()
        {
            EditModel = new MODEL.GoodsInfo();
            this.SearchCommand = new DelegateCommand(Search);
            this.SelectGoodCommand = new DelegateCommand(SelectGoods);
        }
        public int IsOrderId { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand SelectGoodCommand { get; set; }
        private BLL.BLLGoodsInfo bllGoodsInfo = new BLL.BLLGoodsInfo();
        private BLL.BLLInstoreDetail bllInstoreDetail = new BLL.BLLInstoreDetail();
        private MODEL.GoodsInfo editModel;

        public MODEL.GoodsInfo EditModel
        {
            get { return editModel; }
            set
            {
                editModel = value;
                this.RaisePropertyChanged("EditModel");
                //this.RaisePropertyChanged<bool>(() => InStoreDetialModel.ISDGoodId == EditModel.GIId);
            }
        }

        private List<MODEL.GoodsInfo> goodsInfoList;

        public List<MODEL.GoodsInfo> GoodsInfoList
        {
            get { return goodsInfoList; }
            set
            {
                goodsInfoList = value;
                this.RaisePropertyChanged("GoodsInfoList");
            }
        }
        private MODEL.InStoreDetial inStoreDetialModel;

        public MODEL.InStoreDetial InStoreDetialModel
        {
            get { return inStoreDetialModel; }
            set
            {
                inStoreDetialModel = value;
                this.RaisePropertyChanged("InStoreDetialModel");
            }
        }
        private int? iSDGoodCount;

        public int? ISDGoodCount
        {
            get { return iSDGoodCount; }
            set
            {
                iSDGoodCount = value;
                this.RaisePropertyChanged("ISDGoodCount");
            }
        }
        private int? iSDGoodPrice;

        public int? ISDGoodPrice
        {
            get { return iSDGoodPrice; }
            set
            {
                iSDGoodPrice = value;
                this.RaisePropertyChanged("ISDGoodPrice");
            }
        }


        #region 货品编码查询参数
        private string strgiEncoder;

        public string strGiEncoder
        {
            get { return strgiEncoder; }
            set
            {
                strgiEncoder = value;
                this.RaisePropertyChanged("strGiEncoder");
            }
        } 
        #endregion
        #region 货品规格查询参数
        private string strgiSize;

        public string strGiSize
        {
            get { return strgiSize; }
            set
            {
                strgiSize = value;
                this.RaisePropertyChanged("strGiSize");
            }
        } 
        #endregion
        #region 货品名称查询参数
        private string strgiName;

        public string strGiName
        {
            get { return strgiName; }
            set
            {
                strgiName = value;
                this.RaisePropertyChanged("strGiName");
            }
        } 
        #endregion

        private void Search()
        {
            Expression<Func<MODEL.GoodsInfo, bool>> where = PredicateExtensionses.True<MODEL.GoodsInfo>();
            if (!string.IsNullOrWhiteSpace(strGiEncoder))
            {
                where =  where.And(u => u.GIEncoder == strGiEncoder);
            }
            if (!string.IsNullOrWhiteSpace(strGiName))
            {
               where= where.And(u => u.GIName == strGiName);
            }
            if (!string.IsNullOrWhiteSpace(strGiSize))
            {
                where = where.And(u => u.GISize == strGiSize);
            }
            GoodsInfoList =bllGoodsInfo.GetListBy(where);
        }
        #region 选择货品
        /// <summary>
        /// 选择货品
        /// </summary>
        private void SelectGoods()
        {
            MODEL.InStoreDetial model = new MODEL.InStoreDetial() { ISDGoodId = EditModel.GIId, ISDGoodCount = this.ISDGoodCount, ISDGoodPrice = this.ISDGoodPrice, ISDGoodTotalPrice = this.ISDGoodPrice * this.ISDGoodCount, ISDOrderId= IsOrderId };
            //MODEL.GoodsInfo goods = new MODEL.GoodsInfo() { GIId = EditModel.GIId, GICount = EditModel.GICount + this.iSDGoodCount };
            if (bllInstoreDetail.Add(model)>0)
            {
                EditModel.GICount -= this.iSDGoodCount;
                bllGoodsInfo.Modify(EditModel);
                MessageBox.Show("添加成功");
                return;
            }
            MessageBox.Show("添加失败");
        } 
        #endregion
    }
}
