using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MathagramsProgram
{
    class Program
    {
        private enum MATHAGRAMS_TYPE { Single, Double, Triple };
        private const string DIGITS_MATCH_PATTERN = @"[\dx]{3}";
        private const string EXPRESSION_FORMAT = @"{0} + {1} = {2}";
        private const string DOUBLE_EXPRESSION_FORMAT = @"{0} + {1} + {2} + {3} = {4} + {5}";
        private static readonly string[] NUMBERS = { "1", "2", "3", "4", "5", "6", "7", "8", "9", };
        private static readonly string[] DOUBLE_NUMBERS = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "1", "2", "3", "4", "5", "6", "7", "8", "9", };

        static void Main(string[] args)
        {
            Console.WriteLine("Input mathagram to solve:");
            string solved = SolveMathagram(Console.ReadLine());
            Console.WriteLine("Solution: " + solved);

            Console.ReadKey();
        }

        public static string SolveDoubleMathagram(string expression)
        {
            List<string> nums = new List<string>((string[])DOUBLE_NUMBERS.Clone());
            char[] exprArr = expression.ToCharArray();

            foreach (char c in exprArr)
            {
                nums.Remove(c.ToString());
            }
            // nums now contains the numbers we can use to form the equation.

            while (true)
            {
                nums = Shuffle(nums);

                string[] digits = Regex.Matches(expression, DIGITS_MATCH_PATTERN).OfType<Match>().Select(m => m.Groups[0].Value).ToArray<string>();

                int numsIndex = 0;
                for (int i = 0; i < digits.Length; i++)
                {
                    char[] digitsArr = digits[i].ToCharArray();
                    for (int j = 0; j < digits[i].Length; j++)
                    {
                        if (digitsArr[j] == 'x')
                        {
                            digitsArr[j] = nums[numsIndex][0];
                            numsIndex++;
                        }
                    }
                    digits[i] = new string(digitsArr);
                }

                if (Int32.Parse(digits[0]) + Int32.Parse(digits[1]) + Int32.Parse(digits[2]) + Int32.Parse(digits[3]) == Int32.Parse(digits[4]) + Int32.Parse(digits[5]))
                {
                    return String.Format(DOUBLE_EXPRESSION_FORMAT, digits[0], digits[1], digits[2], digits[3], digits[4], digits[5]);
                }
            }
        }

        public static string SolveMathagram(string expression)
        {
            List<string> nums = new List<string>((string[])NUMBERS.Clone());
            char[] exprArr = expression.ToCharArray();

            foreach (char c in exprArr)
            {
                nums.Remove(c.ToString());
            }
            // nums now contains the numbers we can use to form the equation.

            while (true)
            {
                nums = Shuffle(nums);

                string[] digits = Regex.Matches(expression, DIGITS_MATCH_PATTERN).OfType<Match>().Select(m => m.Groups[0].Value).ToArray<string>();

                int numsIndex = 0;
                for (int i = 0; i < digits.Length; i++)
                {
                    char[] digitsArr = digits[i].ToCharArray();
                    for (int j = 0; j < digits[i].Length; j++)
                    {
                        if (digitsArr[j] == 'x')
                        {
                            digitsArr[j] = nums[numsIndex][0];
                            numsIndex++;
                        }
                    }
                    digits[i] = new string(digitsArr);
                }

                if (Int32.Parse(digits[0]) + Int32.Parse(digits[1]) == Int32.Parse(digits[2]))
                {
                    return String.Format(EXPRESSION_FORMAT, digits[0], digits[1], digits[2]);
                }
            }
        }

        private static List<string> Shuffle(List<string> list)
        {
            Random rdm = new Random();
            string[] stringArr = list.ToArray<string>();
            string[] outputStringArr = new string[stringArr.Length];

            for (int i = stringArr.Length - 1; i >= 0; i--)
            {
                do
                {
                    int rdmIndex = rdm.Next(stringArr.Length);
                    if (!outputStringArr.Contains<string>(stringArr[rdmIndex]))
                        { outputStringArr[i] = stringArr[rdmIndex]; }
                } while (outputStringArr[i] == null);
            }

            return outputStringArr.ToList<string>();
        }
    }
}
