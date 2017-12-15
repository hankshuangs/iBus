using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interface;
using Models.RepositoryPattern_ADO;
using System.Data.SqlClient;
using System.Data;

namespace Models.Repository
{
    public class Repository_Article : IRepository_Article
    {
        #region Article 資料操作類別

        #region 顯示資料操作

        /// <summary>
        /// 列表全部文章方法
        /// </summary>
        public IList<Article> ListData_Md()
        {
            //宣告與建構 IList 泛型集合物件操作案例。(文章資料模型)
            IList<Article> objListItem = new List<Article>();

            //宣告字串變數。(SQL 陳述式語法) 
            string strSQL =
                "SELECT NO_F, Title_F, Content_F, JoinTime_F, ViewCount_F, AccountNO_F " +
                "FROM [Article_Tb] " +
                "ORDER BY [NO_F] DESC";

            //建立資料流讀取物件並自動釋放佔用資源。(連接取得資料來源、命令類型、SQL 語法)
            using (SqlDataReader objSqlDataReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, strSQL, null))
            {
                //讀取資料流的資料記錄。
                while (objSqlDataReader.Read())
                {
                    //將取出的資料庫欄位值指派寫入資料模型欄位內容值。
                    //因為 SqlDataReader 是弱型別物件，所以其內容值必須轉型。
                    objListItem.Add(new Article
                    {
                        NO_F = int.Parse(objSqlDataReader["NO_F"].ToString()),
                        Title_F = objSqlDataReader["Title_F"].ToString(),
                        Content_F = objSqlDataReader["Content_F"].ToString(),
                        JoinTime_F = DateTime.Parse(objSqlDataReader["JoinTime_F"].ToString()),
                        ViewCount_F = int.Parse(objSqlDataReader["ViewCount_F"].ToString()),
                        AccountNO_F = int.Parse(objSqlDataReader["AccountNO_F"].ToString()),
                    });
                }
            }

            //傳回結果。
            return objListItem;
        }

