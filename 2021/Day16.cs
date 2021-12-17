using System;
using System.Text;

namespace AdventOfCode2021
{
    internal class Day16 : Day
    {
        public override string Name => "Day 16: Packet Decoder";

        private List<Packet> packets = new();

        public Day16()
        {
            var input = this.InputString();

            var binaryBuilder = new StringBuilder(input.Count() * 4);
            foreach (var character in input.First())
            {
                var base10 = Convert.ToInt32(character.ToString(), 16);
                var binary = Convert.ToString(base10, 2).PadLeft(4, '0');
                binaryBuilder.Append(binary);
            }

            while (binaryBuilder.Length > 0)
            {
                var version = Convert.ToInt32(binaryBuilder.ToString(0, 3), 2);
                var type = Convert.ToInt32(binaryBuilder.ToString(3, 3), 2);

                string data = string.Empty;
                var totalDataLength = 6;
                switch (type)
                {
                    case 4:
                        var dataBuilder = new StringBuilder();
                        var keepGoing = true;
                        while (keepGoing)
                        {
                            keepGoing = binaryBuilder[totalDataLength] == '1';
                            dataBuilder.Append(binaryBuilder.ToString(totalDataLength + 1, 4));
                            totalDataLength += 5;
                        }
                        
                        data = dataBuilder.ToString();
                        break;
                    default:
                        var lengthTypeId = binaryBuilder.ToString(6, 1);
                        totalDataLength += 1;

                        if (lengthTypeId == "0")
                        {
                            var dataLength = Convert.ToInt32(binaryBuilder.ToString(7, 15), 2);
                            data = binaryBuilder.ToString(7 + 15, dataLength);
                            totalDataLength += 15 + dataLength;
                        }
                        else
                        {
                            var subPackets = Convert.ToInt32(binaryBuilder.ToString(7, 11), 2);
                            for (int i = 0; i < subPackets; i++)
                            {
                                data += binaryBuilder.ToString(7 + 11 + i * 11, 11);
                            }
                            totalDataLength += 11 + 11 * subPackets;
                        }
                        
                        break;
                }

                packets.Add(new Packet(version, type, data));
                binaryBuilder.Remove(0, totalDataLength);
            }

        }

       
        public override void FirstAnswer()
        {
            Console.WriteLine(packets.Sum(c=> c.Version));
        }

        public override void SecondAnswer()
        {
            Console.WriteLine(2);
        }

        private class Packet
        {
            public int Version { get; private set; }

            public int Type { get; private set; }

            public int Value { get; private set; }

            public Packet(int version, int type, string data)
            {
                this.Version = version;
                this.Type = type;

                if (this.Type == 4)
                {
                    Value = Convert.ToInt32(data, 2);
                }else if (data.Length == 15)
                {
                    var value1 = Convert.ToInt32(data.Substring(0, 11), 2);
                    var value2 = Convert.ToInt32(data.Substring(11, 16), 2);
                }
                // type 4 literal
                // case else do more magic
            }
        }
    }
}
