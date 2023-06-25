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
    public partial class CostCalculation : ContentPage
    {
        private int selectedQuantity;
        private double ticketPrice;
        private int quantityChildren;
        private int quantityPensioners;
        private int quantityStudents;
        private Movie movie;
        public CostCalculation(double price, Movie movie, int quantity)
        {
            InitializeComponent();

            selectedQuantity = quantity;
            ticketPrice = price;
            quantityChildren = 0;
            quantityPensioners = 0;
            quantityStudents = 0;
            this.movie = movie;
            var childrenEntry = new Entry
            {
                Placeholder = "Количество детей",
                Keyboard = Keyboard.Numeric,
                StyleId = "entry"
            };

            var pensionersEntry = new Entry
            {
                Placeholder = "Количество пенсионеров",
                Keyboard = Keyboard.Numeric,
                StyleId = "entry"
            };

            var studentsEntry = new Entry
            {
                Placeholder = "Количество студентов и учащихся",
                Keyboard = Keyboard.Numeric,
                StyleId = "entry"
            };

            var titleLabel = new Label
            {
                Text = "Расчет стоимости билетов",
                StyleId = "headerlabel"
            };

            var movieLabel = new Label
            {
                Text = $"Фильм: {movie.Title}\n\n{movie.Summary}",
                StyleId = "label"
            };
            var QuantityLabel = new Label
            {
                Text = $"Вы выбрали: {quantity} билетов",
                StyleId = "label"
            };
            var priceLabel = new Label
            {
                Text = $"Цена билета: {movie.TicketPrice:C}",
                StyleId = "label"
            };

            var calculateButton = new Button
            {
                Text = "Купить",
                StyleId = "button"
            };

            var resultLabel = new Label
            {
                Text = "",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };
            calculateButton.Clicked += async (sender, e) =>
            {
                if (!int.TryParse(childrenEntry.Text, out quantityChildren) ||
                    !int.TryParse(pensionersEntry.Text, out quantityPensioners) ||
                    !int.TryParse(studentsEntry.Text, out quantityStudents))
                {
                    await DisplayAlert("Ошибка", "Пожалуйста, введите правильное количество людей", "OK");
                    return;
                }
                if(int.Parse(childrenEntry.Text) < 0 || int.Parse(pensionersEntry.Text) < 0 || int.Parse(studentsEntry.Text) < 0)
                {
                    await DisplayAlert("Ошибка", "Пожалуйста, введите правильное количество людей", "OK");
                    return;
                }

                int totalPeopleDiscount = quantityChildren + quantityPensioners + quantityStudents;
                int PeopleNotDickount = selectedQuantity - totalPeopleDiscount;
                if (totalPeopleDiscount > selectedQuantity)
                {
                    await DisplayAlert("Ошибка", "Сумма введенного количества людей больше, чем билетов", "OK");
                    return;
                }
                int quantityMultichildFamilies = quantityChildren / 3;
                double totalCost = CalculateTotalCost(PeopleNotDickount, quantityMultichildFamilies);
                resultLabel.Text = $"Обычные билеты: {PeopleNotDickount} - {ticketPrice * 1 * PeopleNotDickount} рублей\n" +
                                   $"Дети (30%): {quantityChildren} - {ticketPrice * 0.7 * quantityChildren} рублей\n" +
                                   $"Пенсионеры (20%): {quantityPensioners} - {ticketPrice * 0.8 * quantityPensioners} рублей\n" +
                                   $"Студенты и учащиеся (20%): {quantityStudents} - {ticketPrice * 0.8 * quantityStudents} рублей\n" +
                                   $"Многодетные семьи (10%): {quantityMultichildFamilies} - скидка {ticketPrice * 0.9 * quantityMultichildFamilies} рублей\n" +
                                   $"Общая стоимость: {totalCost} рублей";

            };

            double CalculateTotalCost(int PeopleNotDickount, int quantityMultichildFamilies)
            {
                double totalCost = 0;
                double costChildren = 0;
                if(quantityMultichildFamilies > 0)
                {
                    costChildren = ticketPrice * 0.7 * quantityChildren;
                    costChildren = costChildren * 0.9;
                }
                else
                {
                    costChildren = ticketPrice * 0.7 * quantityChildren;
                }
                totalCost += costChildren;
                totalCost += ticketPrice * 0.8 * quantityPensioners;
                totalCost += ticketPrice * 0.8 * quantityStudents;
                totalCost += ticketPrice * 1 * PeopleNotDickount;

                return totalCost;
            }

            Content = new StackLayout
            {
                BackgroundColor = Color.White,
                Children =
                {
                    titleLabel,
                    movieLabel,
                    QuantityLabel,
                    priceLabel,
                    childrenEntry,
                    pensionersEntry,
                    studentsEntry,
                    calculateButton,
                    resultLabel
                }
            };
        }
    }
}