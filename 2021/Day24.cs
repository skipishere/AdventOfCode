namespace AdventOfCode2021
{
    internal class Day24 : Day
    {
        public override string Name => "Day 24: Arithmetic Logic Unit";

        private readonly List<Set> _sets = new();

        public Day24()
        {
            var input = this.InputString();

            var set = new Set();
            
            foreach (var line in input)
            {
                var command = line[..3];
                if (command == "inp" && set.Instructions.Count > 0)
                {
                    _sets.Add(set);
                    set = new Set();
                }

                set.Instructions.Add(new Instruction(command, line.Substring(4, 1), line.Length > 5 ? line[6..] : string.Empty));
            }

            _sets.Add(set);
        }

        private string Calculate(long seedZ, int setIndex)
        {
            for (int i = 9; i > 0; i--)
            {
                //Console.SetCursorPosition(10 + setIndex, 10);
                //Console.Write(i);

                long w = 0;
                long x = 0;
                long y = 0;
                long z = seedZ;

                foreach (var command in _sets[setIndex].Instructions)
                {
                    command.Activate(ref w, ref x, ref y, ref z, i);
                }

                if (setIndex == _sets.Count - 1)
                {
                    if (z == 0)
                    {
                        // we're at the last digit and z is 0, success...
                        return i.ToString();
                    }
                }
                else
                {
                    if (setIndex < 3)
                    {
                        Console.WriteLine($"Digit {i} z = {z}");
                        var nextDigit = Calculate(z, setIndex + 1);
                        if (!string.IsNullOrEmpty(nextDigit))
                        {
                            return i.ToString() + nextDigit;
                        }
                    }
                }
            }

            return string.Empty;
        }

        public override void FirstAnswer()
        {
            Console.WriteLine(Calculate(0, 0));
        }

        public override void SecondAnswer()
        {
            
            Console.WriteLine(2);
        }

        public record class Set
        {
            public List<Instruction> Instructions { get; set; } = new();
        }

        public record class Instruction
        {
            private readonly string act;
            private readonly string variableResult;
            private readonly string variableIn;
            private readonly Func<long, long, long> action;

            public Instruction(string command, string variableResult, string variableIn)
            {
                action = command switch
                {
                    "inp" => (a, b) => { return b; },
                    "add" => (a, b) => { return a + b; },
                    "mul" => (a, b) => { return a * b; },
                    "div" => (a, b) => { return Math.DivRem(a, b, out _); },
                    "mod" => (a, b) => {
                        Math.DivRem(a, b, out var c);
                        return c;
                    },
                    "eql" => (a, b) => { return a == b ? 1 : 0; },
                    _ => throw new NotImplementedException(),
                };
                act = command;
                this.variableResult = variableResult;
                this.variableIn = variableIn;
            }

            public void Activate(ref long w, ref long x, ref long y, ref long z, int serialDigit)
            {
                long b = variableIn switch
                {
                    "w" => w,
                    "x" => x,
                    "y" => y,
                    "z" => z,
                    _ => act == "inp" ? serialDigit : int.Parse(variableIn),
                };

                _ = variableResult switch
                {
                    "w" => w = action.Invoke(w, b),
                    "x" => x = action.Invoke(x, b),
                    "y" => y = action.Invoke(y, b),
                    "z" => z = action.Invoke(z, b),
                    _ => throw new InvalidDataException(),
                };
            }
        }
    }
}
