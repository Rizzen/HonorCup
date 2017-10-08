using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonorCup2
{
    internal class Gene
    {
        public int[] AAlleles;
        public int[] BAlleles;

        public double Fitness;
        public double Likelihood;
        
        public readonly Dictionary<int, int> aZeroIndexes;
        public readonly Dictionary<int, int> bZeroIndexes;
        private static readonly Random r;

        static Gene()
        {
            r = new Random();
            
        }

        public Gene(int[] aQuants, int[] bQuants, Dictionary<int, int> _aZeroIndexes, Dictionary<int, int> _bZeroIndexes)
        {
            aZeroIndexes = _aZeroIndexes;
            bZeroIndexes = _bZeroIndexes;
            BAlleles = bQuants;
            AAlleles = aQuants;
        }
        
        /// <summary>Mutate gene</summary>
        public Gene Mutate()
        {
            int[] _aAlleles = mutateAlleles(AAlleles, aZeroIndexes);
            int[] _bAlleles = mutateAlleles(BAlleles, bZeroIndexes);
            
            var gene = new Gene(_aAlleles, _bAlleles, aZeroIndexes, bZeroIndexes);

            //Console.WriteLine(gene.ToString());
            return gene;
        }

        private int[] mutateAlleles(int[] alleles, Dictionary<int, int> zeroIndexes)
        {
            var res = new int[alleles.Length];
            for (int i = 0; i < alleles.Length; i++)
            {
                if (zeroIndexes.ContainsKey(i))
                    res[i] += r.Next(-5, 5);
                else
                    res[i] = alleles[i];
            }

            return res;
        }
        

        
        #region Override
        public override string ToString()
        {
            var str = $"Gene {this.GetHashCode()} contains:";
            foreach (var a in AAlleles)
            {
                str = String.Concat(str, $"_{a}_");
            }
            str = String.Concat(str, "\n");
            foreach (var b in BAlleles)
            {
                str = String.Concat(str, $"_{b}_");
            }
            return str;
        }
        #endregion

    }
}
