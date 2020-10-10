using PT__Lab1;
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

        public static List<HistoColumn> getBudgets(IEnumerable<Movie> values)
        {
            List<HistoColumn> res = new List<HistoColumn>();
            Int64 max = 0;
            foreach (var p in values)
            {
                if (p.budget > max) max = p.budget;
            }
            foreach (var p in values)
            {
                res.Add(new HistoColumn() { Title = p.title, Height = ((float)p.budget / (float)max) * 200 });
            }
            return res;
        }
        public static List<double> getBudgetsNum(IEnumerable<Movie> values)
        {
            List<double> res = new List<double>();
            foreach (var p in values)
            {
                res.Add(p.budget);
            }
            return res;
        }
        public static List<double> getRevenuesNum(IEnumerable<Movie> values)
        {
            List<double> res = new List<double>();
            foreach (var p in values)
            {
                res.Add(p.revenue);
            }
            return res;
        }
        public static List<HistoColumn> getRevenues(IEnumerable<Movie> values)
        {
            List<HistoColumn> res = new List<HistoColumn>();
            Int64 max = 0;
            foreach (var p in values)
            {
                if (p.revenue > max) max = p.revenue;
            }
            foreach (var p in values)
            {
                res.Add(new HistoColumn() { Title = p.title, Height = ((float)p.revenue / (float)max) * 200 });
            }
            return res;
        }
        public static List<HistoColumn> getGenres(IEnumerable<Movie> values)
        {
            List<HistoColumn> res = new List<HistoColumn>();
            Int64 max = 0;
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var p in values)
            {
                foreach(var i in p.genres)
                {
                    if (dict.ContainsKey(i.name))
                    {
                        dict[i.name]++;
                        if (dict[i.name] > max) max = dict[i.name];
                    }
                    else dict.Add(i.name, 1);
                }
            }
            foreach (var p in dict)
            {
                res.Add(new HistoColumn() { Title = p.Key, Height = ((float)p.Value / (float)max) * 200 });
            }
            return res;
        }

        public static List<HistoColumn> getWR(IEnumerable<Movie> values)
        {
            long minVotes = Int64.MaxValue;
            double sum = 0;
            foreach(var p in values)
            {
                if (p.vote_count < minVotes) minVotes = p.vote_count;
                sum += p.vote_average;
            }
            sum /= values.Count();
            List<HistoColumn> res = new List<HistoColumn>();
            foreach (var p in values)
            {
                double wr;
                wr = p.vote_count + minVotes != 0 ? (p.vote_count / (p.vote_count + minVotes)) * p.vote_average + (p.vote_count / (p.vote_count + minVotes)) * sum : 0;
                res.Add(new HistoColumn() { Title = p.title, Height = wr });
            }
            return res;
        }
        public static List<HistoColumn> getWRM(IEnumerable<Movie> values)
        {
            long minVotes = Int64.MaxValue;
            List<double> sum = new List<double>();
            foreach (var p in values)
            {
                if (p.vote_count < minVotes) minVotes = p.vote_count;
                sum.Add(p.vote_average);
            }
            List<HistoColumn> res = new List<HistoColumn>();
            foreach (var p in values)
            {
                double wr;
                wr = p.vote_count + minVotes != 0 ? (p.vote_count / (p.vote_count + minVotes)) * p.vote_average + (p.vote_count / (p.vote_count + minVotes)) * Median(sum) : 0;
                res.Add(new HistoColumn() { Title = p.title, Height = wr });
            }
            return res;
        }
        public static List<Movie> FindByGenre(IEnumerable<Movie> values, string Genre)
        {
            List<Movie> res = new List<Movie>();
            foreach(var p in values)
            {
                foreach(var i in p.genres)
                {
                    if (i.name == Genre)
                    {
                        res.Add(p);
                        break;
                    }
                }
            }
            return res;
        }
        public static List<HistoColumn> Cut(List<HistoColumn> values, int num)
        {
            var t = values.OrderByDescending(x => x.Height).ToList();
            var res = new List<HistoColumn>();
            for(int i = 0; i < num; i++)
            {
                res.Add(t[i]);
            }
            return res;
        }
    }



public class HistoColumn
    {
        public string Title { get; set; }
        public double Height { get; set; }
    }
}