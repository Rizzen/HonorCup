using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HonorCup2
{
    internal class Genetics
    {
        private const int MaxPopulation = 50;
        private int GeneSize;

        public int[] Solve(double[] quants)
        {

            return new int[0];
        }
        

        /// <summary>Returns Indexes of zero elements of array</summary>
        public static Dictionary<int, int> GetZeroValueIndexes(int[] array)
        {
            var zeroIndexes = new Dictionary<int, int>();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] == 0)
                {
                    zeroIndexes.Add(i, 0);
                }
            }
            return zeroIndexes;
        }

        //test
        public static Dictionary<int, int> MutateZeroValues(Dictionary<int, int> newValues)
        {
            return newValues.Select(x => new KeyValuePair<int, int>(x.Key, 11))
                                   .ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>Returned values from newValues to its places in array</summary>
        public static int[] ReturnValuesInPlace(int[] array, Dictionary<int, int> newValues)
        {
            foreach (var value in newValues)
            {
                array[value.Key] = value.Value;
            }
            return array;
        }

        //test
        public static int[] MutateValues(int[] array)
        {
            var zeroes = GetZeroValueIndexes(array);
            var mutated = MutateZeroValues(zeroes);
            var result = ReturnValuesInPlace(array, mutated);

            return result;
        }
        

    }
}
