//using System.Data.OracleClient;
//↑須另外加入System.Data.OracleClient參考，才能using此命名空間，不過通常很少用OracleClient，所以把它註解掉
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;

namespace Models
{
    /// <summary>
    /// 此類別為抽象類別，不允許被new，在使用時直接調用即可
    /// </summary>
    public abstract class SqlHelper
    {

        /// <summary>
        /// SqlHelper預設資料庫連線字串
        /// </summary>
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["MvcDbContext"].ConnectionString.Trim();

        #region 資料提供者
        /// <summary>
        /// 資料提供者，依據目標資料庫不同，須修改不同類型的DbProviderFactory
        /// </summary>
        private static readonly DbProviderFactory dbProviderFactory = SqlClientFactory.Instance;
        //private static readonly DbProviderFactory dbProviderFactory = OdbcFactory.Instance;
        //private static readonly DbProviderFactory dbProviderFactory = OleDbFactory.Instance;
        //private static readonly DbProviderFactory dbProviderFactory = OracleClientFactory.Instance;
        #endregion 


        /// <summary>
        /// 為執行命令準備參數
        /// </summary>
        /// <param name="cmd">DbCommand 命令</param>
        /// <param name="conn">資料庫連線</param>
        /// <param name="trans">交易處理</param>
        /// <param name="cmdType">DbCommand類型 (CommandType.StoredProcedure或CommandType.Text)</param>
        /// <param name="cmdText">DbCommand的T-SQL语句 例如：Select * from Products</param>
        /// <param name="cmdParms">使用到的參數集合</param>
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText,
            DbParameter[] cmdParms)
        {
            //判斷資料庫連線狀態
            if (conn.State != ConnectionState.Open) { conn.Open(); }
            //判斷是否需要交易處理
            if (trans != null) { cmd.Transaction = trans; }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (DbParameter param in cmdParms) { cmd.Parameters.Add(param); }
            }
        }

        //#region 連線資料庫存取

        //#region ExecuteNonQuery 異動資料

        ///// <summary>
        ///// 執行新增、修改、刪除指令，透過指定連接字串。
        ///// </summary>
        ///// <param name="connectionString">工程師自行指定DB連線字串</param>
        ///// <param name="cmdType">DbCommand類型 (CommandType.StoredProcedure或CommandType.Text)</param>
        ///// <param name="cmdText">預存程式名稱 或 T-SQL 语句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>受影響的資料筆數</returns>
        //public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        //{
        //    using (DbConnection conn = dbProviderFactory.CreateConnection())
        //    {
        //        conn.ConnectionString = connectionString;
        //        DbCommand cmd = conn.CreateCommand();
        //        //通過PrePareCommand方法將參數逐個加入到DbCommand的参數集合中
        //        PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
        //        int val = cmd.ExecuteNonQuery();
        //        //清空SqlCommand中的参数列表
        //        cmd.Parameters.Clear();
        //        return val;

        //    }//end using 

        //}

        ///// <summary>
        ///// 執行新增、修改、刪除指令，使用SqlHelper預設連接字串。
        ///// </summary>
        ///// <param name="cmdType">DbCommand類型 (CommandType.StoredProcedure或CommandType.Text)</param>
        ///// <param name="cmdText">預存程式名稱 或 T-SQL 语句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>受影響的資料筆數</returns>
        //public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        //{
        //    return ExecuteNonQuery(connectionString, cmdType, cmdText, commandParameters);
        //}

        ///// <summary>
        ///// 執行新增、修改、刪除指令，使用SqlHelper預設連接字串、CommandType.Text
        ///// </summary>
        ///// <param name="cmdText">CommandType.Text的T-Sql語句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>受影響的資料筆數</returns>
        //public static int ExecuteNonQueryText(string cmdText, params DbParameter[] commandParameters)
        //{
        //    return ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        //}

        //#endregion

        //#region  ExecuteScalar

        ///// <summary>
        ///// 取得第一行第一列的資料，通常使用在Select Count(*) From TableName 有聚合函數的Select指令
        ///// 工程師自行指定DB連線字串
        ///// </summary>
        ///// <param name="connectionString">工程師自行指定DB連線字串</param>
        ///// <param name="cmdType">DbCommand類型 (CommandType.StoredProcedure或CommandType.Text)</param>
        ///// <param name="cmdText">預存程式名稱 或 T-SQL 語句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>回傳第一行第一列的資料，型別不確定</returns>
        //public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        //{
        //    using (DbConnection conn = dbProviderFactory.CreateConnection())
        //    {
        //        conn.ConnectionString = connectionString;//連線字串
        //        DbCommand cmd = conn.CreateCommand();
        //        PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
        //        object val = cmd.ExecuteScalar();
        //        cmd.Parameters.Clear();//清除參數集合
        //        conn.Close();
        //        return val;
        //    }
        //}
        ///// <summary>
        ///// 取得第一行第一列的資料，通常使用在Select Count(*) From TableName 有聚合函數的Select指令
        ///// 使用SqlHelper預設DB連線字串
        ///// </summary>
        ///// <param name="cmdType">DbCommand類型 (CommandType.StoredProcedure或CommandType.Text)</param>
        ///// <param name="cmdText">預存程式名稱 或 T-SQL 語句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>回傳第一行第一列的資料，型別不確定</returns>
        //public static object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        //{
        //    return ExecuteScalar(connectionString, cmdType, cmdText, commandParameters);
        //}


        ///// <summary>
        ///// 取得第一行第一列的資料，通常使用在Select Count(*) From TableName 有聚合函數的Select指令
        ///// 使用SqlHelper預設DB連線字串、CommandType.Text
        ///// </summary>
        ///// <param name="cmdText">T-SQL 語句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>回傳第一行第一列的資料，型別不確定</returns>
        //public static object ExecuteScalarText(string cmdText, params DbParameter[] commandParameters)
        //{
        //    return ExecuteScalar(connectionString, CommandType.Text, cmdText, commandParameters);
        //}

        //#endregion

        //#region ExecuteReader 
        ///// <summary>
        ///// 執行Select查詢指令，工程師指定連線字串。
        ///// 前端呼叫時記得要用using包住回傳的DbDataReader變數來關閉連線
        ///// </summary>
        ///// <param name="connectionString">工程師自行指定DB連線字串</param>
        ///// <param name="cmdType">DbCommand類型 (CommandType.StoredProcedure或CommandType.Text)</param>
        ///// <param name="cmdText">預存程式名稱 或 T-SQL 語句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>回傳DbDataReader指標</returns>
        //public static DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        //{

        //    DbConnection conn = dbProviderFactory.CreateConnection();
        //    conn.ConnectionString = connectionString;//連線字串
        //    DbCommand cmd = conn.CreateCommand();
        //    //↓不寫這行的話，由實作的Provider決定數值，OleDb、Odbc、SqlClient預設30秒，OracleClient為0不逾時
        //    cmd.CommandTimeout = 0;//執行SQL指令時間，0為不逾時
        //    try
        //    {
        //        //開啟連線
        //        PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
        //        DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        cmd.Parameters.Clear();//清除參數集合
        //        return reader;
        //    }
        //    catch (DbException ex)
        //    {
        //        conn.Close();//發生查詢例外就關閉連線
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// 執行Select查詢指令，使用SqlHelper預設連線字串。
        ///// 前端呼叫時記得要用using包住回傳的DbDataReader變數來關閉連線
        ///// </summary>
        ///// <param name="cmdType">DbCommand類型 (CommandType.StoredProcedure或CommandType.Text)</param>
        ///// <param name="cmdText">預存程式名稱 或 T-SQL 語句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>回傳DbDataReader指標</returns>
        //public static DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        //{
        //    return ExecuteReader(connectionString, cmdType, cmdText, commandParameters);
        //}

        ///// <summary>
        ///// 執行Select查詢指令，使用SqlHelper預設連線字串、CommandType.Text
        ///// 前端呼叫時記得要用using包住回傳的DbDataReader變數來關閉連線
        ///// </summary>
        ///// <param name="cmdText">T-SQL 語句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>回傳DbDataReader指標</returns>
        //public static DbDataReader ExecuteReaderText(string cmdText, params DbParameter[] commandParameters)
        //{
        //    return ExecuteReader(CommandType.Text, cmdText, commandParameters);
        //}

        //#endregion

        //#region 交易
        ///// <summary>
        ///// 執行多條SQL語句，實現資料庫交易，使用SqlHelper預設DB連線字串
        ///// </summary>
        ///// <param name="sqlStringList">參數集合</param>
        //public static void ExecuteSqlTran(Dictionary<string, DbParameter[]> sqlStringList)
        //{
        //    ExecuteSqlTran(connectionString, CommandType.Text, sqlStringList);
        //}
        ///// <summary>
        ///// 執行多條SQL語句，實現資料庫交易
        ///// </summary>
        ///// <param name="sqlStringList">多條SQL語句</param>  
        //public static void ExecuteSqlTran(string connectionString, CommandType cmdType, Dictionary<string, DbParameter[]> sqlStringList)
        //{

        //    using (DbConnection conn = dbProviderFactory.CreateConnection())
        //    {
        //        conn.ConnectionString = connectionString;//指定連線字串
        //        conn.Open();//開啟連線
        //        DbTransaction trans = conn.BeginTransaction();//開始交易
        //        DbCommand cmd = conn.CreateCommand();
        //        try
        //        {
        //            foreach (KeyValuePair<string, DbParameter[]> item in sqlStringList)
        //            {
        //                PrepareCommand(cmd, conn, trans, cmdType, item.Key, item.Value);
        //                cmd.ExecuteNonQuery();//執行一筆SQL異動語句
        //                //清空DbCommand中的參數集合
        //                cmd.Parameters.Clear();
        //            }//end foreach
        //            trans.Commit();//交易提交
        //        }
        //        catch (DbException ex)
        //        {
        //            trans.Rollback();//交易Rollback
        //            throw ex;
        //        }
        //    }
        //}

        //#endregion

        //#endregion


        //#region 大量寫入 Modify by Shadow at 2014-08-02

        ///// <summary>
        ///// 大量批次新增(限對象為Sql Server)，使用SqlHelper的預設連線
        ///// </summary>
        ///// <param name="dtSource">資料來源的DataTable</param>
        ///// <param name="destDataTableName">目標資料庫的表格名稱</param>
        ///// <param name="optionSqlBulk">匯入時的選項</param>
        //public static void SqlBulkCopyFromDataTable(DataTable dtSource, string destDBTableName, SqlBulkCopyOptions optionSqlBulk = SqlBulkCopyOptions.Default)
        //{
        //    SqlBulkCopyFromDataTable(connectionString, dtSource, destDBTableName, optionSqlBulk);

        //}

        ///// <summary>
        ///// 大量批次新增(限對象為Sql Server)，工程師自行指定DB連線
        ///// </summary>
        ///// <param name="connectionString">DB連線字串</param>
        ///// <param name="dtSource">資料來源的DataTable</param>
        ///// <param name="destDataTableName">目標資料庫的表格名稱</param>
        ///// <param name="optionSqlBulk">匯入時的選項</param>
        //public static void SqlBulkCopyFromDataTable(string connectionString, DataTable dtSource, string destDBTableName, SqlBulkCopyOptions optionSqlBulk = SqlBulkCopyOptions.Default)
        //{

        //    if (string.IsNullOrEmpty(destDBTableName))
        //    {
        //        throw new Exception("缺少目標資料庫的表格名稱");
        //    }

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();//開啟連線
        //        //開始交易
        //        SqlTransaction tran = conn.BeginTransaction();
        //        //宣告SqlBulkCopy  
        //        using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn, optionSqlBulk, tran))
        //        {
        //            //設定一個批次量寫入多少筆資料       
        //            sqlBC.BatchSize = 1000;
        //            //設定逾時的秒數        
        //            sqlBC.BulkCopyTimeout = 30;
        //            //設定要寫入的資料表          
        //            sqlBC.DestinationTableName = destDBTableName;
        //            foreach (DataColumn dataCol in dtSource.Columns)
        //            {
        //                //對應資料行         
        //                sqlBC.ColumnMappings.Add(dataCol.ColumnName, dataCol.ColumnName);
        //            }//end foreach
        //            //開始寫入新增 
        //            try
        //            {
        //                sqlBC.WriteToServer(dtSource);
        //                tran.Commit();//交易提交
        //            }
        //            catch (SqlException ex)
        //            {
        //                tran.Rollback();//交易Rollback
        //                throw ex;
        //            }

        //        }//end using 
        //    }//end using 

        //}
        //#endregion



        //#region 離線資料庫存取

        //#region GetDataTable

        ///// <summary>
        ///// 取得Select指令回傳的結果集，工程師自行指定DB連線字串
        ///// </summary>
        ///// <param name="connecttionString">DB連線字串</param>
        ///// <param name="cmdTye">DbCommand類型 (CommandType.StoredProcedure或CommandType.Text)</param>
        ///// <param name="cmdText">預存程式名稱 或 T-SQL 语句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>取得Select指令回傳的結果集，型別DataTable</returns>
        //public static DataTable GetDataTable(string connecttionString, CommandType cmdTye, string cmdText, DbParameter[] commandParameters)
        //{

        //    DataTable dt = new DataTable();
        //    using (DbConnection conn = dbProviderFactory.CreateConnection())
        //    {
        //        conn.ConnectionString = connectionString;//連線字串
        //        DbCommand cmd = conn.CreateCommand();
        //        //↓不寫這行的話，由實作的Provider決定數值，OleDb、Odbc、SqlClient預設30秒，OracleClient為0不逾時
        //        cmd.CommandTimeout = 0;//執行SQL指令時間，0為不逾時
        //        PrepareCommand(cmd, conn, null, cmdTye, cmdText, commandParameters);
        //        DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter();//DbDataAdapter自己會開/關DB連線
        //        adapter.SelectCommand = cmd;
        //        adapter.Fill(dt);
        //        cmd.Parameters.Clear();
        //        conn.Close();//自關連線
        //    }
        //    return dt;
        //}

        ///// <summary>
        ///// 取得Select指令回傳的結果集，使用SqlHelper預設DB連線字串
        ///// </summary>
        ///// <param name="cmdTye">DbCommand類型 (CommandType.StoredProcedure或CommandType.Text)</param>
        ///// <param name="cmdText">預存程式名稱 或 T-SQL 语句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>取得Select指令回傳的結果集，型別DataTable</returns>
        //public static DataTable GetDataTable(CommandType cmdTye, string cmdText, DbParameter[] commandParameters)
        //{
        //    return GetDataTable(connectionString, cmdTye, cmdText, commandParameters);
        //}

        ///// <summary>
        ///// 取得Select指令回傳的結果集，使用SqlHelper預設DB連線字串、CommandType.Text
        ///// </summary>
        ///// <param name="cmdText">T-SQL 语句</param>
        ///// <param name="commandParameters">使用到的參數集合</param>
        ///// <returns>取得Select指令回傳的結果集，型別DataTable</returns>
        //public static DataTable GetDataTableText(string cmdText, DbParameter[] commandParameters)
        //{
        //    return GetDataTable(CommandType.Text, cmdText, commandParameters);
        //}
        //#endregion

        //#endregion



        //Database connection strings
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Forum_DB_ConnectionString"].ConnectionString;

        //public static readonly string ConnectionStringLocalTransaction = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        //public static readonly string ConnectionStringInventoryDistributedTransaction = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        //public static readonly string ConnectionStringOrderDistributedTransaction = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        //public static readonly string ConnectionStringProfile = ConfigurationManager.ConnectionStrings["local"].ConnectionString;

        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to close the connection throw code,
            // because no datareader will exist, hence the commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to close the connection throw code,
            // because no datareader will exist, hence the commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter()
                {
                    SelectCommand = cmd
                };
                sda.Fill(ds, "biao");
                conn.Close();
                cmd.Parameters.Clear();
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }


    }
}