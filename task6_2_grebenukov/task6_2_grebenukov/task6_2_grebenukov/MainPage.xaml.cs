using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace task6_2_grebenukov
{
    public partial class MainPage : ContentPage
    {
        public List<Movie> movies { get; set; }
        private int selectedQuantity;
        public MainPage()
        {
            InitializeComponent();
            movies = new List<Movie>
            {
                new Movie
                {
                    Title = "Тайна Коко (2017)",
                    Image = "movie7.jpg",
                    Studio = "Pixar Animation Studios",
                    Category = "Приключения/Cемейный",
                    Capacity = 120,
                    Theater = "Гринвич",
                    Hall = "Средний",
                    Seat = "7",
                    Time = "16:30",
                    Date = DateTime.Today,
                    Director = "Ли Анкрич",
                    Year = 2019,
                    Country = "США",
                    Summary = "Мальчик отправляется в мир предков, разгадывая семейную загадку.",
                    TicketPrice = 100,
                },
                new Movie
                {
                        Title = "Побег из Шоушенка (1994)",
                        Image = "movie2.jpg",
                        Studio = "Columbia Pictures",
                        Category = "Драма/Криминал",
                        Capacity = 110,
                        Theater = "Гринвич",
                        Hall = "Большой",
                        Seat = "5",
                        Time = "18:00",
                        Date = DateTime.Today,
                        Director = "Фрэнк Дарабонт",
                        Year = 1994,
                        Country = "США",
                        Summary = "Умный заключенный Анди Дюфрейнт находит способ сбежать из тюрьмы.",
                        TicketPrice = 120,
                },
                new Movie
                {
                    Title = "Зеленая миля (1999)",
                    Image = "movie3.jpg",
                    Studio = "Castle Rock Entertainment",
                    Category = "Драма/Фэнтези",
                    Capacity = 115,
                    Theater = "Пассаж",
                    Hall = "Средний",
                    Seat = "5",
                    Time = "20:15",
                    Date = DateTime.Today,
                    Director = "Фрэнк Дарабонт",
                    Year = 1999,
                    Country = "США",
                    Summary = "Сотрудники тюрьмы сталкиваются с загадочным заключенным с необычными способностями.",
                    TicketPrice = 130,
                },
                new Movie
                {
                    Title = "Форрест Гамп (1994)",
                    Image = "movie4.jpg",
                    Studio = "Paramount Pictures",
                    Category = "Драма/Комедия",
                    Capacity = 120,
                    Theater = "Гринвич",
                    Hall = "Малый",
                    Seat = "2",
                    Time = "22:30",
                    Date = DateTime.Today,
                    Director = "Роберт Земекис",
                    Year = 1994,
                    Country = "США",
                    Summary = "Простодушный Форрест Гамп проживает необычную жизнь, влияя на исторические события.",
                    TicketPrice = 140,
                },
                new Movie
                {
                    Title = "Титаник (1997)",
                    Image = "movie5.jpg",
                    Studio = "20th Century Fox",
                    Category = "Драма/Романтика",
                    Capacity = 125,
                    Theater = "КиноДом",
                    Hall = "Большой",
                    Seat = "1",
                    Time = "16:30",
                    Date = DateTime.Today,
                    Director = "Джеймс Кэмерон",
                    Year = 1997,
                    Country = "США",
                    Summary = "Романтическая история о любви между пассажирами Титаника.",
                    TicketPrice = 150,
                },
                new Movie
                {
                    Title = "Аватар (2009)",
                    Image = "movie6.jpg",
                    Studio = "20th Century Fox",
                    Category = "Научная фантастика/Боевик",
                    Capacity = 130,
                    Theater = "КиноДом",
                    Hall = "Средний",
                    Seat = "3",
                    Time = "19:00",
                    Date = DateTime.Today,
                    Director = "Джеймс Кэмерон",
                    Year = 2009,
                    Country = "США",
                    Summary = "Бывший морской пехотинец вступает в битву за инопланетный мир.",
                    TicketPrice = 160,
                }
            };
            var titleLabel = new Label
            {
                Text = "Онлайн-кинотеатр (покупка билетов)",
                StyleId = "headerlabel"
            };

            var listView = new ListView
            {
                ItemsSource = movies,
                SeparatorVisibility = SeparatorVisibility.Default,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.Red, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "Title");
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Image");
                    return imageCell;
                })
            };
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Title");
            listView.ItemSelected += OnMovieSelected;
            var quantityButton = new Button
            {
                Text = "Выбор количества билетов",
                StyleId = "button"
            };
            quantityButton.Clicked += async (sender, e) =>
            {
                var selectedMovie = listView.SelectedItem as Movie;
                if (selectedMovie != null)
                {
                    var quantityPage = new ChoiceOfTickets(selectedMovie);
                    quantityPage.QuantitySelected += OnQuantitySelected;
                    await Navigation.PushAsync(quantityPage);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Пожалуйста, выберите фильм.", "OK");
                }
            };
            var costButton = new Button
            {
                Text = "Расчет стоимости билетов",
                StyleId = "button"
            };
            costButton.Clicked += async (sender, e) =>
            {
                var selectedMovie = listView.SelectedItem as Movie;
                if (selectedMovie != null)
                {

                        var costPage = new CostCalculation(selectedMovie.TicketPrice, selectedMovie, 1);
                        await Navigation.PushModalAsync(costPage);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Пожалуйста, выберите фильм.", "OK");
                }
            };
            Content = new StackLayout
            {
                Children =
                {
                    titleLabel,
                    listView,
                    quantityButton,
                    costButton
                }
            };
        }
        private async void OnQuantitySelected(object sender, int quantity)
        {
            selectedQuantity = quantity;
            await Navigation.PopAsync();
        }
        async void OnMovieSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedMovie = e.SelectedItem as Movie;
            if (selectedMovie != null)
            {
                await DisplayAlert("Информация о фильме", $"Название: {selectedMovie.Title}\n" +
                                                         $"Киностудия: {selectedMovie.Studio}\n" +
                                                         $"Категория: {selectedMovie.Category}\n" +
                                                         $"Вместимость: {selectedMovie.Capacity}\n" +
                                                         $"Кинотеатр: {selectedMovie.Theater}\n" +
                                                         $"Зал: {selectedMovie.Hall}\n" +
                                                         $"Место: {selectedMovie.Seat}\n" +
                                                         $"Время: {selectedMovie.Time}\n" +
                                                         $"Дата: {selectedMovie.Date}\n" +
                                                         $"Режиссер: {selectedMovie.Director}\n" +
                                                          $"Год выпуска: {selectedMovie.Year}\n" +
                                                         $"Страна: {selectedMovie.Country}\n" +
                                                         $"Краткое содержание: {selectedMovie.Summary}\n" +
                                                          $"Цена билета: {selectedMovie.TicketPrice}\n"

                                                         , "OK");

            }
        }
    }
}
