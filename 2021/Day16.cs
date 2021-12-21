using System;
using System.Text;

namespace AdventOfCode2021
{
    internal class Day16 : Day
    {
        public override string Name => "Day 16: Packet Decoder";

        public List<Packet> packets = new();

        public Day16()
        {
            try
            {
                Read(InputString());
            }
            catch
            {
            }
        }

        public Day16(IEnumerable<string> input)
        {
            try
            {
                this.Read(input);
            }
            catch
            {
                // It's ok, some tests will hit the out of bounds issue due to the test cases given.
            }
        }

        private void Read(IEnumerable<string> input)
        {
            var binaryBuilder = new StringBuilder(input.Count() * 4);
            foreach (var character in input.First())
            {
                var base10 = Convert.ToInt32(character.ToString(), 16);
                var binary = Convert.ToString(base10, 2).PadLeft(4, '0');
                binaryBuilder.Append(binary);
            }

            var start = 0;
            PacketRead(binaryBuilder, ref start, binaryBuilder.Length);
        }

        public void PacketRead(StringBuilder binaryBuilder, ref int readPosition, int end)
        {
            while(readPosition < end)
            {
                var version = Convert.ToInt32(binaryBuilder.ToString(readPosition, 3), 2);
                var type = Convert.ToInt32(binaryBuilder.ToString(readPosition + 3, 3), 2);
                
                readPosition += 6;

                if (type == 4)
                {
                    packets.Add(GetLiteral(binaryBuilder, version, ref readPosition));
                }
                else
                {
                    var lengthTypeId = binaryBuilder.ToString(readPosition, 1);
                    readPosition += 1;
                    packets.Add(new Packet(version, type, string.Empty));

                    if (lengthTypeId == "0")
                    {
                        var dataLength = Convert.ToInt32(binaryBuilder.ToString(readPosition, 15), 2);
                        readPosition += 15;
                        
                        PacketRead(binaryBuilder, ref readPosition, readPosition+dataLength);
                    }
                    else
                    {
                        var subPackets = Convert.ToInt32(binaryBuilder.ToString(readPosition, 11), 2);
                        readPosition+=11;

                        for (int i = 0; i < subPackets; i++)
                        {
                            PacketRead(binaryBuilder, ref readPosition, readPosition + 1);
                        }
                    }
                }
            }
        }

        public static Packet GetLiteral(StringBuilder binaryBuilder, int version, ref int position)
        {
            var dataBuilder = new StringBuilder();
            var keepGoing = true;
            while (keepGoing)
            {
                keepGoing = binaryBuilder[position] == '1';
                dataBuilder.Append(binaryBuilder.ToString(position + 1, 4));
                position += 5;
            }

            return new Packet(version, 4, dataBuilder.ToString());
        }
    
       
        public override void FirstAnswer()
        {
            Console.WriteLine(packets.Sum(c=> c.Version));
        }

        public override void SecondAnswer()
        {
            Console.WriteLine(2);
        }

        internal class Packet
        {
            public int Version { get; private set; }

            public int Type { get; private set; }

            public long Value { get; private set; }

            public Packet(int version, int type, string data)
            {
                this.Version = version;
                this.Type = type;

                if (this.Type == 4)
                {
                    Value = Convert.ToInt64(data, 2);
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
