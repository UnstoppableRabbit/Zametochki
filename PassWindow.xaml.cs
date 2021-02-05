using System.Windows;

namespace Sp1sok_del
{
    /// <summary>
    /// Логика взаимодействия для PassWindow.xaml
    /// </summary>
    public partial class PassWindow : Window
    {
        public PassWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string Password
        {
            get { return pw1.Password; }
        }

    }
}
