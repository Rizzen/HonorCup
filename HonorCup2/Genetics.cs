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

        private static Random r;
        /// <summary>Solve the task</summary>
        public static int[] Solve(double[] aQuants, double[] bQuants, int[] aRoundedQuants, int[] bRoundedQuants)
        {
            r = new Random();
            //get zero indexes
            var aZeroIndexes = GetZeroValueIndexes(aRoundedQuants);
            var bZeroIndexes = GetZeroValueIndexes(bRoundedQuants);

            //create start population
            for (int i = 0; i < 50; i++)
            {
                populaton[i] = new Gene(aRoundedQuants, bRoundedQuants, aZeroIndexes, bZeroIndexes).Mutate();
            }
            
            //calc fitness for current population
            CalclulatePopulationFitness(aQuants, bQuants, populaton);
            
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

        /// <summary>Calculate fitness of population</summary>
        public static void CalclulatePopulationFitness(double[] aStartQuants, double[] bStartQuants, Gene[] _population)
        {
            foreach (var gene in _population)
            {
                gene.Fitness = Filter.MeanSquareOfError(aStartQuants, bStartQuants, gene.BAlleles, gene.AAlleles);
                Console.WriteLine(gene.Fitness);
            }
        }


        public static void GenerateLikehoods(Gene[] _population)
        {
            var multInv = MultInv(_population);
            foreach (var gene in _population)
            {
                gene.Likelihood = (1 / gene.Fitness) / multInv * 100;
            }
        }

        public static Gene GetGeneForCrossover(Gene[] _population)
        {
            var averageLikelihood = _population.Average(x => x.Likelihood);
            foreach (Gene gene in _population)
            {
                var num = r.NextDouble();
                if (num >= averageLikelihood)
                {
                    return gene;
                }
            }

            return _population[r.Next(_population.Length - 1)];
        }

        public static double MultInv(Gene[] _population)
        {
            return _population.Sum(x => (1 / x.Fitness));
        }

        /// <summary>Breed</summary>
        public static Gene Breed(Gene parent1, Gene parent2)
        {
            //selecting crossover point
            var crossPoint = r.Next(2, parent1.AAlleles.Length);
            
            //initial child
            var child = new Gene(parent1.AAlleles, parent1.BAlleles, parent1.aZeroIndexes, parent1.bZeroIndexes);

            //whos alleles comes first?
            var whoIsFirst = r.NextDouble();
            var initial = 0;
            var final = child.AAlleles.Length - 1 - crossPoint;
            if (whoIsFirst >= 0.5)
            {
                initial = crossPoint;
            }

            //Crossover!
            for (int i = initial; i < final; i++)
            {
                child.AAlleles[i] = parent2.AAlleles[i];
                child.BAlleles[i] = parent2.BAlleles[i];
            }

            //for great selection!
            if (r.NextDouble() >= 0.05)
            {
                child = child.Mutate();
            }

            return child;
        }
        
        public static Gene[] CreateNewPopulation(Gene[] _population)
        {
            var newPopulation = new Gene[MaxPopulation];
            
            for (int i = 0; i < newPopulation.Length; i++)
            {
                var parent1 = GetGeneForCrossover(_population);
                var parent2 = GetGeneForCrossover(_population);
                var child = Breed(parent1, parent2);
                newPopulation[i] = child;
            }
            return newPopulation;
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
