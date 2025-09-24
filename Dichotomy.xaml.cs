using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class Dichotomy : Page
    {
        private string fun { get; set; }
        private double rangeA { get; set; }
        private double rangeB { get; set; }
        private double accuracy { get; set; }

        public Dichotomy()
        {
            InitializeComponent();
        }

        private void HandleBackMenu(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Menu());
        }

        private void TextBox_F(object sender, TextChangedEventArgs e)
        {
            fun = ((TextBox)sender).Text;
        }

        private void TextBox_NumberChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            string input = textBox.Text.Replace('.', ',');
            if (!double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                value = 0.0;
                Console.WriteLine("Недопустимый формат данных!");
            }

            switch (textBox.Tag?.ToString())
            {
                case "A":
                    rangeA = value;
                    break;

                case "B":
                    rangeB = value;
                    break;

                case "E":
                    accuracy = value;
                    break;
            }
        }

        private void HandleCalculate(object sender, RoutedEventArgs e)
        {
            DichtomyMethod dichtomyMethod = new DichtomyMethod(fun, rangeA, rangeB, accuracy);

            if (!string.IsNullOrEmpty(fun) && accuracy > 0) 
            {
                double root = dichtomyMethod.Solve();

                if (root != 0 || (rangeA == 0 || rangeB == 0))
                {
                    ChartBuilder chart = new ChartBuilder(fun);
                    chart.Draw(ChartLine, rangeA, rangeB);
                    chart.DrawRootPoint(ChartLine, root);
                    inputRoot.Text = root.ToString();
                }
                else
                {
                    Console.WriteLine("Корень не найден");
                }
            }
            else
            {
                Console.WriteLine("Введите данные!");
            }
        }
    }
}
