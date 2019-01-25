using System;
using System.Security.Cryptography;
using System.Text;
using Orz.Common.Extensions;

namespace Orz.Common.Helpers
{
	/// <summary>
	/// 加密辅助类
	/// </summary>
	/// <remarks>
	/// HashAlgorithm：
	/// https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.hashalgorithm?view=netframework-4.7.2
	/// SymmetricAlgorithm：
	/// https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.symmetricalgorithm?view=netframework-4.7.2
	/// AsymmetricAlgorithm：
	/// https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.asymmetricalgorithm?view=netframework-4.7.2
	/// </remarks>
	public static class CryptographyHelper
	{
		#region 创建加密类

		/// <summary>
		/// 根据<paramref name="assemblyQualifiedTypeName"/>创建<typeparamref name="T"/>的实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="assemblyQualifiedTypeName">类型的程序集限定名</param>
		/// <returns></returns>
		public static T CreateAlgo<T>(string assemblyQualifiedTypeName) where T : class
		{
			return CreateAlgo<T>(Type.GetType(assemblyQualifiedTypeName));
		}

		/// <summary>
		/// 根据<paramref name="type"/>创建<typeparamref name="T"/>的实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type">类型</param>
		/// <returns></returns>
		public static T CreateAlgo<T>(Type type) where T : class
		{
			return Activator.CreateInstance(type) as T;
		}

		#region 哈希加密
		/// <summary>
		/// 创建MD5CryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static HashAlgorithm CreateHashAlgoMd5()
		{
			return new MD5CryptoServiceProvider();
		}

		/// <summary>
		/// 创建SHA1CryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static HashAlgorithm CreateHashAlgoSha1()
		{
			return new SHA1CryptoServiceProvider();
		}

		/// <summary>
		/// 创建SHA256CryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static HashAlgorithm CreateHashAlgoSha256()
		{
			return new SHA256CryptoServiceProvider();
		}

		/// <summary>
		/// 创建SHA384CryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static HashAlgorithm CreateHashAlgoSha384()
		{
			return new SHA384CryptoServiceProvider();
		}

		/// <summary>
		/// 创建SHA512CryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static HashAlgorithm CreateHashAlgoSha512()
		{
			return new SHA512CryptoServiceProvider();
		}
		#endregion

		#region 对称加密
		/// <summary>
		/// 创建AesCryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static SymmetricAlgorithm CreateSymmAlgoAes()
		{
			return new AesCryptoServiceProvider();
		}

		/// <summary>
		/// 创建DESCryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static SymmetricAlgorithm CreateSymmAlgoDes()
		{
			return new DESCryptoServiceProvider();
		}

		/// <summary>
		/// 创建RC2CryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static SymmetricAlgorithm CreateSymmAlgoRc2()
		{
			return new RC2CryptoServiceProvider();
		}

		/// <summary>
		/// 创建TripleDESCryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static SymmetricAlgorithm CreateSymmAlgoTripleDes()
		{
			return new TripleDESCryptoServiceProvider();
		}
		#endregion

		#region 非对称加密

		#region DSACryptoServiceProvider
		/// <summary>
		/// 创建DSACryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static AsymmetricAlgorithm CreateAsymmAlgoDsa()
		{
			return new DSACryptoServiceProvider();
		}

		/// <summary>
		/// 创建DSACryptoServiceProvider
		/// </summary>
		/// <param name="dwKeySize">密钥的大小（以位为单位）</param>
		/// <returns></returns>
		public static AsymmetricAlgorithm CreateAsymmAlgoDsa(int dwKeySize)
		{
			return new DSACryptoServiceProvider(dwKeySize);
		}

		/// <summary>
		/// 创建DSACryptoServiceProvider
		/// </summary>
		/// <param name="parameters">要传递给加密服务提供程序 (CSP) 的参数</param>
		/// <returns></returns>
		public static AsymmetricAlgorithm CreateAsymmAlgoDsa(CspParameters parameters)
		{
			return new DSACryptoServiceProvider(parameters);
		}

