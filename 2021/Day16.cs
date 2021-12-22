using System;
using System.Text;

namespace AdventOfCode2021
{
    internal class Day16 : Day
    {
        public override string Name => "Day 16: Packet Decoder";

        public readonly Packet OuterPacket;

        /// <summary>
        /// Constructor for Runner.
        /// </summary>
        public Day16()
        {
            OuterPacket = Read(InputString());
        }

        /// <summary>
        /// Constructor for testing.
        /// </summary>
        /// <param name="input"></param>
        public Day16(IEnumerable<string> input)
        {
            OuterPacket = Read(input);
        }

        private Packet Read(IEnumerable<string> input)
        {
            var binaryBuilder = new StringBuilder(input.Count() * 4);
            foreach (var character in input.First())
            {
                var base10 = Convert.ToInt32(character.ToString(), 16);
                var binary = Convert.ToString(base10, 2).PadLeft(4, '0');
                binaryBuilder.Append(binary);
            }

            var start = 0;
            return PacketRead(binaryBuilder, ref start, binaryBuilder.Length).First();
        }

        public List<Packet> PacketRead(StringBuilder binaryBuilder, ref int readPosition, int end)
        {
            var result = new List<Packet>();
            try
            {
                while(readPosition < end)
                {
                    var version = Convert.ToInt32(binaryBuilder.ToString(readPosition, 3), 2);
                    var type = Convert.ToInt32(binaryBuilder.ToString(readPosition + 3, 3), 2);
                
                    readPosition += 6;
                
                    if (type == 4)
                    {
                        result.Add(GetLiteral(binaryBuilder, version, ref readPosition));
                    }
                    else
                    {
                        var lengthTypeId = binaryBuilder.ToString(readPosition, 1);
                        readPosition += 1;
                        var calculation = new Packet(version, type);

                        if (lengthTypeId == "0")
                        {
                            var dataLength = Convert.ToInt32(binaryBuilder.ToString(readPosition, 15), 2);
                            readPosition += 15;

                            calculation.Packets = PacketRead(binaryBuilder, ref readPosition, readPosition + dataLength);
                        }
                        else
                        {
                            var subPackets = Convert.ToInt32(binaryBuilder.ToString(readPosition, 11), 2);
                            readPosition+=11;

                            for (int i = 0; i < subPackets; i++)
                            {
                                calculation.Packets.Add(PacketRead(binaryBuilder, ref readPosition, readPosition + 1).First());
                            }
                        }

                        result.Add(calculation);
                    }
                }
            }
            catch
            {
                // Excess bit padding, so fall out.
            }

            return result;
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
            Console.WriteLine(OuterPacket.VersionSum());
        }

        public override void SecondAnswer()
        {
            Console.WriteLine(OuterPacket.Value);
        }

        internal class Packet
        {
            public int Version { get; private set; }

            public int Type { get; private set; }

            private readonly long _value;

            public long Value 
            {
                get
                {
                    return Type switch
                    {
                        0 => Packets.Sum(c => c.Value),
                        1 => Packets.Aggregate((long)1, (x, y) => x * y.Value),
                        2 => Packets.Min(c => c.Value),
                        3 => Packets.Max(c => c.Value),
                        4 => _value,
                        5 => Packets[0].Value > Packets[1].Value ? 1 : 0,
                        6 => Packets[0].Value < Packets[1].Value ? 1 : 0,
                        7 => Packets[0].Value == Packets[1].Value ? 1 : 0,
                        _ => throw new ArgumentOutOfRangeException(nameof(Type)),
                    };
                }
            }

            public List<Packet> Packets { get; set; } = new();

            public Packet(int version, int type, string data = "")
            {
                this.Version = version;
                this.Type = type;

                if (this.Type == 4)
                {
                    // type 4 literal
                    _value = Convert.ToInt64(data, 2);
                }
            }

            public int VersionSum()
            {
                return this.Version + Packets.Sum(c => c.VersionSum());
            }
        }
    }
}
