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

        private string sha256_lower = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4";
        private string sha256_upper = "03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4";

        private string sha384_lower = "504f008c8fcf8b2ed5dfcde752fc5464ab8ba064215d9c5b5fc486af3d9ab8c81b14785180d2ad7cee1ab792ad44798c";
        private string sha384_upper = "504F008C8FCF8B2ED5DFCDE752FC5464AB8BA064215D9C5B5FC486AF3D9AB8C81B14785180D2AD7CEE1AB792AD44798C";

        private string sha512_lower = "d404559f602eab6fd602ac7680dacbfaadd13630335e951f097af3900e9de176b6db28512f2e000b9d04fba5133e8b1c6e8df59db3a8ab9d60be4b97cc9e81db";
        private string sha512_upper = "D404559F602EAB6FD602AC7680DACBFAADD13630335E951F097AF3900E9DE176B6DB28512F2E000B9D04FBA5133E8B1C6E8DF59DB3A8AB9D60BE4B97CC9E81DB";

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

        [Fact]
        public void Sha256()
        {
            Assert.Equal(sha256_lower, EncryptHelper.Sha256(input, false));
            Assert.Equal(sha256_upper, EncryptHelper.Sha256(input, true));
        }

        [Fact]
        public void IsSha256Match()
        {
            Assert.True(EncryptHelper.IsSha256Match(input, sha256_lower));
            Assert.True(EncryptHelper.IsSha256Match(input, sha256_upper));
        }

        [Fact]
        public void Sha384()
        {
            Assert.Equal(sha384_lower, EncryptHelper.Sha384(input, false));
            Assert.Equal(sha384_upper, EncryptHelper.Sha384(input, true));
        }

        [Fact]
        public void IsSha384Match()
        {
            Assert.True(EncryptHelper.IsSha384Match(input, sha384_lower));
            Assert.True(EncryptHelper.IsSha384Match(input, sha384_upper));
        }

        [Fact]
        public void Sha512()
        {
            Assert.Equal(sha512_lower, EncryptHelper.Sha512(input, false));
            Assert.Equal(sha512_upper, EncryptHelper.Sha512(input, true));
        }

        [Fact]
        public void IsSha512Match()
        {
            Assert.True(EncryptHelper.IsSha512Match(input, sha512_lower));
            Assert.True(EncryptHelper.IsSha512Match(input, sha512_upper));
        }
    }
}
