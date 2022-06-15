using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace Saper_Game.Classes
{
    public class Functionality
    {
        //Поля
        private static int count_points = 0;
        private static bool win_rezult;
        private static Button [,] game_pole= new Button[8,10];

        //Свойства
        public static int Count_points { get => count_points; set => count_points = value;}
        public static bool Win_rezult { get => win_rezult; set => win_rezult = value; }
        public static Button [,] Game_pole { get => game_pole; set => game_pole = value; }

        //Методы

        //Метод для создания игрового поля
        public static void Create_Array(WrapPanel button_Panel)
        {
            //Создаем лист кнопок
            List<Button> list_Button = new List<Button>();
            foreach (Button item in button_Panel.Children)
            {
                list_Button.Add(item);
            }

            //Добавляем бомбы
            Set_Bombs(list_Button);

            //Заполнение массива кнопками
            int number_button = 0;
            for (int i = 0; i < game_pole.GetLength(0); i++)
            {
                for (int j = 0; j < game_pole.GetLength(1); j++)
                {
                    game_pole[i, j] = list_Button[number_button];
                    number_button++;
                }
            }

        }

        //Метод для создания бомб
        private static void Set_Bombs(List<Button> list_button)
        {
            //Создание массива случайных числе от 0 до 79 без повторений
            Random random = new Random();
            int[] random_numbers=new int [10];
            for (int i = 0; i < random_numbers.Length;)
            {
                int random_number = random.Next(0, 79);
                if (random_numbers.Contains(random_number)==false)
                    {
                    random_numbers[i] = random_number;
                    i++;
                    }
            }

            //Создание бомб через Uid
            for (int i = 0; i < random_numbers.Length; i++)
            {
                list_button[random_numbers[i]].Uid = "1";
            }
        }

        //Метод завершения игры
        public static void End_Game()
        {
            //Вывод всех мин и отключение кнопок
            for (int i = 0; i < game_pole.GetLength(0); i++)
            {
                for (int j = 0; j  < game_pole.GetLength(1); j ++)
                {
                    if(game_pole[i,j].Uid=="1")
                    {
                        game_pole[i, j].Content = new Image { Source=(new BitmapImage(new Uri("Source/bomb.png", UriKind.Relative)))};
                    }
                    game_pole[i, j].IsEnabled = false;
                }
            }

            //Оповещение пользователя о результате
            if (win_rezult == false)
            {
                MessageBox.Show($"Вы проиграли!\nКоличество очков: {Count_points}");
            }
            else
            {
                MessageBox.Show($"Вы победили!\nКоличество очков: {Count_points}");
            }
        }

        //Метод, ищущий количество бомб вокруг клетки
        public static int Select_Count_Bombs(string name_button)
        {
            int count_bombs = 0;

            //Получаем строку и столбец нужной нам кнопки
            int row = 0;
            int column = 0;
            for (int i = 0; i < game_pole.GetLength(0); i++)
            {
                for (int j = 0; j < game_pole.GetLength(1); j++)
                {
                    if(game_pole[i,j].Name==name_button)
                    {
                        row = i;
                        column = j;
                    }
                }
            }

            //Подсчитываем количество мин вокруг клетки
            for (int i = row-1; i <= row+1; i++)
            {
                for (int j = column-1; j <= column+1; j++)
                {
                    if(i>=0 && i<game_pole.GetLength(0) && j>=0 && j<game_pole.GetLength(1))
                    {
                        if(game_pole[i,j].Uid=="1")
                        {
                            count_bombs++;
                        }
                    }
                }
            }

            //Возвращаем количество бомб
            return count_bombs;
        }
    }
}
