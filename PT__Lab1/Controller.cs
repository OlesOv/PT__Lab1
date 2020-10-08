using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PT_Lab1
{
    public static class Controller
    {
        public static double Average(IEnumerable<double> values)
        {
            //всередині звичайний foreach, який рахує суму, кількість, і ділить перше на друге
            return values.Average();
        }

        public static bool IsAverage(IEnumerable<double> values, double supposedAverage)
        {
            double sum = 0;
            foreach (var p in values)
            {
                sum += supposedAverage - p;
            }

            //Комп'ютер не вміє мислити настільки абстрактно, як ми, і у нього є обмеження в точності обрахунку.
            //Точність того типу даних, що ми використовуємо - 16 знаків після крапки
            return Math.Abs(sum) < 1e-15;
        }

        public static double Dispersion(IEnumerable<double> values)
        {
            double average = Average(values), sum = 0;
            foreach (var p in values)
            {
                sum += Math.Pow(p - average, 2);
            }

            return sum / (values.Count() - 1);
        }

        public static double Divergence(IEnumerable<double> values)
        {
            //Корінь з дисперсії
            return Math.Sqrt(Dispersion(values));
        }

        public static List<double> Trend(IEnumerable<double> values)
        {
            //рахуємо, скільки разів яке значення зустрічається
            Dictionary<double, int> dict = new Dictionary<double, int>();
            foreach (var p in values)
            {
                if (dict.ContainsKey(p))
                {
                    dict[p]++;
                }
                else dict.Add(p, 1);
            }

            //Повертаємо ті значення, яке зустрічається найчастіше
            var maxNum = dict.First();
            List<double> res = new List<double>();
            foreach (var p in dict)
            {
                if (p.Value > maxNum.Value)
                {
                    maxNum = p;
                    res.Clear();
                    res.Add(p.Key);
                }
                else if (p.Value == maxNum.Value)
                {
                    res.Add(p.Key);
                }
            }

            return res;
        }

        public static double Median(IEnumerable<double> values)
        {
            //повертаємо квантиль 0.5
            return Quantile(values, 0.5);
        }

        public static double Max(IEnumerable<double> values)
        {
            //всередині звичайний foreach, який шукає максимальне
            return values.Max();
        }

        public static double Min(IEnumerable<double> values)
        {
            //всередині звичайний foreach, який шукає мінімальне
            return values.Min();
        }

        public static double Difference(IEnumerable<double> values)
        {
            return Max(values) - Min(values);
        }

        public static double Quantile(IEnumerable<double> values, double quan)
        {
            //переганяємо в масив, сортуємо, і повертаємо значення у вказаній точці
            var arr = values.ToArray();
            Array.Sort(arr);
            return arr[Convert.ToInt32(arr.Length * quan)];
        }
    }
}