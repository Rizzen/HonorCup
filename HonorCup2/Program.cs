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

            var arr = new[] {0.5, -0.0001, 0.0012, 0.07, -0.0001, 0.0007};

            var newArr = Filter.FilterRoundArray(arr);

            Genetics.Solve(arr, newArr);
            //var d = newArr.Select(x => (double) x).ToArray();

            //var mse = Filter.MeanSquareOfError(arr, d);
            ////var res = Genetics.MutateValues(arr);

            //foreach (var i in newArr)
            //{
            //    Console.WriteLine($"{i}");
            //}

            //Console.WriteLine($"{mse}");

            //Console.WriteLine($"{arr.Count(x => x == 0)}");
            Console.ReadLine();
        }
    }
}
