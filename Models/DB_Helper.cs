using Models;
using System.Data;
using System.Data.SqlClient;

namespace Models
{
    /// <summary>
    /// 宣告公開類別。(DB 輔助操作)
    /// </summary>
    public class DB_Helper_Cs
    {
        /// <summary>
        /// 宣告公開靜態整數方法。(取得資料表特定欄位編號資料最大值)
        /// </summary>
        /// <param name="strFieldName_Val">欄位名稱</param>
        /// <param name="strTableName_Val">表格名稱</param>
        /// <param name="strConnectionString_Val">資料庫連線字串</param>
        public static int GetMaxFieldData_Md(string strFieldName_Val, string strTableName_Val, string strConnectionString_Val)
        {
            //宣告整數變數。(最大值)
            int intMaxNo = 0;

            //要執行的 SQL 陳述式語法。
            string objSQL = "SELECT MAX([" + strFieldName_Val + "]) FROM [" + strTableName_Val + "]";

            //建立資料流讀取物件並自動釋放佔用資源。(連接取得資料來源、命令類型、SQL 語法)
            using (SqlDataReader objSqlDataReader = SqlHelper.ExecuteReader(strConnectionString_Val, CommandType.Text, objSQL))
            {
                //讀取資料流的資料記錄。
                while (objSqlDataReader.Read())
                {
                    //當指定欄位值無值時。
                    if (objSqlDataReader.IsDBNull(0))
                    {
                        //指派最大值為 1。
                        intMaxNo = 1;
                    }
                    else //當指定欄位值有值時。
                    {
                        //指派(最大值)為，欄位取出值加1。
                        intMaxNo = objSqlDataReader.GetInt32(0) + 1;
                    }
                }
            }

            //傳回處理結果。
            return intMaxNo;
        }

    }
}