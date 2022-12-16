namespace AdventOfCode2022;

internal partial record Day08 : Day
{
    public override string Name => "Day 8: Treetop Tree House";

    private readonly int[] _treeMap;
    private readonly int _width;
    private readonly int _height;

    public Day08()
    {
        //99x99
        var input = this.InputString();
        _width = input.First().Length;
        _height = input.Count();

        _treeMap = InputString()
            .SelectMany(c => c.ToCharArray())
            .Select(d => int.Parse(d.ToString()))
            .ToArray();
    }

    public override object FirstAnswer()
    {
        var visible = new HashSet<int>();

        for (int row = 0; row < _height; row++)
        {
            ViewLeft(visible, row, 0);
            ViewRight(visible, row, _width - 1);
        }

        for (int col = 0; col < _width; col++)
        {
            ViewDown(visible, col, 0);
            ViewUp(visible, col, _height - 1);
        }

        return visible.Count;
    }

    public override object SecondAnswer()
    {
        var bestScore = 0;

        for (int row = 1; row < _height; row++)
        {
            for (int col = 1; col < _width; col++)
            {
                bestScore = Math.Max(bestScore, View(row, col));
            }
        }

        return bestScore;
    }

    private int View(int treeRow, int treeCol)
    {
        var location = treeRow * _width + treeCol;
        var height = _treeMap[location];

        // look up
        var sceneScore = 0;
        for (var row = treeRow-1; row > 0; row++)
        {
            var nextTree = _treeMap[Location(row, treeCol)];
            if (nextTree >= height)
            {
                break;
            }

            sceneScore++;
        }
        if (sceneScore == 0) { return 0; }

        // look down
        for (var row = treeRow + 1; row > 0; row++)
        {
            var nextTree = _treeMap[Location(row, treeCol)];
            if (nextTree >= height)
            {
                break;
            }

            sceneScore++;
        }
        if (sceneScore == 0) { return 0; }

        return 0;
    }

    private int Location(int row, int col) => row * _width + col;

    private void ViewLeft(HashSet<int> visible, int row, int startCol)
    {
        ViewHorizontal(visible, row, startCol, col => col < _width, 1);
    }

    private void ViewRight(HashSet<int> visible, int row, int startCol)
    {
        ViewHorizontal(visible, row, startCol, col => col >= 0, -1);
    }

    private void ViewHorizontal(HashSet<int> visible, int row, int startCol, Func<int, bool> loopCheck, int direction)
    {
        var lastHeight = -1;
        for (int col = startCol; loopCheck(col); col+=direction)
        {
            var location = Location(row, col);

            if (_treeMap[location] > lastHeight)
            {
                lastHeight = _treeMap[location];
                visible.Add(location);
            }
        }
    }

    private void ViewDown(HashSet<int> visible, int col, int startRow)
    {
        ViewVertical(visible, col, startRow, row => row < _height, 1);
    }

    private void ViewUp(HashSet<int> visible, int col, int startRow)
    {
        ViewVertical(visible, col, startRow, row => row >= 0, -1);
    }

    private void ViewVertical(HashSet<int> visible, int col, int startRow, Func<int, bool> loopCheck, int direction)
    {
        var lastHeight = -1;
        for (var row = startRow; loopCheck(row); row+= direction)
        {
            var location = Location(row, col);
            
            if (_treeMap[location] > lastHeight)
            {
                lastHeight = _treeMap[location];
                visible.Add(location);
            }
        }
    }
}
