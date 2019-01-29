using Orz.Common.Helpers;
using Xunit;

namespace Orz.Common.Test.Helpers
{
	public class EncryptHelperTest
	{
		[Fact]
		public void Md5()
		{
			string input = "1234";
			Assert.Equal("52d04dc20036dbd8", EncryptHelper.Md5With16Bits(input, false));
			Assert.Equal("52D04DC20036DBD8", EncryptHelper.Md5With16Bits(input, true));
			Assert.Equal("81dc9bdb52d04dc20036dbd8313ed055", EncryptHelper.Md5With32Bits(input, false));
			Assert.Equal("81DC9BDB52D04DC20036DBD8313ED055", EncryptHelper.Md5With32Bits(input, true));
		}

		[Fact]
		public void Md5IsMatch()
		{
			string input = "1234";
			Assert.True(EncryptHelper.IsMd5With16BitsMatch(input, "52d04dc20036dbd8"));
			Assert.True(EncryptHelper.IsMd5With16BitsMatch(input, "52D04DC20036DBD8"));
			Assert.True(EncryptHelper.IsMd5With32BitsMatch(input, "81dc9bdb52d04dc20036dbd8313ed055"));
			Assert.True(EncryptHelper.IsMd5With32BitsMatch(input, "81DC9BDB52D04DC20036DBD8313ED055"));
		}
	}
}
