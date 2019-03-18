using System;
using System.Security.Cryptography;
using System.Text;

namespace Orz.Common.Extensions
{
	/// <summary>
	/// <see cref="RSA"/>扩展方法
	/// </summary>
	public static class RsaExtension
	{
#if IS_NETCOREAPP1
		/// <summary>
		/// 返回<paramref name="rsa"/>的密钥的Xml字符串
		/// </summary>
		/// <param name="rsa"></param>
		/// <param name="includePrivateParameters">true表示同时包含RSA公钥和私钥；false表示仅包含公钥</param>
		/// <returns></returns>
		public static string ToXmlString(this RSA rsa, bool includePrivateParameters)
		{
			return rsa.ExportParameters(includePrivateParameters).ToXmlString(includePrivateParameters);
		}
#endif

		/// <summary>
		/// 返回<paramref name="parameters"/>的Xml字符串
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="includePrivateParameters">true表示同时包含RSA公钥和私钥；false表示仅包含公钥</param>
		/// <returns></returns>
		public static string ToXmlString(this RSAParameters parameters, bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<RSAKeyValue>");
			stringBuilder.Append("<Modulus>" + Convert.ToBase64String(parameters.Modulus) + "</Modulus>");
			stringBuilder.Append("<Exponent>" + Convert.ToBase64String(parameters.Exponent) + "</Exponent>");
			if (includePrivateParameters)
			{
				stringBuilder.Append("<P>" + Convert.ToBase64String(parameters.P) + "</P>");
				stringBuilder.Append("<Q>" + Convert.ToBase64String(parameters.Q) + "</Q>");
				stringBuilder.Append("<DP>" + Convert.ToBase64String(parameters.DP) + "</DP>");
				stringBuilder.Append("<DQ>" + Convert.ToBase64String(parameters.DQ) + "</DQ>");
				stringBuilder.Append("<InverseQ>" + Convert.ToBase64String(parameters.InverseQ) + "</InverseQ>");
				stringBuilder.Append("<D>" + Convert.ToBase64String(parameters.D) + "</D>");
			}
			stringBuilder.Append("</RSAKeyValue>");
			return stringBuilder.ToString();
		}
	}
}
