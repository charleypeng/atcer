// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DataEncryption;

namespace ATCer.Authorization.Core
{
    /// <summary>
    /// 加密密码
    /// </summary>
    public class PasswordEncryptHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string Encrypt (string password,string encryptKey)
        {
            var encryptedPassword = MD5Encryption.Encrypt(password+ encryptKey);
            return encryptedPassword;
        }
    }
}
