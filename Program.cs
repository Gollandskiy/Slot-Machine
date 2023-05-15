using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading;

namespace Slot_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Название консольного окна
            Console.Title = "Slot-Machine (NE OBMAN TOCHNO)";
            // Создание игры
            SlotMachine machine = new SlotMachine();
            // Создание параметров меню
            string[] opt = { "\t\t\t\t\tНачать", "\t\t\t\t\tНастройки", "\t\t\t\t\tОб авторе", "\t\t\t\t\tВыход" };
            // Создание меню
            Menu menu = new Menu(opt);
            // Делегат с лямбда-выражениями
            menu.SetAction(0, () => Start(machine));
            menu.SetAction(1, () => Parameters(machine));
            menu.SetAction(2, () => Author());
            menu.SetAction(3, () => Exit());
            // Показ названия игры
            menu.DisplayName();
            // Старт меню
            menu.Start();
        }
        // Дальше идут статические функ.
        static void Start(SlotMachine machine) // Начало игры
        {
            // Обьявление множителей
            Console.WriteLine("  \t\t\t---Добро пожаловать в игру \"Однорукий бандит\"!---\n\t\t\t\t" +
                "|1|1|1| - ПРОГРЕССИВНЫЙ ДЖЕКПОТ!\n" +
                "\t\t\t\t|2|2|2| - ПОВЫШЕНИЕ СТАВКИ НА 5!\n" +
                "\t\t\t\t|3|3|3| - УДВОЕНИЕ СТАВКИ!");

            // Продолжения цикла до тех пор, пока у пользователя 10 или более кредитов
            while (machine.Credits >= 10)
            {
                // Показ счета пользователя
                Console.WriteLine($"\n\t\t\t\tВаш счет: {machine.Credits} кредитов");
                Console.Write("\t\t\t\tСделайте вашу ставку (от 10 кредитов): ");
                // Переменная которая отвечает за ставки
                int bet = 0;
                // Переменная которая отвечает за то, чтобы пользователь не вводил мусор вместо ставки
                string input = Console.ReadLine();
                if (int.TryParse(input, out bet))
                {
                    // Если меньше 10, то не принимаем
                    if (bet < 10)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\t\t\t\t---Слишком маленькая ставка!---");
                        Console.ResetColor();
                        continue;
                    }
                    //  Если ставка больше количества кредитов у пользователя - не принимаем
                    else if (bet > machine.Credits)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\t\t\t\t---Ваша ставка превышает ваше количество кредитов!---\n");
                        Console.ResetColor();
                        continue;
                    }
                    // Крутим-вертим, слот-машину обогащаем
                    machine.Spin(bet);

                    // Проверка хочет ли пользователь продолжить
                    Console.Write("Хотите продолжить игру? (да/нет): ");
                    string answer = Console.ReadLine();

                    if (answer.ToLower() != "да")
                    {
                        Console.WriteLine("\t\t\t\t---Спасибо за то, что потратили свои деньги ИМЕННО у нас!---\n");
                        Thread.Sleep(1000);
                        break;
                    }
                    // Если пользователь хочет продолжить, но у него слишком маленькое количество кредитов, то мы прекращаем игру
                    else if (machine.Credits < 10)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\t\t\t\t---Слишком маленькое количество кредитов!---\n");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        break;
                    }
                }
                // Если пользователь ввел какой-то мусор вместо "да", к примеру "отрфыпвмрыфов"
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\t\t\t\t---Вы ввели что-то неправильно!---");
                    Console.ResetColor();
                    continue;
                }
            }
            // Завершение игры
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\t\t\t\tИгра завершена. Спасибо за игру!");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
        // Настройки позволяющие изменять количество кредитов
        static void Parameters(SlotMachine machine)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\tНастройки:");
            Console.WriteLine();

            Console.WriteLine($"\t\t\t\tТекущее количество кредитов: {machine.Credits}");
            Console.WriteLine("\t\t\t\tИспользуйте стрелки вверх/вниз для изменения количества кредитов.");
            Console.WriteLine("\t\t\t\tНажмите Enter, чтобы сохранить изменения.");
            Console.WriteLine("\t\t\t\tНажмите Escape, чтобы выйти из настроек.");

            // Обьект типа ConsoleKeyInfo
            ConsoleKeyInfo keyInfo;
            do
            {
                // Читаем кнопочку
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    // Если пользователь нажал вверх кнопку, то прибавляем на одну и останавливаемся
                    case ConsoleKey.UpArrow:
                        machine.Credits++;
                        break;
                    // Здесь тоже самое, но кнопка вниз и минусуем один кредитик
                    case ConsoleKey.DownArrow:
                        machine.Credits = Math.Max(0, machine.Credits - 1);
                        break;
                }
                // Обновляем количество кредитов(чтобы в консоли показывалось динамическое изменение их)
                Console.Clear();
                Console.WriteLine("\t\t\t\tНастройки:");
                Console.WriteLine();
                Console.WriteLine($"\t\t\t\tТекущее количество кредитов: {machine.Credits}");
                Console.WriteLine("\t\t\t\tИспользуйте стрелки вверх/вниз для изменения количества кредитов.");
                Console.WriteLine("\t\t\t\tНажмите Enter, чтобы сохранить изменения.");
                Console.WriteLine("\t\t\t\tНажмите Escape, чтобы выйти из настроек.");

                // Продолжаем до тех пор, пока пользователь не нажмет Enter или Escape
            } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);

            // Выводим надпись о изменении
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine($"\t\t\t\tКоличество кредитов успешно изменено на: {machine.Credits}");
                Console.WriteLine("\t\t\t\tНажмите любую клавишу, чтобы вернуться в меню.");
                Console.ReadKey(true);
            }
        }
        // Открываем браузер с моим гитхабом на стандартном браузере которая использует ОС(Гугл Хром к примеру)
        static void Author()
        {
            System.Diagnostics.Process.Start("https://github.com/Gollandskiy");
        }
        // Выходим с игры
        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
