using System.Security.Cryptography;
using System.Text;
using Orz.Common.Extensions;

namespace Orz.Common.Helpers
{
    /// <summary>
    /// 常用加密算法
    /// </summary>
    public static class EncryptHelper
    {
        #region Md5
        /// <summary>
        /// 获取32位md5加密字符串
        /// </summary>
        /// <param name="dataToMd5">待加密字符串</param>
        /// <param name="toUpper">是否转大写</param>
        /// <returns></returns>
        public static string Md5With32Bits(string dataToMd5, bool toUpper = true)
        {
            return Encrypt(new MD5CryptoServiceProvider(), dataToMd5, toUpper);
        }

        /// <summary>
        /// 获取16位md5加密字符串
        /// </summary>
        /// <param name="dataToMd5">待加密字符串</param>
        /// <param name="toUpper">是否转大写</param>
        /// <returns></returns>
        public static string Md5With16Bits(string dataToMd5, bool toUpper = true)
        {
            return Md5With32Bits(dataToMd5, toUpper).Substring(8, 16);
        }

        /// <summary>
        /// 32位md5校验
        /// </summary>
        /// <param name="input">原始字符串</param>
        /// <param name="md5">32位md5</param>
        /// <returns></returns>
        public static bool IsMd5With32BitsMatch(string input, string md5)
        {
            string md5ToCompare = Md5With32Bits(input);
            return string.Compare(md5, md5ToCompare, true) == 0;
        }

        /// <summary>
        /// 16位md5校验
        /// </summary>
        /// <param name="input">原始字符串</param>
        /// <param name="md5">16位md5</param>
        /// <returns></returns>
        public static bool IsMd5With16BitsMatch(string input, string md5)
        {
            string md5ToCompare = Md5With16Bits(input);
            return string.Compare(md5, md5ToCompare, true) == 0;
        }
        #endregion

        #region Sha1
        /// <summary>
        /// 获取sha1加密字符串
        /// </summary>
        /// <param name="dataToSha1">待加密字符串</param>
        /// <param name="toUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha1(string dataToSha1, bool toUpper = true)
        {
            return Encrypt(new SHA1CryptoServiceProvider(), dataToSha1, toUpper);
        }

        /// <summary>
        /// sha1校验
        /// </summary>
        /// <param name="input">原始字符串</param>
        /// <param name="sha1">sha1字符串</param>
        /// <returns></returns>
        public static bool IsSha1Match(string input, string sha1)
        {
            string sha1ToCompare = Sha1(input);
            return string.Compare(sha1, sha1ToCompare, true) == 0;
        }
        #endregion

        #region Sha256
        /// <summary>
        /// 获取sha256加密字符串
        /// </summary>
        /// <param name="dataToSha256">待加密字符串</param>
        /// <param name="toUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha256(string dataToSha256, bool toUpper = true)
        {
            return Encrypt(new SHA256CryptoServiceProvider(), dataToSha256, toUpper);
        }

        /// <summary>
        /// sha256校验
        /// </summary>
        /// <param name="input">原始字符串</param>
        /// <param name="sha256">sha256字符串</param>
        /// <returns></returns>
        public static bool IsSha256Match(string input, string sha256)
        {
            string sha256ToCompare = Sha256(input);
            return string.Compare(sha256, sha256ToCompare, true) == 0;
        }
        #endregion

        #region Sha384
        /// <summary>
        /// 获取sha384加密字符串
        /// </summary>
        /// <param name="dataToSha384">待加密字符串</param>
        /// <param name="toUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha384(string dataToSha384, bool toUpper = true)
        {
            return Encrypt(new SHA384CryptoServiceProvider(), dataToSha384, toUpper);
        }

        /// <summary>
        /// sha384校验
        /// </summary>
        /// <param name="input">原始字符串</param>
        /// <param name="sha384">sha384字符串</param>
        /// <returns></returns>
        public static bool IsSha384Match(string input, string sha384)
        {
            string sha384ToCompare = Sha384(input);
            return string.Compare(sha384, sha384ToCompare, true) == 0;
        }
        #endregion

        #region Sha512
        /// <summary>
        /// 获取sha512加密字符串
        /// </summary>
        /// <param name="dataToSha512">待加密字符串</param>
        /// <param name="toUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha512(string dataToSha512, bool toUpper = true)
        {
            return Encrypt(new SHA512CryptoServiceProvider(), dataToSha512, toUpper);
        }

        /// <summary>
        /// sha512校验
        /// </summary>
        /// <param name="input">原始字符串</param>
        /// <param name="sha512">sha512字符串</param>
        /// <returns></returns>
        public static bool IsSha512Match(string input, string sha512)
        {
            string sha512ToCompare = Sha512(input);
            return string.Compare(sha512, sha512ToCompare, true) == 0;
        }
        #endregion

        //public static string EncryptFile(HashAlgorithm hashAlgorithm, string filePath)
        //{
        //    Stream dataToHash = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        //    byte[] result = hashAlgorithm.ComputeHash(dataToHash);
        //    return result.ToHex();
        //}

        //public static bool IsFileHashMatch(HashAlgorithm hashAlgorithm, string filePath, string hashedText)
        //{
        //    return string.Compare(EncryptFile(hashAlgorithm, filePath), hashedText, true) == 0;
        //}

        private static string Encrypt(HashAlgorithm hashAlgorithm, string dataToHash, bool toUpper)
        {
            return hashAlgorithm.ComputeHash(new UTF8Encoding().GetBytes(dataToHash)).ToHex(toUpper);
        }
    }
}
