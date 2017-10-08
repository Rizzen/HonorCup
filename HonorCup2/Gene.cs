using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonorCup2
{
    internal class Gene
    {
        public int[] aAlleles;
        public int[] bAlleles;

        public double Fitness;
        public double Likelihood;
        
        private readonly Dictionary<int, int> aZeroIndexes;
        private readonly Dictionary<int, int> bZeroIndexes;
        private static readonly Random r;

        static Gene()
        {
            r = new Random();
            
        }

        public Gene(int[] aQuants, int[] bQuants, Dictionary<int, int> _aZeroIndexes, Dictionary<int, int> _bZeroIndexes)
        {
            aZeroIndexes = _aZeroIndexes;
            bZeroIndexes = _bZeroIndexes;
            bAlleles = bQuants;
            aAlleles = aQuants;
        }
        
        /// <summary>Mutate gene</summary>
        public Gene Mutate()
        {
            int[] _aAlleles = mutateAlleles(aAlleles, aZeroIndexes);
            int[] _bAlleles = mutateAlleles(bAlleles, bZeroIndexes);
            
            var gene = new Gene(_aAlleles, _bAlleles, aZeroIndexes, bZeroIndexes);

            Console.WriteLine(gene.ToString());
            return gene;
        }

        private int[] mutateAlleles(int[] alleles, Dictionary<int, int> zeroIndexes)
        {
            var res = new int[alleles.Length];
            for (int i = 0; i < alleles.Length; i++)
            {
                if (zeroIndexes.ContainsKey(i))
                    res[i] = r.Next(-255, 255);
                else
                    res[i] = alleles[i];
            }

            return res;
        }
        

        
        #region Override
        public override string ToString()
        {
            var str = $"Gene {this.GetHashCode()} contains:";
            foreach (var a in Alleles)
            {
                str = String.Concat(str, $"_{a}_");
            }
            return str;
        }
        #endregion

    }
}
