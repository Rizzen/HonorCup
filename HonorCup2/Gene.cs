using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonorCup2
{
    internal class Gene
    {
        public int[] Alleles;

        public double Fitness;
        public double Likelihood;
        
        private readonly Dictionary<int, int> zeroIndexes;
        private static readonly Random r;

        static Gene()
        {
            r = new Random();
            
        }

        public Gene(int[] quants, Dictionary<int, int> _zeroIndexes)
        {
            zeroIndexes = _zeroIndexes;
            Alleles = quants;
        }
        
        /// <summary>Mutate gene</summary>
        public Gene Mutate()
        {
            int[] alleles = new int[Alleles.Length];

            for (int i = 0; i < Alleles.Length; i++)
            {
                if (zeroIndexes.ContainsKey(i))
                    alleles[i] = r.Next(-255, 255);
                else
                    alleles[i] = Alleles[i];
            }

            var gene = new Gene(alleles, zeroIndexes);

            Console.WriteLine(gene.ToString());
            return gene;
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
