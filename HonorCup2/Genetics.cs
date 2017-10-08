using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonorCup2
{
    internal class Genetics
    {
        
        private const int MaxPopulation = 100;
        
        private static Gene[] populaton = new Gene[MaxPopulation];

        private static Random r;
        /// <summary>Solve the task</summary>
        public static Gene Solve(double[] aQuants, double[] bQuants, int[] aRoundedQuants, int[] bRoundedQuants)
        {
            r = new Random();
            var Choosen = new List<Gene>();
            //get zero indexes
            var aZeroIndexes = GetZeroValueIndexes(aRoundedQuants);
            var bZeroIndexes = GetZeroValueIndexes(bRoundedQuants);

            //create start population
            for (int i = 0; i < MaxPopulation; i++)
            {
                populaton[i] = new Gene(aRoundedQuants, bRoundedQuants, aZeroIndexes, bZeroIndexes).Mutate();
            }
            
            //calc fitness for current population
            CalclulatePopulationFitness(aQuants, bQuants, populaton);
            //generate likehoods for population
            GenerateLikehoods(populaton);

            //new population
            var pop = CreateNewPopulation(populaton);
            CalclulatePopulationFitness(aQuants, bQuants, pop);
            var dweller = NaturalSelection(pop);
            GenerateLikehoods(pop);

            var iterator = 0;
            while (iterator < 50)
            {
                pop = CreateNewPopulation(pop);
                CalclulatePopulationFitness(aQuants, bQuants, pop);
                GenerateLikehoods(pop);
                var newDweller = NaturalSelection(pop);
                if (newDweller.Fitness > dweller.Fitness)
                {
                    pop = CreateMutants(pop);
                    CalclulatePopulationFitness(aQuants, bQuants, pop);
                    GenerateLikehoods(pop);
                    newDweller = NaturalSelection(pop);
                }
                Choosen.Add(newDweller);
                Console.WriteLine($"Generation {iterator}");
                iterator++;
            }

            foreach (var d in Choosen)
            {
                var str = "A alleles is: ";
                foreach (var aa in d.AAlleles)
                {
                    str = String.Concat(str, $" {aa} ");
                }
                str = String.Concat(str, "\nB alleles is:");

                foreach (var aa in d.BAlleles)
                {
                    str = String.Concat(str, $" {aa} ");
                }
                str = String.Concat(str, $"\n Fitsess is {d.Fitness}");
                Console.WriteLine(str);
            }

            return Choosen.OrderByDescending(x => x.Fitness).Last();
        }

        public static Gene NaturalSelection(Gene[] _population)
        {
            return _population.OrderByDescending(x => x.Fitness).Last();
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
                //Console.WriteLine(gene.Fitness);
            }
        }

        public static double Fitnessest(Gene[] _population)
        {
            return _population.Min(x => x.Fitness);
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
            var pop = _population.OrderByDescending(x=>x.Fitness).ToArray();
            return pop[r.Next(pop.Length/2)];
            //var averageLikelihood = _population.Average(x => x.Likelihood) * 100;
            //foreach (Gene gene in _population)
            //{
            //    var num = r.Next(0, (int) averageLikelihood * 2);
            //    if (num >= averageLikelihood)
            //    {
            //        return gene;
            //    }
            //}

            //return _population[r.Next(_population.Length - 1)];
        }

        public static double MultInv(Gene[] _population)
        {
            return _population.Sum(x => (1 / x.Fitness));
        }

        /// <summary>Returns Child of two parents</summary>
        public static Gene Breed(Gene parent1, Gene parent2)
        {
            //selecting crossover point
            var crossPoint = r.Next(2, parent1.AAlleles.Length);
            
            //initial child
            var child = new Gene(parent1.AAlleles, parent1.BAlleles, parent1.aZeroIndexes, parent1.bZeroIndexes);
            while (child.BAlleles.Any(x => x == 0) || child.AAlleles.Any(x => x == 0))
            {
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
                if (r.NextDouble() <= 0.05)
                {
                    child = child.Mutate();
                }
            }
            return child;
        }
        
        /// <summary>Generate new population based on specified </summary>
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

        public static Gene[] CreateMutants(Gene[] _population)
        {
            var newPopulation = new Gene[MaxPopulation];

            for (int i = 0; i < newPopulation.Length; i++)
            {
                var parent1 = GetGeneForCrossover(_population).Mutate();
                var parent2 = GetGeneForCrossover(_population).Mutate();
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
