using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.RepositoryPattern_ADO;

/// <summary>
/// 定義 IRepository 倉儲介面
/// </summary>
namespace Models.Interface
{
    public interface IRepository_Article
    {
        #region Article_Cs

        #region 顯示資料操作
        /// <summary>
        /// 列表全部文章方法
        /// </summary>
        IList<Article> ListData_Md();

        /// <summary>
        /// 取得單一文章依據編號方法
        /// </summary>
        /// <param name="intNO_Val">編號</param>
        Article GetDataByNO_Md(int intNO_Val);

        /// <summary>
        /// 取得全部文章筆數方法
        /// </summary>
        int GetDataCount_Md();

        /// <summary>
        /// 取得文章最新編號方法
        /// </summary>
        int GetMaxDataNO_Md();

        #endregion


        #region 異動資料操作

        /// <summary>
        /// 插入文章方法
        /// </summary>
        /// <param name="objArticle_Val">文章資料模型</param>
        bool Insert_Md(Article objArticle_Val);

        /// <summary>
        /// 更新文章方法
        /// </summary>
        /// <param name="objArticle_Val">文章資料模型</param>
        bool Update_Md(Article objArticle_Val);

        /// <summary>
        /// 刪除文章方法
        /// </summary>
        /// <param name="intNO_Val">編號</param>
        bool Delete_Md(int intNO_Val);

        #endregion

        #endregion
    }

}
