namespace com.toni.mlin.Match
{
    public class MatchConfig
    {
        public MatchConfig(int size)
        {
            Size = size;
        }

        public void SetSize(int size)
        {
            Size = size;
        }

        public int Size { get; private set; }
    }
}