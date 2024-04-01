namespace NumberGeneratorApi.Application.Interfaces
{
    public interface INumberGeneratorService
    {
        public List<List<int>> GetCombinationsRange(int n, long x, long y);
       
        public List<int> GetNthCombination(int n, long nth);

        public long TotalCombinations(int n);
       
    }
}
