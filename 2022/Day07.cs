namespace AdventOfCode2022;

internal partial record Day07 : Day
{
    public override string Name => "Day 6: No Space Left On Device";

    private Folder _root;

    public Day07()
    {
        var listMode = false;
        foreach (var item in InputString().Skip(1))
        {
            if (item.StartsWith("$"))
            {
                listMode = false;
                switch (item)
                {
                    case "$ cd ..":
                        // pop current folder
                        break;
                    case "$ ls":
                        listMode = true;
                        break;
                    default:
                        // cd <name> - change folder
                        break;
                }
            }
            else if (item.StartsWith("dir "))
            {

            }
            else
            {
                var file = new File(item);

            }


        }
        //_input = InputString().ToList();
    }

    public override object FirstAnswer()
    {
        return _root.Size;
    }

    public override object SecondAnswer()
    {
        return "";
    }

    private  interface IFileSystem
    {
        string Name { get; }
        int Size { get;}
    }

    private record Folder : IFileSystem
    {
        public string Name { get; set; }

        public int Size => Children.Sum(c => c.Size);

        public List<Folder> Children { get; set; }

        public long GetSize(int limit)
        {
            return Children
                .Where(c => c.Size <= limit)
                .Sum(c => c.Size);
        }
    }

    private record File :IFileSystem
    {
        public int Size { get; set; }

        public string Name { get; set; }

        public File(string rawInput)
        {
            var input = rawInput.Split(' ');
            Size = int.Parse(input[0]);
            Name = input[1];
        }
    }
}
