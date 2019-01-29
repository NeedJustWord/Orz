using Orz.Common.Helpers;
using Xunit;

namespace Orz.Common.Test.Helpers
{
	public class EncryptHelperTest
	{
		private string input = "1234";

		private string md5_16_lower = "52d04dc20036dbd8";
		private string md5_16_upper = "52D04DC20036DBD8";
		private string md5_32_lower = "81dc9bdb52d04dc20036dbd8313ed055";
		private string md5_32_upper = "81DC9BDB52D04DC20036DBD8313ED055";

		private string sha1_lower = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220";
		private string sha1_upper = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220";

		[Fact]
		public void Md5()
		{
			Assert.Equal(md5_16_lower, EncryptHelper.Md5With16Bits(input, false));
			Assert.Equal(md5_16_upper, EncryptHelper.Md5With16Bits(input, true));
			Assert.Equal(md5_32_lower, EncryptHelper.Md5With32Bits(input, false));
			Assert.Equal(md5_32_upper, EncryptHelper.Md5With32Bits(input, true));
		}

		[Fact]
		public void IsMd5Match()
		{
			Assert.True(EncryptHelper.IsMd5With16BitsMatch(input, md5_16_lower));
			Assert.True(EncryptHelper.IsMd5With16BitsMatch(input, md5_16_upper));
			Assert.True(EncryptHelper.IsMd5With32BitsMatch(input, md5_32_lower));
			Assert.True(EncryptHelper.IsMd5With32BitsMatch(input, md5_32_upper));
		}

		[Fact]
		public void Sha1()
		{
			Assert.Equal(sha1_lower, EncryptHelper.Sha1(input, false));
			Assert.Equal(sha1_upper, EncryptHelper.Sha1(input, true));
		}

		[Fact]
		public void IsSha1Match()
		{
			Assert.True(EncryptHelper.IsSha1Match(input, sha1_lower));
			Assert.True(EncryptHelper.IsSha1Match(input, sha1_upper));
		}
	}
}
