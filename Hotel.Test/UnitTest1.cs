using Hotel.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hotel.Test
{
    [TestClass]
    public class UnitTest1
    {
        /*
        50,Scandic Rubinen,20
        50,Scandic Opalen,20
        60,Scandic Backadal,5
        70,Scandic Helsingborg North,20
        */
        [TestMethod]
        public void TestMethod1()
        {
            var parser = new Parse(@"C:\Project\Hotel\Hotel\Hotel.Test\Import");
            var list = parser.ParseScandicfile();

            Assert.AreEqual(50, list[0].AreaId);
        }

        [TestMethod]
        public void Parse_BestWesternHotel_And_Expect()
        {
            var parser = new Parse(@"C:\Project\Hotel\Hotel\Hotel.Test\Import");
            var list = parser.ParseBestWesternfile();

            Assert.AreEqual("Hotell Eggers", list[0].Name);
        }
    }
}
