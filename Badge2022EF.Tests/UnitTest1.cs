using Badge2022EF.WebApi.Controllers.RSA;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Badge2022EF.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            [TestMethod]
            void TestMethod1()
            {
                //Arrange
                string startString = "Voila...";
                var dc = new RsaHelper();
                string expectedResult = startString;

                //Act
                string result = dc.Decrypt(dc.Encrypt(startString));

                //Assert
                Assert.AreEqual(expectedResult, result);
            }

        }
    }
}