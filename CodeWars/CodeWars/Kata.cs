using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public class Kata
    {
        public static string DuplicateEncode(string word)
        {
            var wordToLower = word.ToLower();
            var chars = wordToLower.Distinct();
            var dict = chars.ToDictionary(key => key, key => wordToLower.Count(x => x == key));
            StringBuilder returnWord = new StringBuilder("");
            for (int i = 0; i < word.Length; i++)
            {
                if (dict[wordToLower[i]] > 1)
                {
                    returnWord.Append(')');
                }
                else
                {
                    returnWord.Append("(");
                }
            }
            return returnWord.ToString();
        }

        //Find the odd int
        public static int find_it(int[] seq)
        {
            var seqWithoutDuplicate = seq.Distinct();
            var dictionary = seqWithoutDuplicate.ToDictionary(key => key, key => seq.Count(x => x == key));
            var number = dictionary.Single(x => x.Value % 2 == 1);
            return number.Key;
        }

        public static int find_it2(int[] seq)
        {
            return seq.GroupBy(x => x).Single(g => g.Count() % 2 == 1).Key;
        }

        //Find the Parity Outlier
        public static int Find(int[] integers)
        {
            try
            {
                return integers.Single(x => x % 2 == 0);
            }
            catch
            {
                return integers.Single(x => x % 2 == 1 || x % 2 == -1);
            }
        }

        //Count Bits
        public static int CountBits(int n)
        {
            return (int)Convert.ToString(n, 2).Sum(x => char.GetNumericValue(x));
        }

        //Take a ten Minute Walk
        public static bool IsValidWalk(string[] walk)
        {
            if (walk.Length != 10) return false;
            var walkDirections = walk.ToArray();
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int> { { "n", 0 }, { "e", 0 }, { "s", 0 }, { "w", 0 } };
            foreach (string direction in walkDirections)
            {
                keyValuePairs[direction]++;
            }
            return keyValuePairs["n"] == keyValuePairs["s"] && keyValuePairs["e"] == keyValuePairs["w"];
        }

        public static long QueueTime(int[] customers, int n)
        {
            if (customers.Length == 0) return 0;
            if (n == 1) return customers.Sum();
            var tills = new int[n > customers.Length ? customers.Length : n];
            foreach (var item in customers)
            {
                tills[Array.IndexOf(tills, tills.Min())] += item;
            }
            return tills.Max();
        }

        public static string MakeComplement(string dna)
        {
            return string.Concat(dna.Select(x => x == 'T' ? 'A' : x == 'A' ? 'T' : x == 'G' ? 'C' : x == 'C' ? 'G' : x));
        }

        //Who likes it?
        public static string Likes(string[] name)
        {
            switch (name.Length)
            {
                case 0: return "no one likes this";
                case 1: return $"{name[0]} likes this";
                case 2: return $"{name[0]} and {name[1]} like this";
                case 3: return $"{name[0]}, {name[1]} and {name[2]} like this";
                default: return $"{name[0]}, {name[1]} and {name.Length - 2} others like this";
            }
        }

        //Binary Addition
        public static string AddBinary(int a, int b)
        {
            return Convert.ToString(a + b, 2);
        }

        //Number of people in the Bus

        public static int Number2(List<int[]> peopleListInOut) => peopleListInOut.Sum(p => p[0] - p[1]);

        public static int Number(List<int[]> peopleListInOut)
        {
            int sum = 0;
            foreach (var item in peopleListInOut)
            {
                sum += item[0] - item[1];
            }
            return sum;
        }

        // Two to One
        public static string Longest(string s1, string s2)
        {
            return String.Concat(s1.Union(s2).OrderBy(s => s));
        }

        // ShortestWord
        public static int FindShort1(string s) => s.Split(' ').Min(p => p.Length);

        public static int FindShort(string s)
        {
            return s.Split(' ').OrderBy(p => p.Length).FirstOrDefault().Length;
        }

        public static int[] ArrayDiff(int[] a, int[] b)
        {
            return a.Where(p => !b.Contains(p)).ToArray();
        }

        //Square Every Digit
        public static int SquareDigits(int n)
        {
            return int.Parse(String.Concat(n.ToString().Select(x => Math.Pow(int.Parse(x.ToString()), 2))));
        }

        // Replace with Alphabet Position

        public static string AlphabetPosition(string text)
        {
            var output = String.Join(" ", text.ToLower().Where(x => char.IsLetter(x)).Select(y => ((int)y - 96)));
            return output;
        }

        // Equal Sides of An Array
        public static int FindEvenIndex2(int[] arr)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                if (arr.Take(i).Sum() == arr.Skip(i + 1).Sum()) { return i; }
            }
            return -1;
        }

        public static int FindEvenIndex3(int[] arr)
        {
            int leftSum = 0, rightSum = arr.Sum();

            for (int i = 0; i < arr.Length; ++i)
            {
                rightSum -= arr[i];

                if (leftSum == rightSum)
                {
                    return i;
                }

                leftSum += arr[i];
            }

            return -1;
        }

        public static int FindEvenIndex(int[] arr)
        {
            if (arr.Length <= 1) return 0;
            int first, last, index1, index2;
            for (int i = 0; i < arr.Length; i++)
            {
                first = last = 0;
                index1 = i - 1;
                index2 = i + 1;
                while (index1 >= 0)
                {
                    first += arr[index1];
                    index1--;
                }
                while (index2 < arr.Length)
                {
                    last += arr[index2];
                    index2++;
                }
                if (first == last) return i;
            }
            return -1;
        }
    }
}