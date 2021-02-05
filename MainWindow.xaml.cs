using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sp1sok_del
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            myTabContrl.ItemContainerStyle = s;

            ListUpdate();
        }
        private void Item_Click(object sender, MouseButtonEventArgs e)
        {
            string pass;
            using (DelaContext ncon = new DelaContext())
            {
                     pass = (from dela in ncon.Delas
                            where dela.Title.Equals(qwe.SelectedItem.ToString())
                            select dela.Password
                            ).FirstOrDefault().ToString();
            }
           if (string.IsNullOrWhiteSpace(pass) || pass == "")
               itemclk();
            else
            {
                PassWindow pw = new PassWindow();
                if (pw.ShowDialog() == true)
                {
                    if (pw.Password == pass)
                        itemclk();
                    else
                        MessageBox.Show("Неверный пароль");
                }
            }
        }

        void itemclk()
        {
            abc.IsReadOnly = false;
            deleteButton.Visibility = Visibility;
            saveButton.Visibility = Visibility;
            var k = qwe.SelectedItem.ToString();
            using (DelaContext ncon = new DelaContext())
            {
                var delo = (from dela in ncon.Delas
                            where dela.Title.Equals(k)
                            select dela.Delo
                             ).FirstOrDefault().ToString();
                abc.Text = delo;

                var title = (from dela in ncon.Delas
                             where dela.Title.Equals(k)
                             select dela.Title).FirstOrDefault().ToString();
                titleTblock.Text = title;

                var date = (from dela in ncon.Delas
                            where dela.Title.Equals(k)
                            select dela.DateTime).FirstOrDefault().ToString();
                dateTblock.Text = date;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            myTabContrl.SelectedIndex = 1;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            myTabContrl.SelectedIndex = 0;
        }

        private void deleteButton1_Click(object sender, RoutedEventArgs e)
        {
            Tab2Clear();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DelaContext ncon = new DelaContext())
                {
                    var zxc = (from o in ncon.Delas
                               where o.Title == titleTblock.Text
                               select o).First<Delas>();
                    zxc.Delo = abc.Text;
                    ncon.SaveChangesAsync();
                }
                ListUpdate();
            }
            catch (Exception)
            {
                MessageBox.Show("Нечего сохранять");
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить заметку?","Удаление заметки",
                                                      MessageBoxButton.YesNo , MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                using (DelaContext ncon = new DelaContext())
                {
                    var zxc = (from o in ncon.Delas
                               where o.Title == titleTblock.Text
                               select o).FirstOrDefault();
                    ncon.Delas.Remove(zxc);
                    ncon.SaveChanges();
                }
                ListUpdate();
                abc.Text = "Выберете заметку";
                abc.IsReadOnly = true;
                titleTblock.Text = "";
                dateTblock.Text = "";
                deleteButton.Visibility = Visibility.Hidden;
                saveButton.Visibility = Visibility.Hidden;
            }
            else
            {
                // Пользователь отменил действие.
            }
        }
            void ListUpdate()
            {
                using (DelaContext ncon = new DelaContext())
                {
                   var name = (from dela in ncon.Delas
                            select dela.Title).ToList();

                   qwe.ItemsSource = name;
                }
            }

        void Tab2Clear()
        {
            mainTbox.Text = "";
            AddNameTBox.Text = "";
            AddPassTBox.Text = "";
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            using (DelaContext ncon = new DelaContext())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(AddNameTBox.Text))
                    {
                        var zametka = new Delas()
                        {
                            Title = AddNameTBox.Text,
                            Delo = mainTbox.Text,
                            DateTime = DateTime.Now.ToString("G"),
                            Password = AddPassTBox.Text
                        };
                        ncon.Delas.Add(zametka);
                        ncon.SaveChanges();
                        ListUpdate();
                        Tab2Clear();
                        myTabContrl.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Дайте заметке название");
                    }
                }
                catch
                {
                    MessageBox.Show("Название должно быть уникальным");
                }
            }  
        }

        private void abc_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                int f = abc.SelectionStart;
                abc.Text= abc.Text.Insert(f,"\n");
                abc.SelectionStart = f + 1;
            }
        }

        private void mainTbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int f = mainTbox.SelectionStart;
                mainTbox.Text = mainTbox.Text.Insert(f, "\n");
                mainTbox.SelectionStart = f + 1;
            }
        }
    }
}
