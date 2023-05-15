using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Slot_Machine
{
    class SlotMachine
    {
        // Количество кредитов
        public int Credits { get; set; }

        // Прогрессивный джекпот
        public int Jackpot { get; private set; }

        // Конструктор класса
        public SlotMachine()
        {
            Credits = 100;
            Jackpot = 0;
        }

        // Метод запуска игрового раунда
        public void Spin(int bet)
        {
            // Вычитаем ставку из кредитов
            Credits -= bet;

            // Генерируем случайные значения для барабанов
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---Генерация случайных чисел---");
            int reel1 = new Random().Next(1, 4);
            Thread.Sleep(750);
            int reel2 = new Random().Next(1, 4);
            Thread.Sleep(750);
            int reel3 = new Random().Next(1, 4);
            Thread.Sleep(750);

            // Выводим значения барабанов на экран
            Console.WriteLine($"\t\t\t\t|{reel1}| |{reel2}| |{reel3}|");
            Console.ResetColor();

            // Проверяем, выиграл ли игрок
            if (reel1 == 1 && reel2 == 1 && reel3 == 1)
            {
                // Джекпот!
                Console.ForegroundColor = ConsoleColor.Yellow;
                int jackpotWin = Jackpot;
                Jackpot = 0;
                Credits += jackpotWin;
                Console.WriteLine($"\t\t\t\tВы выиграли джекпот в {jackpotWin} кредитов!");
                Console.ResetColor();
            }
            else if (reel1 == 2 && reel2 == 2 && reel3 == 2)
            {
                // Выигрыш в 5 раз ставки
                Console.ForegroundColor = ConsoleColor.Blue;
                int winAmount = bet * 5;
                Credits += winAmount;
                Console.WriteLine($"\t\t\t\tВы выиграли {winAmount} кредитов!");
                Console.ResetColor();
            }
            else if (reel1 == 3 && reel2 == 3 && reel3 == 3)
            {
                // Выигрыш в 2 раза ставки
                Console.ForegroundColor = ConsoleColor.Cyan;
                int winAmount = bet * 2;
                Credits += winAmount;
                Console.WriteLine($"\t\t\t\tВы выиграли {winAmount} кредитов!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\t\t\tВы проиграли.");
                Console.ResetColor();
            }

            // Увеличиваем прогрессивный джекпот
            Jackpot += bet;
        }
    }
}
