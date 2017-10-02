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
        //по условию
        private const double K = 512;

        /// <summary>Frequency Grid</summary>
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

        /// <summary>Complex Transfer Сoefficient</summary>
        public static Complex ComplexTransferСoefficient(double[] quants, double omegaFrequency)
        {
            var res = ComplexSumB(quants, omegaFrequency) 
                     / (new Complex(1, 0) + ComplexSumA(quants, omegaFrequency));
            return res;
        }

        /// <summary>Complex Sum for B (начинается с нуля)</summary>
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

        /// <summary>Complex Sum for A (started with 1)</summary>
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

        /// <summary>Rounding rule</summary>
        public static double FilterRound(double number) => Math.Round(number * 16);

        
        /// <summary>Mean Square of Error</summary>
        public static double MeanSquareOfError(double[] quants, double[] roundedQuants)
        {
            var fGrid = FrequencyGrid();
            var sum = 0d;
            for (int i = 0; i < 512; i++)
            {
                var Hq = ComplexTransferСoefficient(roundedQuants, fGrid[i]);
                var H = ComplexTransferСoefficient(quants, fGrid[i]);
                var summary = Math.Pow(Complex.Abs(Hq - H), 2);
                sum += summary;
            }

            sum = sum / K;

            return sum;
        }
    }
}
