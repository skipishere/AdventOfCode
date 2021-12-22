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

            Assert.AreEqual(6, day.OuterPacket.Version);
            Assert.AreEqual(4, day.OuterPacket.Type);
            Assert.AreEqual(expected, day.OuterPacket.Value);
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

            Assert.AreEqual(10, day.OuterPacket.Packets[0].Value);
            Assert.AreEqual(20, day.OuterPacket.Packets[1].Value);
        }

        [TestMethod]
        public void OperatorExampleLengthType1()
        {
            var data = new List<string> { "EE00D40C823060" };

            var day = new Day16(data);

            Assert.AreEqual(1, day.OuterPacket.Packets[0].Value);
            Assert.AreEqual(2, day.OuterPacket.Packets[1].Value);
            Assert.AreEqual(3, day.OuterPacket.Packets[2].Value);
        }

        [TestMethod]
        [DataRow("8A004A801A8002F478", 16)]
        [DataRow("620080001611562C8802118E34", 12)]
        [DataRow("C0015000016115A2E0802F182340", 23)]
        [DataRow("A0016C880162017C3686B18A3D4780", 31)]
        public void VersionSum(string input, int expectedSumVersion)
        {
            var data = new List<string> { input };

            var day = new Day16(data);

            Assert.AreEqual(expectedSumVersion, day.OuterPacket.VersionSum());
        }

        [TestMethod]
        [DataRow("C200B40A82", 3)]
        [DataRow("04005AC33890", 54)]
        [DataRow("880086C3E88112", 7)]
        [DataRow("CE00C43D881120", 9)]
        [DataRow("D8005AC2A8F0", 1)]
        [DataRow("F600BC2D8F", 0)]
        [DataRow("9C005AC2F8F0", 0)]
        [DataRow("9C0141080250320F1802104A08", 1)]
        public void Value(string input, int expectedValue)
        {
            var data = new List<string> { input };

            var day = new Day16(data);

            Assert.AreEqual(expectedValue, day.OuterPacket.Value);
        }
    }
}
