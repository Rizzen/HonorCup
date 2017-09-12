using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonorCup2
{
    internal class Filter
    {
        private const double K = 512;
        
        /// <summary>Возвращает сетку частот</summary>
        public static double[] FrequencyGrid ()
        {
            var grid = new double[512];
            for (int i = 0; i < 512; i++)
            {
                grid[i] = (i * Math.PI) / K;
                Console.WriteLine($"{grid[i]} {i}");
            }
            return grid;
        }

        /// <summary>Комплексный коэффициент передачи</summary>
        public static Complex ComplexTransferСoefficient(double[] quants, double omegaFrequency)
        {
            var res = ComplexSumB(quants, omegaFrequency) 
                     / (new Complex(1, 0) + ComplexSumA(quants, omegaFrequency));
            return res;
        }

        /// <summary>Комплексная сумма для b (начинается с нуля)</summary>
        public static Complex ComplexSumB(double[] quants, double omegaFrequency)
        {
            var sum = new Complex(0, 0);
            for (int i = 0; i < quants.Length; i++)
            {
                var quantComplex = new Complex(quants[i], 0);
                var complexExponent = Complex.Exp(new Complex(0, omegaFrequency * i * -1));
                sum += quantComplex * complexExponent;
            }
            return sum;
        }

        /// <summary>Комплексная сумма для a (начинается с единицы)</summary>
        public static Complex ComplexSumA(double[] quants, double omegaFrequency)
        {
            var sum = new Complex(0, 0);
            for (int i = 1; i <= quants.Length; i++)
            {
                var quantComplex = new Complex(quants[i - 1], 0);
                var complexExponent = Complex.Exp(new Complex(0, omegaFrequency * i * -1));
                sum += quantComplex * complexExponent; //Complex.Multiply(quantComplex, complexExponent);
            }
            return sum;
        }

        //Mean Square of Error must be here
    }
}
