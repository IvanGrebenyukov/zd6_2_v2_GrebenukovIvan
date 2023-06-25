using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace task6_2_grebenukov
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class ChoiceOfTickets : ContentPage
    {
        public event EventHandler<int> QuantitySelected;
        private int selectedQuantity { get; set; }
        private Movie movie;
        public Movie SelectedMovie { get; set; }
        public ChoiceOfTickets(Movie selectedMovie)
        {
            InitializeComponent();
            this.SelectedMovie = selectedMovie;
            movie = selectedMovie;
            var titleLabel = new Label
            {
                Text = "Выбор количества билетов",
                StyleId = "headerlabel"

            };
            var movieImage = new Image
            {
                Source = selectedMovie.Image,
                Aspect = Aspect.AspectFit,
                HeightRequest = 200
            };
            var movieLabel = new Label
            {
                Text = $"Фильм: {movie.Title}\nКоличество мест: {movie.Capacity}\nВремя: {movie.Time}\n{movie.Summary}",
                StyleId = "label"

            };
            var quantityEntry = new Entry
            {
                Placeholder = "Введите количество билетов",
                Keyboard = Keyboard.Numeric,
                StyleId = "entry"
                
            };

            var selectButton = new Button
            {
                Text = "Выбрать",
                HorizontalOptions = LayoutOptions.Center,
                StyleId = "button"
            };

            selectButton.Clicked += (sender, e) =>
            {
                if (int.TryParse(quantityEntry.Text, out int quantity))
                {
                    if (quantity <= movie.Capacity)
                    {
                        selectedQuantity = quantity;
                        var costPage = new CostCalculation(selectedMovie.TicketPrice, selectedMovie, int.Parse(quantityEntry.Text));
                        Navigation.PushModalAsync(costPage);
                    }
                    else
                    {
                        DisplayAlert("Ошибка", "Количество выбранных билетов превышает вместимость", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Ошибка", "Некорректное количество билетов", "OK");
                }
            };

            Content = new StackLayout
            {
                Children =
                {
                    titleLabel,
                    movieLabel,
                    movieImage,
                    quantityEntry,
                    selectButton
                },
                BackgroundColor = Color.White
            };
        }
    }
}