namespace AdventOfCode2021
{
    internal abstract record Day
    {
        public abstract string Name { get; }

        public abstract void FirstAnswer();
        public abstract void SecondAnswer();

        internal IEnumerable<string> InputString() => File.ReadLines($@"inputs\{GetType().Name}.txt");

        internal IEnumerable<int> InputInt() => this.InputString().Select(c => int.Parse(c));

        //TODO Add in ability to load in input with mapping ("string int" etc)
        //TODO Get input directly from website url 
    }
}
