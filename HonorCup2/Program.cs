using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HonorCup2
{
    class Program
    {
        static void Main()
        {
            //var inputB = Console.ReadLine()?.Split(',');
            //var inputA = Console.ReadLine()?.Split(',');

            //if (!inputA.Contains("") || !inputB.Contains(""))
            //{
            //    IEnumerable<double> InputToNumbers(IEnumerable<string> input)
            //    {
            //        var nums = new List<double>();
            //        foreach (var i in input)
            //        {
            //            if (double.TryParse(i, NumberStyles.Number, CultureInfo.InvariantCulture, out var num))
            //                nums.Add(num);
            //            else
            //                Console.WriteLine("Wrong Input!");
            //        }
            //        return nums;
            //    }

            //    var numbersA = InputToNumbers(inputA);
            //    var numbersB = InputToNumbers(inputB);

            //    foreach (var number in numbersA)
            //    {
            //        Console.WriteLine(number);
            //    }

            //    Filter.FrequencyGrid();

            //    Console.ReadLine();
            //}
            //else
            //{
            //    Console.WriteLine("Wrong Input!");
            //    Console.ReadLine();
            //}

            //Console.WriteLine($"{Complex.Abs(new Complex(1,1) * new Complex(1, 1))}");

            var aArr = new[] {1, 4.8444, 10.3069, -12.2480, 8.5481, -3.3180, 1, 4.8444, 10.3069, -12.2480, 8.5481, -3.3180 };
            var bArr = new[] {0.0007, -0.0001, 0.0012, 0.0001, -0.0001, 0.0007, 0.0007, -0.0001, 0.0012, 0.0001, -0.0001, 0.0007 };

            var aNewArr = Filter.FilterRoundArray(aArr);
            var bNewArr = Filter.FilterRoundArray(bArr);

            var gene = Genetics.Solve(aArr, bArr, aNewArr, bNewArr);
            //var d = newArr.Select(x => (double) x).ToArray();

            //var mse = Filter.MeanSquareOfError(arr, d);
            ////var res = Genetics.MutateValues(arr);

            //foreach (var i in newArr)
            //{
            //    Console.WriteLine($"{i}");
            //}

            Console.WriteLine($"{gene.Fitness}");

            //Console.WriteLine($"{arr.Count(x => x == 0)}");
            Console.ReadLine();
        }
    }
}
