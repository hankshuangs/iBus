using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RepositoryPattern_ADO
{
    class Member
    {
        /// <summary>
        /// 宣告公開整數屬性。(編號)
        /// </summary>
        public int NO_F { get; set; }

        /// <summary>
        /// 宣告公開字串屬性。(用戶帳號)
        /// </summary>
        public string Account_F { get; set; }

        /// <summary>
        /// 宣告公開字串屬性。(用戶密碼)
        /// </summary>
        public string Password_F { get; set; }

        /// <summary>
        /// 宣告公開字串屬性。(用戶名字)
        /// </summary>
        public string Name_F { get; set; }

        /// <summary>
        /// 宣告公開字串屬性。(電子信箱)
        /// </summary>
        public string Email_F { get; set; }

        /// <summary>
        /// 宣告公開字串屬性。(註冊驗証碼)
        /// </summary>
        public string AuthCode_F { get; set; }

        /// <summary>
        /// 宣告公開布林屬性。(是否為管理員)
        /// </summary>
        public Boolean IsAdmin_F { get; set; }
    }
}
