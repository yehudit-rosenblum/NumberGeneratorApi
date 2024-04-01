using NumberGeneratorApi.Application.Interfaces;

namespace NumberGeneratorApi.Application.Services
{

    public class NumberGeneratorService: INumberGeneratorService
    {
    
        //מחזירה את הקומבינציות החל ממספר סידורי עד למספר סידורי
        public  List<List<int>> GetCombinationsRange(int n, long x, long y)
        {
            //יוצרת רשימה של רשימות
            var range = new List<List<int>>();
            for (long i = x; i <= y; i++)
            {
                //מוסיפה את הקומבינציה לרשימה
                range.Add(GetNthCombination(n, i));
            }
            return range;
        }

        //מקבלת את המספר קלט ואת המספר הסידורי העכשווי 
        //מחזיר קומבינציה ספציפית 
        public  List<int> GetNthCombination(int n, long nth)
        {
            List<int> numbers = new List<int>();
            for (int i = 1; i <= n; i++) numbers.Add(i);

            List<int> permutation = new List<int>();
            nth--; // Convert to zero-based index
            for (int i = 1; i <= n; i++)
            {
                int factorial = Factorial(n - i);
                int index = (int)(nth / factorial);
                permutation.Add(numbers[index]);
                numbers.RemoveAt(index);
                nth -= index * factorial;
            }
            return permutation;
        }


        //מחזירה כמה קומבינציות קיימים
        public  long TotalCombinations(int n)
        {
            long total = 1;
            for (int i = 2; i <= n; i++)
            {
                total *= i;
            }
            return total;
        }


        //
        private static int Factorial(int n)
        {
            int result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;
            return result;
        }
    }

}
