using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void HandleOpenDichotomy(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Dichotomy());
        }
    }
}
