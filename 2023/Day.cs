namespace AdventOfCode2023
{
    internal abstract record Day
    {
        public abstract string Name { get; }

        public abstract object FirstAnswer();
        public abstract object SecondAnswer();

        internal IEnumerable<string> InputString() => File.ReadLines($@"inputs\{GetType().Name}.txt");

        internal IEnumerable<int> InputInt() => this.InputString().Select(int.Parse);

        //TODO Add in ability to load in input with mapping ("string int" etc)
        //TODO Get input directly from website url 
    }
}
