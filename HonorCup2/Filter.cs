using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonorCup2
{
    /// <summary>Represents methods collection for filter coefficients calculation</summary>
    internal static class Filter
    {
        //From Statement
        private const double K = 512;

        public static double[] FrequencyGrid { get; set; }

        static Filter()
        {
            FrequencyGrid = GetFrequencyGrid();
        }

        /// <summary>Frequency Grid</summary>
        public static double[] GetFrequencyGrid ()
        {
            var grid = new double[512];
            for (int i = 0; i < 512; i++)
            {
                grid[i] = (i * Math.PI) / K;
               // Console.WriteLine($"{grid[i]} {i}");
            }
            return grid;
        }

        /// <summary>Complex Transfer Сoefficient</summary>
        public static Complex ComplexTransferСoefficient(double[] aQuants, double[] bQuants, double omegaFrequency)
        {
            var res = ComplexSumB(bQuants, omegaFrequency) 
                     / (new Complex(1, 0) + ComplexSumA(aQuants, omegaFrequency));
            return res;
        }

        /// <summary>Complex Sum for B (starting with 0)</summary>
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

        /// <summary>Complex Sum for A (starting with 1)</summary>
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
        public static int FilterRound(double number) => (int) Math.Round(number * 16);

        /// <summary>Round Array</summary>
        public static int[] FilterRoundArray(double[] array)
        {
            int[] roundedArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                roundedArray[i] = FilterRound(array[i]);
            }
            return roundedArray;
        }
        
        /// <summary>Mean Square of Error</summary>
        public static double MeanSquareOfError(double[] quants, double[] roundedQuants)
        {
            var fGrid = FrequencyGrid;
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

        /// <summary>Mean Square of Error</summary>
        public static double MeanSquareOfError(double[] aQuants, double[] bQuants, int[] bRoundedQuants, int[] aRoundedQuants)
        {
            var Arq = ToDoubleArray(aRoundedQuants);
            var Brq = ToDoubleArray(bRoundedQuants);

            var fGrid = FrequencyGrid;
            var sum = 0d;
            for (int i = 0; i < 512; i++)
            {
                var Hq = ComplexTransferСoefficient(Arq, Brq, fGrid[i]);
                var H = ComplexTransferСoefficient(aQuants, bQuants, fGrid[i]);
                var summary = Math.Pow(Complex.Abs(Hq - H), 2);
                sum += summary;
            }

            sum = sum / K;

            return sum;
        }

        //helper
        private static double[] ToDoubleArray(int[] array)
        {
            return array.Select(x => (double) x).ToArray();
        }
    }
}
