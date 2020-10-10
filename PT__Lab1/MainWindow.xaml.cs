using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PT_Lab1;

namespace PT__Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ObservableCollection<Movie> movies = CSVReader.ReadMovies(AppDomain.CurrentDomain.BaseDirectory + @"\tmdb_5000_movies.csv");
        ObservableCollection<Credit> credits = CSVReader.ReadCredits(AppDomain.CurrentDomain.BaseDirectory + @"\tmdb_5000_credits.csv");
        public List<double> allBudgets = Controller.getBudgetsNum(movies);
        public List<double> allRevenues = Controller.getRevenuesNum(movies);
        public MainWindow()
        {
            InitializeComponent();
            var moviesTitles = new List<string>();
            var creditsList = new List<string>();
            foreach (var p in movies)
            {
                moviesTitles.Add(p.title);
            }
            foreach(var p in credits)
            {
                creditsList.Add(p.Title);
            }
            MovieCB.ItemsSource = moviesTitles;
            CreditsCB.ItemsSource = creditsList;

            //List<double> test = new List<double>();
            //test.Add(2);
            //for(int i = 1; i < 10000; i++)
            //{
            //    test.Add(i);
            //}
            
            BudgetsHisto.ItemsSource = Controller.getBudgets(movies);
            RevenueHisto.ItemsSource = Controller.getRevenues(movies);
            GenresHisto.ItemsSource = Controller.getGenres(movies);
            AverageBudgetBox.Text = Convert.ToString(Controller.Average(allBudgets));
            AverageRevenueBox.Text = Convert.ToString(Controller.Average(allRevenues));
            DispersionBudgetBox.Text = Convert.ToString(Controller.Dispersion(allBudgets));
            DispersionRevenueBox.Text = Convert.ToString(Controller.Dispersion(allRevenues));
            DivergenceBudgetBox.Text = Convert.ToString(Controller.Divergence(allBudgets));
            DivergenceRevenueBox.Text = Convert.ToString(Controller.Divergence(allRevenues));
            MedianBudgetBox.Text = Convert.ToString(Controller.Median(allBudgets));
            MedianRevenueBox.Text = Convert.ToString(Controller.Median(allRevenues));
            TrendBudgetBox.Text = String.Join(", ", Controller.Trend(allBudgets));
            TrendRevenueBox.Text = String.Join(", ", Controller.Trend(allRevenues));
            MaxBudgetBox.Text = Convert.ToString(Controller.Max(allBudgets));
            MaxRevenueBox.Text = Convert.ToString(Controller.Max(allRevenues));
            MinBudgetBox.Text = Convert.ToString(Controller.Min(allBudgets));
            MinRevenueBox.Text = Convert.ToString(Controller.Min(allRevenues));
            DifferenceBudgetBox.Text = Convert.ToString(Controller.Difference(allBudgets));
            DifferenceRevenueBox.Text = Convert.ToString(Controller.Difference(allRevenues));
            var t = Controller.getWR(movies);
            var t1 = Controller.getWRM(movies);
            WRDG.DataContext = Controller.Cut(t, 10);
            WRMDG.DataContext = Controller.Cut(t1, 10);
            ActionDG.DataContext = Controller.Cut(Controller.getWR(Controller.FindByGenre(movies, "Action")), 5);
            ActionMDG.DataContext = Controller.Cut(Controller.getWRM(Controller.FindByGenre(movies, "Action")), 5);
            AdventureDG.DataContext = Controller.Cut(Controller.getWR(Controller.FindByGenre(movies, "Adventure")), 5);
            AdventureMDG.DataContext = Controller.Cut(Controller.getWRM(Controller.FindByGenre(movies, "Adventure")), 5);
            FantasyDG.DataContext = Controller.Cut(Controller.getWR(Controller.FindByGenre(movies, "Fantasy")), 5);
            FantasyMDG.DataContext = Controller.Cut(Controller.getWRM(Controller.FindByGenre(movies, "Fantasy")), 5);
            ScienceFictionDG.DataContext = Controller.Cut(Controller.getWR(Controller.FindByGenre(movies, "Science Fiction")), 5);
            ScienceFictionMDG.DataContext = Controller.Cut(Controller.getWRM(Controller.FindByGenre(movies, "Science Fiction")), 5);
            CrimeDG.DataContext = Controller.Cut(Controller.getWR(Controller.FindByGenre(movies, "Crime")), 5);
            CrimeMDG.DataContext = Controller.Cut(Controller.getWRM(Controller.FindByGenre(movies, "Crime")), 5);
        }

        private void CreditsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCredit = credits[CreditsCB.SelectedIndex];
            movieIdBox.Text = selectedCredit.MovieId.ToString();
            movieTitleBox.Text = selectedCredit.Title;
            CastDG.DataContext = selectedCredit.Cast;
            CrewDG.DataContext = selectedCredit.Crew;
        }

        private void MovieCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMovie = movies[MovieCB.SelectedIndex];
            BudgetBox.Text = selectedMovie.budget.ToString();
            string s = "";
            foreach(var p in selectedMovie.genres)
            {
                s += p.name + ", ";
            }
            GenresBox.Text = s.Length > 0 ? s.Substring(0, s.Length - 2) : "";
            if (selectedMovie.homepage.Length > 0) HomepageBox.NavigateUri = new Uri(selectedMovie.homepage);
            else HomepageBox.NavigateUri = null;
            HomepageBox.Inlines.Clear();
            HomepageBox.Inlines.Add(selectedMovie.homepage);
            IdBox.Text = selectedMovie.id.ToString();
            s = "";
            foreach(var p in selectedMovie.keywords)
            {
                s += p.name + ", ";
            }
            KeywordsBox.Text = s.Length > 0 ? s.Substring(0, s.Length - 2) : "";
            OLBox.Text = selectedMovie.original_language;
            OTBox.Text = selectedMovie.original_title;
            OverviewBox.Text = selectedMovie.overview;
            PopularityBox.Text = selectedMovie.popularity.ToString();
            s = "";
            foreach (var p in selectedMovie.production_companies)
            {
                s += p.name + ", ";
            }
            ProdCompaniesBox.Text = s.Length > 0 ? s.Substring(0, s.Length - 2) : "";
            s = "";
            foreach (var p in selectedMovie.production_countries)
            {
                s += p.name + ", ";
            }
            ProdCountriesBox.Text = s.Length > 0 ? s.Substring(0, s.Length - 2) : "";
            ReleaseDateBox.Text = Convert.ToString(selectedMovie.release_date);
            RevenueBox.Text = selectedMovie.revenue.ToString();
            RuntimeBox.Text = selectedMovie.runtime.ToString();
            s = "";
            foreach (var p in selectedMovie.spoken_languages)
            {
                s += p.name + ", ";
            }
            SLBox.Text = s.Length > 0 ? s.Substring(0, s.Length - 2) : "";
            StatusBox.Text = selectedMovie.status;
            TaglineBox.Text = selectedMovie.tagline;
            TitleBox.Text = selectedMovie.title;
            RateBox.Text = selectedMovie.vote_average.ToString();
            VotesAmountBox.Text = selectedMovie.vote_count.ToString();

            CreditsCB.SelectedIndex = MovieCB.SelectedIndex;
        }

        private void HomepageBox_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void QuantileBudget_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(QuantileBudget.Text.Length > 0) QBBox.Text = Convert.ToString(Controller.Quantile(allBudgets, Convert.ToDouble(QuantileBudget.Text)));
        }

        private void QuantileRevenue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (QuantileRevenue.Text.Length > 0) QRBox.Text = Convert.ToString(Controller.Quantile(allRevenues, Convert.ToDouble(QuantileRevenue.Text)));
        }
    }
}
