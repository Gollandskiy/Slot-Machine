using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine
{
    class Menu
    {
        // Массив пунктов меню
        private string[] menuOptions;
        // Выбранный индекс
        private int selectedIndex;
        // Массив действий выбранного параметра
        private Action[] menuActions;

        public Menu(string[] options)
        {
            menuOptions = options;
            selectedIndex = 0;
            menuActions = new Action[options.Length];
        }

        public void SetAction(int index, Action action)
        {
            // Проверка индекса(находится ли в допустимом диапазоне)
            if (index >= 0 && index < menuOptions.Length)
            {
                // Присвоение индексу переданное действие
                menuActions[index] = action;
            }
        }

        public void Start()
        {
            // Скрываем курсор
            Console.CursorVisible = false;
            ConsoleKeyInfo keyInfo;

            do
            {
                // Очищаем консоль
                Console.Clear();
                // Отображение меню
                DisplayMenu();

                // Читаем кнопочку
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    // Изменяем индекс если пользователь жмет стрелочку вверх
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + menuOptions.Length) % menuOptions.Length;
                        break;
                    // Тоже меняем, но с другой кнопкой
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % menuOptions.Length;
                        break;
                    // Вызываем действие которое выбрал пользователь
                    case ConsoleKey.Enter:
                        Console.Clear();
                        ActivateMenuItem();
                        break;
                }
                // Продолжаем до тех пор, пока не будет нажата клавиша Escape
            } while (keyInfo.Key != ConsoleKey.Escape);

            // Восстанавливаем видимость курсора
            Console.CursorVisible = true; 
        }
        // Простое отображение названия игры
        public void DisplayName()
        {
            // Меняем на желтый
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    \t\t███████    ██╗         ██████╗   ████████                   ███╗   ███╗    ██");
            Console.WriteLine("    \t\t██╔═══╝    ██╗        ██╔═══██╗     ██╗                     ████╗ ████║    ██");
            Console.WriteLine("    \t\t███████╗   ██╗        ██║   ██║     ██║     ██████████      ██╔████╔██║    ██");
            Console.WriteLine("    \t\t╚════██║   ██╗        ██║   ██║     ██║                     ██║╚██╔╝██║    ██");
            Console.WriteLine("    \t\t███████║   █████████  ╚██████╔╝     ██║                     ██║ ╚═╝ ██║    ");
            Console.WriteLine("    \t\t╚══════╝   ╚═╝╚═╝╚═╝   ╚═════╝      ╚═╝                     ╚═╝     ╚═╝    ██");
            Console.ResetColor();
            // Тут тоже
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\tНАЖМИТЕ ЛЮБУЮ КНОПКУ ДЛЯ ПРОДОЛЖЕНИЯ!");
            // Ждем пока пользователь нажмет любую кнопку
            Console.ReadKey();
        }


        private void DisplayMenu()
        {
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    // Показываем что был выбран какой-то пункт
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    // Сбросить предыдущие цвета, если они были заданы
                    Console.ResetColor();
                    // Задать цвет для остальных пунктов меню
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                // Выводим пункт меню
                Console.WriteLine(menuOptions[i]);

                Console.ResetColor();
            }
        }

        private void ActivateMenuItem()
        {
            // Проверка "Существует ли выбранный пункт меню?"
            if (menuActions[selectedIndex] != null)
            {
                // Если существует - вызывается
                menuActions[selectedIndex].Invoke();
            }
        }
      }
    }
