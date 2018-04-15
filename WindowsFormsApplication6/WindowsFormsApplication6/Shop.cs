
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication6
{
    class Shop
    {
        DataSet _shop;
        SqlDataAdapter _goodsAdapter = null;

        private void createGoodAdapter(string conectionString) {

            var selecCommand = "Select GoodsID, Name from goods";
            _goodsAdapter = new SqlDataAdapter(selecCommand, conectionString);
        }

        private void FillGoods()
        {

            _goodsAdapter.Fill(this.Goods);
        }

        private void UpdadeGoods()
         {

            _goodsAdapter.Update(this.Goods);

        }
        public DataTable Goods
        {
            get { return _shop.Tables["Goods"]; }


        }


        public Shop()
            : this("Data Source=.;" +
              "Initial Catalog=Shop;" +
              "User ID=sa;" +
              "Password=student;" )
        {

            
        }


        public Shop(string conectionString)
        {



            _shop = new DataSet();

           


            #region Goods
            DataTable tblGoods = CreateGoods();
            _shop.Tables.Add(tblGoods);
            #endregion

            #region Orders
            DataTable tblOrder = CreateOrders();
            _shop.Tables.Add(tblOrder);


            #endregion
            DataTable tblOrderDetails = CreateOrderDetails();
            _shop.Tables.Add(tblOrderDetails);

            For_Key_O_OD(tblOrder, tblOrderDetails);

            For_Key_G_OD(tblGoods, tblOrderDetails);
            _shop.EnforceConstraints = true;
        }

        private void For_Key_G_OD(DataTable tblGoods, DataTable tblOrderDetails)
        {
            var ParentColum = tblGoods.Columns["GoodsID"];
            var ChaildColum = tblOrderDetails.Columns["ArticleID"];
            var dr = new DataRelation("FK_G_OD", ParentColum, ChaildColum);
            _shop.Relations.Add(dr);


            //var fk = new ForeignKeyConstraint("O_ODet_C", ParentColum, ChaildColum);
            //fk.DeleteRule = Rule.Cascade;
            //fk.UpdateRule = Rule.Cascade;
            //fk.AcceptRejectRule = AcceptRejectRule.Cascade;

            //tblOrderDetails.Constraints.Add(fk);
            
        }
        
        private void For_Key_O_OD(DataTable tblOrder, DataTable tblOrderDetails)
        {
            var parentColum = tblOrder.Columns["OrderID"];
            var chaildColum = tblOrderDetails.Columns["OrderIDD"];
            var dr = new DataRelation("FK_Orders_OrdersDeteil", parentColum, chaildColum);
            _shop.Relations.Add(dr);


            //var fk = new ForeignKeyConstraint("O_ODet_C2", parentColum, chaildColum);
            //fk.DeleteRule = Rule.Cascade;
            //fk.UpdateRule = Rule.Cascade;
            //fk.AcceptRejectRule = AcceptRejectRule.Cascade;


            //tblOrderDetails.Constraints.Add(fk);
            
        }

        private DataTable CreateOrderDetails()
        {
            var tblOrderDetails = new DataTable("OrdersDateils");
            var clmOrderDeteilsId = new DataColumn()

            {
                AllowDBNull = false,
                ColumnName = "OrderIDD",
                Unique = true,
                AutoIncrement = true,
                AutoIncrementSeed = -1,
                AutoIncrementStep = -1,
                DataType = typeof(int)
            };

            tblOrderDetails.Columns.Add(clmOrderDeteilsId);

            var clmOrderId_details = new DataColumn()
            {

                AllowDBNull = false,
                ColumnName = "OrderID",
                DataType = typeof(int)


            };
            tblOrderDetails.Columns.Add(clmOrderId_details);

            var clmArId = new DataColumn()
            {

                AllowDBNull = false,
                ColumnName = "ArticleID",
                DataType = typeof(int)


            };
            tblOrderDetails.Columns.Add(clmArId);

            var clmfmount = new DataColumn()
            {
                AllowDBNull = false,
                ColumnName = "Fmount",
                DataType = typeof(double)

            };

            tblOrderDetails.Columns.Add(clmfmount);





            var clmPrice = new DataColumn()
            {
                AllowDBNull = false,
                ColumnName = "Price",
                DataType = typeof(double)

            };

            tblOrderDetails.Columns.Add(clmPrice);
            return tblOrderDetails;
        }

        private DataTable CreateOrders()
        {
            var tblOrder = new DataTable("Orders");
            var clmOrderId = new DataColumn()
            {
                AllowDBNull = false,
                ColumnName = "OrderID",
                Unique = true,
                AutoIncrement = true,
                AutoIncrementSeed = -1,
                AutoIncrementStep = -1,
                DataType = typeof(int)
            };
            tblOrder.Columns.Add(clmOrderId);

            var clmOrderDate = new DataColumn()
            {
                AllowDBNull = false,
                ColumnName = "Date time",
                DataType = typeof(DateTime)
            };
            tblOrder.Columns.Add(clmOrderDate);

            var clmOrderNumber = new DataColumn()
            {
                AllowDBNull = false,
                ColumnName = "Number",
                DataType = typeof(string)

            };

            tblOrder.Columns.Add(clmOrderNumber);
            return tblOrder;
        }

        private DataTable CreateGoods()
        {
            var tblGoods = new DataTable("Goods");
            var clmarticleid = new DataColumn()
            {
                AllowDBNull = false,
                ColumnName = "GoodsID",
                Unique = true,
                AutoIncrement = true,
                AutoIncrementSeed = -1,
                AutoIncrementStep = -1,
                DataType = typeof(int)
            };
            tblGoods.Columns.Add(clmarticleid);
            var column = new DataColumn()
            {
                AllowDBNull = false,
                ColumnName = "Name",
                MaxLength = 100,
                Unique = true,
                DataType = typeof(string)
            };
            tblGoods.Columns.Add(column);
            return tblGoods;
        }
    }
}