        /// <summary>
        /// 取得單一文章依據編號方法
        /// </summary>
        /// <param name="intNO_Val">編號</param>
        public Article GetDataByNO_Md(int intNO_Val)
        {
            //宣告字串變數。(SQL 陳述式語法) 
            string strSQL =
                "SELECT NO_F, Title_F, Content_F, JoinTime_F, ViewCount_F, AccountNO_F " +
                "FROM [Article_Tb] " +
                "WHERE NO_F = @NO_F " +
                "ORDER BY [NO_F] DESC";

            //建立命令參數列物件。(傳入參數列)
            SqlParameter[] objSqlParameter = new SqlParameter[]
            {
                new SqlParameter("@NO_F", SqlDbType.Int)
            };

            //指定要寫入欄位的資料值。
            objSqlParameter[0].Value = intNO_Val;

            //建立資料流讀取物件並自動釋放佔用資源。(連接取得資料來源、命令類型、SQL 語法、輸入參數列)
            using (SqlDataReader objSqlDataReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, strSQL, objSqlParameter))
            {
                //當有資料記錄時。
                if (objSqlDataReader.Read())
                {
                    //將取出的資料庫欄位值指派寫入建立資料模型欄位內容值。
                    //因為 SqlDataReader 是弱型別物件，所以其內容值必須轉型。
                    return new Article
                    {
                        NO_F = int.Parse(objSqlDataReader["NO_F"].ToString()),
                        Title_F = objSqlDataReader["Title_F"].ToString(),
                        Content_F = objSqlDataReader["Content_F"].ToString(),
                        JoinTime_F = DateTime.Parse(objSqlDataReader["JoinTime_F"].ToString()),
                        ViewCount_F = int.Parse(objSqlDataReader["ViewCount_F"].ToString()),
                        AccountNO_F = int.Parse(objSqlDataReader["AccountNO_F"].ToString()),
                    };
                }
                else //沒有資料記錄時。
                {
                    //傳回空的物件型別。(避免找不記錄時產生 null 物件的例外錯誤)
                    return new Article();
                }
            }
        }

        /// <summary>
        /// 取得全部文章筆數方法
        /// </summary>
        public int GetDataCount_Md()
        {
            //宣告字串變數。(SQL 陳述式語法) 
            string strSQL = "SELECT COUNT(NO_F) FROM [Article_Tb]";

            //傳回執行查詢的記錄筆數。(連接取得資料來源、命令類型、SQL 語法)
            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, strSQL, null);
        }

        /// <summary>
        /// 取得文章最新編號方法
        /// </summary>
        public int GetMaxDataNO_Md()
        {
            //傳回指定資料表中欄位的最大值。
            return DB_Helper_Cs.GetMaxFieldData_Md("NO_F", "Article_Tb", SqlHelper.ConnectionString);
        }

        #endregion

        #region 異動資料操作

        /// <summary>
        /// 插入單筆文章方法
        /// </summary>
        /// <param name="objArticle_Val">文章資料模型</param>
        public bool Insert_Md(Article objArticle_Val)
        {
            //宣告字串變數。(SQL 陳述式語法) 
            string strSQL = "INSERT INTO [Article_Tb] (Title_F, Content_F, JoinTime_F, ViewCount_F, AccountNO_F) VALUES (@Title_F, @Content_F, @JoinTime_F, @ViewCount_F, @AccountNO_F)";

            //建立命令參數列物件。(傳入參數列)
            SqlParameter[] objSqlParameter = new SqlParameter[]
            {
                new SqlParameter("@Title_F", SqlDbType.NVarChar),
                new SqlParameter("@Content_F", SqlDbType.NText),
                new SqlParameter("@JoinTime_F", SqlDbType.DateTime),
                new SqlParameter("@ViewCount_F", SqlDbType.Int),
                new SqlParameter("@AccountNO_F", SqlDbType.Int)
            };

            //指定要寫入欄位的資料值。
            objSqlParameter[0].Value = objArticle_Val.Title_F;
            objSqlParameter[1].Value = objArticle_Val.Content_F;
            objSqlParameter[2].Value = objArticle_Val.JoinTime_F;
            objSqlParameter[3].Value = objArticle_Val.ViewCount_F;
            objSqlParameter[4].Value = objArticle_Val.AccountNO_F;

            //傳回取得從資料庫資料異動的記錄筆數是否大於 0。
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, strSQL, objSqlParameter) > 0;
        }

        /// <summary>
        /// 更新單筆文章方法
        /// </summary>
        /// <param name="objArticle_Val">文章資料模型</param>
        public bool Update_Md(Article objArticle_Val)
        {
            //宣告字串變數。(SQL 陳述式語法) 
            string strSQL = "UPDATE [Article_Tb] SET [Title_F] = @Title_F, [Content_F] = @Content_F WHERE [NO_F] = @NO_F";

            //建立命令參數列物件。(傳入參數列)
            SqlParameter[] objSqlParameter = new SqlParameter[]
            {
                new SqlParameter("@Title_F", SqlDbType.NVarChar),
                new SqlParameter("@Content_F", SqlDbType.NText),
                new SqlParameter("@NO_F", SqlDbType.Int)
            };

            //指定要寫入欄位的資料值。
            objSqlParameter[0].Value = objArticle_Val.Title_F;
            objSqlParameter[1].Value = objArticle_Val.Content_F;
            objSqlParameter[2].Value = objArticle_Val.NO_F;

            //傳回取得從資料庫資料異動的記錄筆數是否大於 0。
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, strSQL, objSqlParameter) > 0;
        }

        /// <summary>
        /// 刪除單筆文章方法
        /// </summary>
        /// <param name="intNO_Val">編號</param>
        public bool Delete_Md(int intNO_Val)
        {
            //宣告字串變數。(SQL 陳述式語法) 
            string strSQL = "DELETE FROM [Article_Tb] WHERE [NO_F] = @NO_F";

            //建立命令參數列物件。(傳入參數列)
            SqlParameter[] objSqlParameter = new SqlParameter[]
            {
                new SqlParameter("@NO_F", SqlDbType.Int)
            };

            //指定要寫入欄位的資料值。
            objSqlParameter[0].Value = intNO_Val;

            //傳回取得從資料庫資料異動的記錄筆數是否大於 0。
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, strSQL, objSqlParameter) > 0;
        }

        #endregion

        #endregion
    }
}
