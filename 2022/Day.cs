namespace AdventOfCode2022
{
    internal abstract record Day
    {
        public abstract string Name { get; }

        public abstract string FirstAnswer();
        public abstract string SecondAnswer();

        internal IEnumerable<string> InputString() => File.ReadLines($@"inputs\{GetType().Name}.txt");

        internal IEnumerable<int> InputInt() => this.InputString().Select(c => int.Parse(c));

        //TODO Add in ability to load in input with mapping ("string int" etc)
        //TODO Get input directly from website url 
    }
}
