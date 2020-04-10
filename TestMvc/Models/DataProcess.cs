using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace TestMvc.Models
{
    public class DataProcess
    {
        public string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        /// <summary>
        /// 讀取列表資料
        /// </summary>
        /// <returns></returns>
        public List<ListViewModel> GetListData()
        {
            List<ListViewModel> vm = new List<ListViewModel>();

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select L.OrderId,ProductName,Price,Cost,S.*,P.ProductId from OrderList L inner join OrderItem I on L.OrderId = I.OrderId inner join Product P on I.ProductId = P.ProductId inner join Status S on L.Status = S.StatusCode", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewModel _vm = new ListViewModel();
                    _vm.OrderId = dr[0].ToString();
                    _vm.OrderItem = dr[1].ToString();
                    _vm.Price = Convert.ToInt32(dr[2] ?? 0);
                    _vm.Cost = Convert.ToInt32(dr[3] ?? 0);
                    _vm.Status = dr[5].ToString();
                    _vm.ProductId = dr[6].ToString();
                    _vm.StatusCode = Convert.ToInt16(dr[4] ?? 0);

                    vm.Add(_vm);
                }
            }

            return vm;
        }

        /// <summary>
        /// 讀取商品資料
        /// </summary>
        /// <returns></returns>
        public DetailViewModel GetDetailData(string id)
        {
            DetailViewModel vm = new DetailViewModel();

            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Product where ProductId=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    vm.ProductName = dr[1].ToString();
                    vm.Price = Convert.ToInt32(dr[2] ?? 0);
                    vm.Cost = Convert.ToInt32(dr[3] ?? 0);
                }
            }

            return vm;
        }

        /// <summary>
        /// 更新訂單狀態
        /// </summary>
        /// <returns></returns>
        public void UpdateStatus(string[] OrderId)
        {
            //刪掉沒有被勾選的項目(vlaue=="false")
            OrderId = OrderId.Where(x => !x.Equals("false")).ToArray();

            if (OrderId.Length > 0)
            {
                try
                {
                    //組sql where in字串、加入parameter
                    string sqlWhere = "";
                    List<SqlParameter> SqlParameters = new List<SqlParameter>();
                    for (int i = 0; i < OrderId.Length; i++)
                    {
                        string param = string.Format("@OrderId{0}", i.ToString());
                        sqlWhere += string.Format("{0},", param);
                        SqlParameters.Add(new SqlParameter(param, OrderId[i]));
                    }

                    //去除最後一個逗號
                    sqlWhere = sqlWhere.Substring(0, sqlWhere.Length - 1);

                    using (SqlConnection conn = new SqlConnection(ConnString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(string.Format("update OrderList set Status=2 where OrderId in ({0})", sqlWhere), conn);
                        
                        cmd.Parameters.AddRange(SqlParameters.ToArray());
                        cmd.ExecuteNonQuery();
                    }
                }
                catch { }
            }

        }


    }
}