		/// <summary>
		/// 创建DSACryptoServiceProvider
		/// </summary>
		/// <param name="dwKeySize">密钥的大小（以位为单位）</param>
		/// <param name="parameters">要传递给加密服务提供程序 (CSP) 的参数</param>
		/// <returns></returns>
		public static AsymmetricAlgorithm CreateAsymmAlgoDsa(int dwKeySize, CspParameters parameters)
		{
			return new DSACryptoServiceProvider(dwKeySize, parameters);
		}
		#endregion

		#region RSACryptoServiceProvider
		/// <summary>
		/// 创建RSACryptoServiceProvider
		/// </summary>
		/// <returns></returns>
		public static AsymmetricAlgorithm CreateAsymmAlgoRsa()
		{
			return new RSACryptoServiceProvider();
		}

		/// <summary>
		/// 创建RSACryptoServiceProvider
		/// </summary>
		/// <param name="dwKeySize">密钥的大小（以位为单位）</param>
		/// <returns></returns>
		public static AsymmetricAlgorithm CreateAsymmAlgoRsa(int dwKeySize)
		{
			return new RSACryptoServiceProvider(dwKeySize);
		}

		/// <summary>
		/// 创建RSACryptoServiceProvider
		/// </summary>
		/// <param name="parameters">要传递给加密服务提供程序 (CSP) 的参数</param>
		/// <returns></returns>
		public static AsymmetricAlgorithm CreateAsymmAlgoRsa(CspParameters parameters)
		{
			return new RSACryptoServiceProvider(parameters);
		}

		/// <summary>
		/// 创建RSACryptoServiceProvider
		/// </summary>
		/// <param name="dwKeySize">密钥的大小（以位为单位）</param>
		/// <param name="parameters">要传递给加密服务提供程序 (CSP) 的参数</param>
		/// <returns></returns>
		public static AsymmetricAlgorithm CreateAsymmAlgoRsa(int dwKeySize, CspParameters parameters)
		{
			return new RSACryptoServiceProvider(dwKeySize, parameters);
		}
		#endregion

		#endregion

		#endregion

		#region 加密解密、签名校验

		#region 哈希
		/// <summary>
		/// 文本哈希加密
		/// </summary>
		/// <param name="hashAlgorithm">加密算法</param>
		/// <param name="dataToHash">加密文本</param>
		/// <param name="encoding">加密时的文本编码，为null时使用<see cref="UTF8Encoding()"/>构建的实例</param>
		/// <param name="toUpper">是否转大写</param>
		/// <returns></returns>
		public static string Encrypt(HashAlgorithm hashAlgorithm, string dataToHash, Encoding encoding = null, bool toUpper = true)
		{
			encoding = encoding ?? new UTF8Encoding();
			byte[] data = encoding.GetBytes(dataToHash);
			byte[] result = hashAlgorithm.ComputeHash(data);
			return result.ToHex(toUpper);
		}

		/// <summary>
		/// 文本哈希校验
		/// </summary>
		/// <param name="hashAlgorithm">加密算法</param>
		/// <param name="hashedText">加密后的文本</param>
		/// <param name="unhashedText">加密前的文本</param>
		/// <param name="encoding">加密时的文本编码，为null时使用<see cref="UTF8Encoding()"/>构建的实例</param>
		/// <returns></returns>
		public static bool IsHashMatch(HashAlgorithm hashAlgorithm, string hashedText, string unhashedText, Encoding encoding = null)
		{
			string hashedTextToCompare = Encrypt(hashAlgorithm, unhashedText, encoding);
			return string.Compare(hashedText, hashedTextToCompare, true) == 0;
		}
		#endregion

		#region 对称

		#endregion

		#region 非对称

		#endregion

		#endregion
	}
}
