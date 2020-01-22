using docker_wpf_exercise_tklecka.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;


namespace wpf_exercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        static readonly HttpClient HttpClient = new HttpClient();
        public DateTime Date { get; set; } = DateTime.Now;
        public MainWindow()
        {
            HttpClient.BaseAddress = new Uri("http://localhost:5000/api/");
            DataContext = this;
            InitializeComponent();
            GetCars();
        }

        public async void GetCars()
        {
            Cars.Clear();
            var resp = await HttpClient.GetAsync("cars");
            JsonSerializer.Deserialize<List<Car>>(await resp.Content.ReadAsStringAsync()).ForEach(c => Cars.Add(c));
        }

        public async void GetCarsForDay()
        {
            Cars.Clear();
            var resp = await HttpClient.GetAsync("reservations/date/" + Date.Day + "." + Date.Month + "." + Date.Year + "");
            JsonSerializer.Deserialize<List<Car>>(await resp.Content.ReadAsStringAsync()).ForEach(c => Cars.Add(c));
        }

        public async void ReservateCar(int carid)
        {
            DateTime date = new DateTime(Date.Year, Date.Month, Date.Day);
            Reservation reservation = new Reservation { Car = null, CarID = carid, ReservationDay = date };
            var response = await HttpClient.PostAsync("reservations", new StringContent(JsonSerializer.Serialize(reservation), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error: " + response);
                MessageBox.Show("Could not Book Car", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetCarsForDay();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetCars();
        }

        public void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ReservateCar(Int32.Parse(((Button)sender).Tag.ToString()));
            GetCarsForDay();
        }
    }
}
