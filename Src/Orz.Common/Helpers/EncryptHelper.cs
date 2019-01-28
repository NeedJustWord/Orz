using System.Text;

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
			return CryptographyHelper.Encrypt(CryptographyHelper.CreateHashAlgoMd5(), dataToMd5, new UTF8Encoding(), toUpper);
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

		//public static string EncryptFile(HashAlgorithm hashAlgorithm, string filePath)
		//{
		//	Stream dataToHash = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
		//	byte[] result = hashAlgorithm.ComputeHash(dataToHash);
		//	return result.ToHex();
		//}

		//public static bool IsFileHashMatch(HashAlgorithm hashAlgorithm, string filePath, string hashedText)
		//{
		//	return string.Compare(EncryptFile(hashAlgorithm, filePath), hashedText, true) == 0;
		//}
	}
}
