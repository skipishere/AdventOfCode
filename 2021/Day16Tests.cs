using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace AdventOfCode2021
{
    [TestClass]
    public class Day16Tests
    {
        [TestMethod]
        [DataRow("D2FE28", 2021)]
        public void LiteralValueHex(string input, int expected)
        {
            var data = new List<string> { input };

            var day = new Day16(data);

            Assert.AreEqual(1, day.packets.Count);

            var packet = day.packets[0];
            Assert.AreEqual(6, packet.Version);
            Assert.AreEqual(4, packet.Type);
            Assert.AreEqual(expected, packet.Value);
        }

        [TestMethod]
        [DataRow("11010001010", 10)]
        [DataRow("0101001000100100", 20)]
        [DataRow("01010000001", 1)]
        [DataRow("10010000010", 2)]
        [DataRow("00110000011", 3)]
        public void LiteralValueBinary(string input, int expected)
        {
            var binary = new StringBuilder(input);

            var position = 6;
            var packet = Day16.GetLiteral(binary, 0, ref position);

            Assert.AreEqual(expected, packet.Value);
            Assert.AreEqual(input.Length, position);
        }

        [TestMethod]
        public void OperatorExampleLengthType0()
        {
            var data = new List<string> { "38006F45291200" };

            var day = new Day16(data);

            Assert.AreEqual(4, day.packets.Count);

            Assert.AreEqual(10, day.packets[1].Value);
            Assert.AreEqual(20, day.packets[2].Value);
        }

        [TestMethod]
        public void OperatorExampleLengthType1()
        {
            var data = new List<string> { "EE00D40C823060" };

            var day = new Day16(data);

            Assert.AreEqual(4, day.packets.Count);

            Assert.AreEqual(1, day.packets[1].Value);
            Assert.AreEqual(2, day.packets[2].Value);
            Assert.AreEqual(3, day.packets[3].Value);
        }

        [TestMethod]
        [DataRow("8A004A801A8002F478", 4, 16)]
        [DataRow("620080001611562C8802118E34", 5, 12)]
        [DataRow("C0015000016115A2E0802F182340", 5, 23)]
        [DataRow("A0016C880162017C3686B18A3D4780", 7, 31)]
        public void VersionSum(string input, int expectedPacketCount, int expectedSumVersion)
        {
            var data = new List<string> { input };

            var day = new Day16(data);

            //Assert.AreEqual(expectedPacketCount, day.packets.Count);
            Assert.AreEqual(expectedSumVersion, day.packets.Sum(c => c.Version));
        }
    }
}
