using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonorCup2
{
    internal class Genetics
    {
        private const int MaxPopulation = 50;
        private static Gene[] populaton = new Gene[MaxPopulation];

        /// <summary>Solve the task</summary>
        public static int[] Solve(double[] quants, int[] roundedQuants)
        {
            //get zero indexes
            var zeroIndexes = GetZeroValueIndexes(roundedQuants);

            //create population
            for (int i = 0; i < 50; i++)
            {
                populaton[i] = new Gene(roundedQuants, zeroIndexes).Mutate();
                
            }
            //calc fitness
            CalclulatePopulationFitness(quants, populaton);

            return new int[0];
        }
        
        /// <summary>Returns indexes of zero elements of array</summary>
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
        
        /// <summary>Returned values from newValues to its places in array</summary>
        public static int[] ReturnValuesInPlace(int[] array, Dictionary<int, int> newValues)
        {
            foreach (var value in newValues)
            {
                array[value.Key] = value.Value;
            }
            return array;
        }

        public static void CalclulatePopulationFitness(double[] startQuants, Gene[] _population)
        {
            foreach (var gene in _population)
            {
                gene.Fitness = Filter.MeanSquareOfError(startQuants, gene.Alleles);
            }
        }

        #region Test
        //test
        public static Dictionary<int, int> MutateZeroValues(Dictionary<int, int> values)
        {
            return values.Select(x => new KeyValuePair<int, int>(x.Key, 11))
                .ToDictionary(x => x.Key, y => y.Value);
        }

        //test
        public static int[] MutateValues(int[] array)
        {
            var zeroes = GetZeroValueIndexes(array);
            var mutated = MutateZeroValues(zeroes);
            var result = ReturnValuesInPlace(array, mutated);

            return result;
        }
        #endregion
    }
}
