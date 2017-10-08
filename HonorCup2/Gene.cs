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
        public int Fitness;
        public double Likelihood;

        public Gene(int geneSize) //possible to add gene list in ctor
        {
            Alleles = new int [geneSize];
        }
    }
}
