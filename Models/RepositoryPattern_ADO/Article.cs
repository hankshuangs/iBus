using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RepositoryPattern_ADO
{
    /// <summary>
    /// 宣告公開類別。(文章資料模型)
    /// </summary>
    /// <remarks>
    /// 這裡面資料模型屬性欄位主要是配合 ADO.NET 裡面的資料庫欄位使用，
    /// 所以屬性命名會跟資料庫的欄位名稱一樣，以便未來轉換成 EntityFramework 時欄位名稱可以直接套用。
    /// 所以這裡的屬性名稱尾巴就不使用 _P 的命名規則。
    /// </remarks>
    public class Article
    {
        /// <summary>
        /// 宣告公開整數屬性。(編號)
        /// </summary>
        public int NO_F { get; set; }

        /// <summary>
        /// 宣告公開字串屬性。(文章標題)
        /// </summary>
        public string Title_F { get; set; }

        /// <summary>
        /// 宣告公開字串屬性。(文章內容)
        /// </summary>
        public string Content_F { get; set; }

        /// <summary>
        /// 宣告公開時間屬性。(加入時間)
        /// </summary>
        public DateTime JoinTime_F { get; set; }

        /// <summary>
        /// 宣告公開整數屬性。(點閱次數)
        /// </summary>
        public int ViewCount_F { get; set; }

        /// <summary>
        /// 宣告公開整數屬性。(擁有者編號)
        /// </summary>
        public int AccountNO_F { get; set; }

        /// <summary>
        /// 宣告公開字串屬性。(擁有者名字)
        /// </summary>
        public string Name_F { get; set; }
    }
}
