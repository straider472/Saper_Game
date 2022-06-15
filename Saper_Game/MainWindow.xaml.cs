using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
namespace Saper_Game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Присваивание события всем кнопкам во WrapPanel
            foreach (var item in Button_Panel.Children)
            {
                ((Button)item).Click += Button_Click;
            }

            //Создание поля (массива кнопок)
            Classes.Functionality.Create_Array(Button_Panel);
        }

        //Событие при нажатии на кнопку
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //В зависиомсти от выбранного поля, либо взорвётся мина, либо добавится очко
            var button = sender as Button;
            if (button.Uid == "1")
            {
                Classes.Functionality.Win_rezult = false;
                Classes.Functionality.End_Game();
            }
            else
            {
                button.Content=Classes.Functionality.Select_Count_Bombs(button.Name);
                Classes.Functionality.Count_points++;
                Text_Points.Text = Classes.Functionality.Count_points.ToString();
                button.IsEnabled = false;
            }

            //Вызов победы в случае, если количество очков станет равным 70
            if(Classes.Functionality.Count_points==70)
            {
                Classes.Functionality.Win_rezult = true;
                Classes.Functionality.End_Game();
            }
        }


        //Рестарт игры
        private void Restart_Game_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
