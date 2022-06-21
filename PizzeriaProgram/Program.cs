using System;
using System.IO;
using System.Collections.Generic;
using PizzeriaLibrary;
using System.Threading;

namespace PizzeriaProgram
{
    class MyEventArgs : EventArgs
    {
        public char ch;
    }

    class KeyEvent
    {
        // Создадим событие, используя обобщенный делегат.
        public event EventHandler<MyEventArgs> KeyDown;

        public void OnKeyDown(char ch)
        {
            MyEventArgs c = new MyEventArgs();

            if (KeyDown != null)
            {
                c.ch = ch;
                KeyDown(this, c);
            }
        }
    }
    internal class Program
    {
        const int MAX = 200;
        private static void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e) //Метод для закрытия программы.
        {
            Console.WriteLine("Программа отменена, все данные сохранены.");
        }

        static void Main(string[] args)
        {
            int q = 0;
            string path = @"..\Pizzeria.csv";  //Заносим Баланс и ингридиенты на складе из файла.
            var pizzeria = new Pizzeria();
            while (q == 0)
            {
                q++;
                var lines = File.ReadAllLines(path);
                var splits = lines[1].Split(';');
                pizzeria.Balance = Convert.ToInt32(splits[0]);
                pizzeria.Salami = Convert.ToInt32(splits[1]);
                pizzeria.Bacon = Convert.ToInt32(splits[2]);
                pizzeria.Jalapeno = Convert.ToInt32(splits[3]);
                pizzeria.Cheese = Convert.ToInt32(splits[4]);
                pizzeria.Pineapple = Convert.ToInt32(splits[5]);
                pizzeria.Champignons = Convert.ToInt32(splits[6]);
                pizzeria.Tomato = Convert.ToInt32(splits[7]);
                pizzeria.Onion = Convert.ToInt32(splits[8]);
                pizzeria.Cucumbers = Convert.ToInt32(splits[9]);
                pizzeria.Ham = Convert.ToInt32(splits[10]);
            }

            path = @"..\Buyers.csv";  //Заносим список покупателей.
            var buyers = new Buyers();
            buyers.LoadBuyers(path);

            path = @"..\Drinks.csv";  //Заносим список напитков.
            var drinks = new Drinks<string>();
            drinks.LoadDrinks(path);

            path = @"..\Kebabs.csv";  //Заносим список шаурмы.
            var kebabs = new Kebabs<string>();
            kebabs.LoadKebabs(path);

            Console.CancelKeyPress += Console_CancelKeyPress;            //Методы для сохранении при закрытии программы.
            Console.CancelKeyPress += pizzeria.Console_CancelKeyPress;
            Console.CancelKeyPress += drinks.Console_CancelKeyPress;
            Console.CancelKeyPress += kebabs.Console_CancelKeyPress;
            Console.CancelKeyPress += buyers.Console_CancelKeyPress;

            Console.WriteLine("Здравствуйте, авторизируйтесь, пожалуйста.");
            int avtorizacia = 1;
            var customer = new Buyers();
            while (avtorizacia == 1)
            {
                Console.Write("Ваш логин:");
                string login = Console.ReadLine();
                Console.WriteLine("\nВаш пароль:");
                string pass = Console.ReadLine();
                customer = buyers.TryLogin(login, pass);
                if (customer == null)
                {
                    Console.WriteLine("Такого пользователя нет.");
                    Thread.Sleep(500);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Добро пожаловать!");
                    Console.WriteLine(customer.Name);
                    Console.WriteLine(customer.Balance);
                    avtorizacia = 0;
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }

            var selldrinks = new Drinks<string>[drinks.Count()];        //Переменные для меню.
            var sellkebabs = new Kebabs<string>[kebabs.Count()];
            var pizza = new Pizza[MAX];
            int numdrinks = 0;
            int numkebabs = 0;
            int numpizza = 0;
            int fullprice = 0;
            int price = 0;

            Console.WriteLine("1) Пицца\n" + "2) Шаурма\n" + "3) Напитки\n" + "4) Корзина\n" + "e) Выход");
            KeyEvent @event = new KeyEvent();
            @event.KeyDown += (sender, e) =>       //Используем EventArgs для нажатия кнопок в меню.
            {
                switch (e.ch)
                {
                    case '1':                   //Вкладка пиццы.
                        {
                            Console.Clear();
                            Console.WriteLine("Выберите размер пиццы:" + "\n1) 20cm" + "\n2) 30cm" + "\n3) 40cm" + "\ne) Назад");
                            KeyEvent @event = new KeyEvent();
                            @event.KeyDown += (sender, e) =>       //Используем EventArgs для нажатия кнопок в меню.
                            {
                                switch (e.ch)
                                {
                                    case '1':                   //Вкладка 20 cm
                                        {
                                            var pizz = new Pizza(false);
                                            Console.Clear();
                                            pizz.Base = true;
                                            pizz.Size = "20cm";
                                            int i = 1;
                                            Console.WriteLine("Выберите ингридиенты:" + "\n1) Салями" + "\n2) Бекон" + "\n3) Холопеньо" + "\n4) Сыр" + "\n5) Ананас" + "\n6) Шампиньоны" + "\n7) Помидоры" + "\n8) Лук" + "\n9) Огурцы" + "\n0) Ветчина" + "\na) Принять");
                                            KeyEvent @event = new KeyEvent();
                                            @event.KeyDown += (sender, e) =>       //Используем EventArgs для нажатия кнопок в меню.
                                            {
                                                switch (e.ch)
                                                {
                                                    case '1':                   //Вкладка
                                                        {
                                                            if (pizzeria.Salam1(i, price))
                                                            {
                                                                pizz.Salami += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '2':                   //Вкладка
                                                        {
                                                            if (pizzeria.Bac0n(i, price))
                                                            {
                                                                pizz.Bacon += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '3':                   //Вкладка
                                                        {
                                                            if (pizzeria.Ja1openo(i, price))
                                                            {
                                                                pizz.Jalapeno += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '4':                   //Вкладка
                                                        {
                                                            if (pizzeria.Ch3ese(i, price))
                                                            {
                                                                pizz.Cheese += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '5':                   //Вкладка
                                                        {
                                                            if (pizzeria.P1neapple(i, price))
                                                            {
                                                                pizz.Pineapple += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '6':                   //Вкладка
                                                        {
                                                            if (pizzeria.Champign0ns(i, price))
                                                            {
                                                                pizz.Champignons += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '7':                   //Вкладка
                                                        {
                                                            if (pizzeria.Tomat0(i, price))
                                                            {
                                                                pizz.Tomato += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '8':                   //Вкладка
                                                        {
                                                            if (pizzeria.Oni0n(i, price))
                                                            {
                                                                pizz.Onion += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '9':                   //Вкладка
                                                        {
                                                            if (pizzeria.Cucumber(i, price))
                                                            {
                                                                pizz.Cucumbers += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '0':                   //Вкладка
                                                        {
                                                            if (pizzeria.H4m(i, price))
                                                            {
                                                                pizz.Ham += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case 'a':                   //Вкладка
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Добавить в заказ?" + "\ny) yes" + "\nn) no");
                                                            string h = Console.ReadLine();
                                                            if (h == "y")
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Выберите размер пиццы:" + "\n1) 20cm" + "\n2) 30cm" + "\n3) 40cm" + "\ne) Назад");
                                                                pizz.Price = price;
                                                                pizza[numpizza] = pizz;
                                                                fullprice += price;
                                                                price = 0;
                                                                numpizza++;
                                                            }
                                                            else if (h == "n")
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Выберите размер пиццы:" + "\n1) 20cm" + "\n2) 30cm" + "\n3) 40cm" + "\ne) Назад");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Такой команды нет!");
                                                            }
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("\nТакая команда не найдена!");
                                                            break;
                                                        }
                                                }

                                            };
                                            char ch;
                                            do
                                            {
                                                Console.Write("\nВыберите тип Добавки (3): ");
                                                ConsoleKeyInfo key;
                                                key = Console.ReadKey();
                                                ch = key.KeyChar;
                                                @event.OnKeyDown(key.KeyChar);
                                            }
                                            while (ch != 'a');
                                            break;
                                        }
                                    case '2':                   //Вкладка 30 cm
                                        {
                                            var pizz = new Pizza(false);
                                            Console.Clear();
                                            int i = 2;
                                            pizz.Base = true;
                                            pizz.Size = "30cm";
                                            Console.WriteLine("Выберите ингридиенты:" + "\n1) Салями" + "\n2) Бекон" + "\n3) Холопеньо" + "\n4) Сыр" + "\n5) Ананас" + "\n6) Шампиньоны" + "\n7) Помидоры" + "\n8) Лук" + "\n9) Огурцы" + "\n0) Ветчина" + "\na) Принять");
                                            KeyEvent @event = new KeyEvent();
                                            @event.KeyDown += (sender, e) =>       //Используем EventArgs для нажатия кнопок в меню.
                                            {
                                                switch (e.ch)
                                                {
                                                    case '1':                   //Вкладка
                                                        {
                                                            if (pizzeria.Salam1(i, price))
                                                            {
                                                                pizz.Salami += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '2':                   //Вкладка
                                                        {
                                                            if (pizzeria.Bac0n(i, price))
                                                            {
                                                                pizz.Bacon += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '3':                   //Вкладка
                                                        {
                                                            if (pizzeria.Ja1openo(i, price))
                                                            {
                                                                pizz.Jalapeno += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '4':                   //Вкладка
                                                        {
                                                            if (pizzeria.Ch3ese(i, price))
                                                            {
                                                                pizz.Cheese += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '5':                   //Вкладка
                                                        {
                                                            if (pizzeria.P1neapple(i, price))
                                                            {
                                                                pizz.Pineapple += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '6':                   //Вкладка
                                                        {
                                                            if (pizzeria.Champign0ns(i, price))
                                                            {
                                                                pizz.Champignons += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '7':                   //Вкладка
                                                        {
                                                            if (pizzeria.Tomat0(i, price))
                                                            {
                                                                pizz.Tomato += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '8':                   //Вкладка
                                                        {
                                                            if (pizzeria.Oni0n(i, price))
                                                            {
                                                                pizz.Onion += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '9':                   //Вкладка
                                                        {
                                                            if (pizzeria.Cucumber(i, price))
                                                            {
                                                                pizz.Cucumbers += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '0':                   //Вкладка
                                                        {
                                                            if (pizzeria.H4m(i, price))
                                                            {
                                                                pizz.Ham += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case 'a':                   //Вкладка
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Добавить в заказ?" + "\ny) yes" + "\nn) no");
                                                            string h = Console.ReadLine();
                                                            if (h == "y")
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Выберите размер пиццы:" + "\n1) 20cm" + "\n2) 30cm" + "\n3) 40cm" + "\ne) Назад");
                                                                pizz.Price = price;
                                                                pizza[numpizza] = pizz;
                                                                fullprice += price;
                                                                price = 0;
                                                                numpizza++;
                                                            }
                                                            else if (h == "n")
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Выберите размер пиццы:" + "\n1) 20cm" + "\n2) 30cm" + "\n3) 40cm" + "\ne) Назад");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Такой команды нет!");
                                                            }
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("\nТакая команда не найдена!");
                                                            break;
                                                        }
                                                }

                                            };
                                            char ch;
                                            do
                                            {
                                                Console.Write("\nВыберите тип Добавки (3): ");
                                                ConsoleKeyInfo key;
                                                key = Console.ReadKey();
                                                ch = key.KeyChar;
                                                @event.OnKeyDown(key.KeyChar);
                                            }
                                            while (ch != 'a');
                                            break; ;
                                        }
                                    case '3':                   //Вкладка 40 cm
                                        {
                                            var pizz = new Pizza(false);
                                            Console.Clear();
                                            int i = 3;
                                            pizz.Base = true;
                                            pizz.Size = "40cm";
                                            Console.WriteLine("Выберите ингридиенты:" + "\n1) Салями" + "\n2) Бекон" + "\n3) Холопеньо" + "\n4) Сыр" + "\n5) Ананас" + "\n6) Шампиньоны" + "\n7) Помидоры" + "\n8) Лук" + "\n9) Огурцы" + "\n0) Ветчина" + "\na) Принять");
                                            KeyEvent @event = new KeyEvent();
                                            @event.KeyDown += (sender, e) =>       //Используем EventArgs для нажатия кнопок в меню.
                                            {
                                                switch (e.ch)
                                                {
                                                    case '1':                   //Вкладка
                                                        {
                                                            if (pizzeria.Salam1(i, price))
                                                            {
                                                                pizz.Salami += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '2':                   //Вкладка
                                                        {
                                                            if (pizzeria.Bac0n(i, price))
                                                            {
                                                                pizz.Bacon += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '3':                   //Вкладка
                                                        {
                                                            if (pizzeria.Ja1openo(i, price))
                                                            {
                                                                pizz.Jalapeno += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '4':                   //Вкладка
                                                        {
                                                            if (pizzeria.Ch3ese(i, price))
                                                            {
                                                                pizz.Cheese += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '5':                   //Вкладка
                                                        {
                                                            if (pizzeria.P1neapple(i, price))
                                                            {
                                                                pizz.Pineapple += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '6':                   //Вкладка
                                                        {
                                                            if (pizzeria.Champign0ns(i, price))
                                                            {
                                                                pizz.Champignons += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '7':                   //Вкладка
                                                        {
                                                            if (pizzeria.Tomat0(i, price))
                                                            {
                                                                pizz.Tomato += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '8':                   //Вкладка
                                                        {
                                                            if (pizzeria.Oni0n(i, price))
                                                            {
                                                                pizz.Onion += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '9':                   //Вкладка
                                                        {
                                                            if (pizzeria.Cucumber(i, price))
                                                            {
                                                                pizz.Cucumbers += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case '0':                   //Вкладка
                                                        {
                                                            if (pizzeria.H4m(i, price))
                                                            {
                                                                pizz.Ham += i;
                                                                price += 50 * i;
                                                            }
                                                            break;
                                                        }
                                                    case 'a':                   //Вкладка
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Добавить в заказ?" + "\ny) yes" + "\nn) no");
                                                            string h = Console.ReadLine();
                                                            if (h == "y")
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Выберите размер пиццы:" + "\n1) 20cm" + "\n2) 30cm" + "\n3) 40cm" + "\ne) Назад");
                                                                pizz.Price = price;
                                                                pizza[numpizza] = pizz;
                                                                fullprice += price;
                                                                price = 0;
                                                                numpizza++;
                                                            }
                                                            else if (h == "n")
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("Выберите размер пиццы:" + "\n1) 20cm" + "\n2) 30cm" + "\n3) 40cm" + "\ne) Назад");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Такой команды нет!");
                                                            }
                                                            
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("\nТакая команда не найдена!");
                                                            break;
                                                        }
                                                }

                                            };
                                            char ch;
                                            do
                                            {
                                                Console.Write("\nВыберите тип Добавки (3): ");
                                                ConsoleKeyInfo key;
                                                key = Console.ReadKey();
                                                ch = key.KeyChar;
                                                @event.OnKeyDown(key.KeyChar);
                                            }
                                            while (ch != 'a');
                                            break; ;
                                        }
                                    case 'e':
                                        {
                                            Console.Clear();
                                            Console.WriteLine("1) Пицца\n" + "2) Шаурма\n" + "3) Напитки\n" + "4) Корзина\n" + "e) Выход");
                                            break;
                                        }
                                }

                            };
                            char ch;
                            do
                            {
                                Console.Write("Выберите тип товара (2): ");
                                ConsoleKeyInfo key;
                                key = Console.ReadKey();
                                ch = key.KeyChar;
                                @event.OnKeyDown(key.KeyChar);
                            }
                            while (ch != 'e');
                            break;
                        }
                    case '2':         //Открываем вкладку с шаурмой.
                        {
                            Console.WriteLine("\n");
                            kebabs.Spisok();
                            KeyEvent @event = new KeyEvent();
                            @event.KeyDown += (sender, e) =>
                            {
                                switch (e.ch)
                                {
                                    case '1':       //Добавляем в корзину, все остальные аналогично.
                                        {
                                            if (kebabs.Nalichie(0) == true)
                                            {
                                                int povtor = 0;
                                                int index = 0;
                                                var keb = kebabs.Element(0);
                                                fullprice += keb.Price;
                                                for (int i = 0; i < sellkebabs.Length; i++)
                                                {
                                                    if (sellkebabs[i] != null)
                                                    {
                                                        if (sellkebabs[i].Name == keb.Name)
                                                        {
                                                            povtor = 1;
                                                            index = i;
                                                        }
                                                    }
                                                }
                                                if (povtor == 1)
                                                {
                                                    sellkebabs[index].Quantity = sellkebabs[index].Quantity + 1;
                                                }
                                                else
                                                {
                                                    var keb1 = kebabs.Element(0);
                                                    sellkebabs[numkebabs] = keb1;
                                                    sellkebabs[numkebabs].Quantity = 1;
                                                    numkebabs++;
                                                }
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                kebabs.Spisok();
                                            }
                                            break;
                                        }
                                    case '2':
                                        {
                                            if (kebabs.Nalichie(1) == true)
                                            {
                                                int povtor = 0;
                                                int index = 0;
                                                var keb = kebabs.Element(1);
                                                fullprice += keb.Price;
                                                for (int i = 0; i < sellkebabs.Length; i++)
                                                {
                                                    if (sellkebabs[i] != null)
                                                    {
                                                        if (sellkebabs[i].Name == keb.Name)
                                                        {
                                                            povtor = 1;
                                                            index = i;
                                                        }
                                                    }
                                                }
                                                if (povtor == 1)
                                                {
                                                    sellkebabs[index].Quantity = sellkebabs[index].Quantity + 1;
                                                }
                                                else
                                                {
                                                    var keb1 = kebabs.Element(1);
                                                    sellkebabs[numkebabs] = keb1;
                                                    sellkebabs[numkebabs].Quantity = 1;
                                                    numkebabs++;
                                                }
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                kebabs.Spisok();
                                            }
                                            break;
                                        }
                                    case '3':
                                        {
                                            if (kebabs.Nalichie(2) == true)
                                            {
                                                int povtor = 0;
                                                int index = 0;
                                                var keb = kebabs.Element(2);
                                                fullprice += keb.Price;
                                                for (int i = 0; i < sellkebabs.Length; i++)
                                                {
                                                    if (sellkebabs[i] != null)
                                                    {
                                                        if (sellkebabs[i].Name == keb.Name)
                                                        {
                                                            povtor = 1;
                                                            index = i;
                                                        }
                                                    }
                                                }
                                                if (povtor == 1)
                                                {
                                                    sellkebabs[index].Quantity = sellkebabs[index].Quantity + 1;
                                                }
                                                else
                                                {
                                                    var keb1 = kebabs.Element(2);
                                                    sellkebabs[numkebabs] = keb1;
                                                    sellkebabs[numkebabs].Quantity = 1;
                                                    numkebabs++;
                                                }
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                kebabs.Spisok();
                                            }
                                            break;
                                        }
                                    case '4':
                                        {
                                            if (kebabs.Nalichie(3) == true)
                                            {
                                                int povtor = 0;
                                                int index = 0;
                                                var keb = kebabs.Element(3);
                                                fullprice += keb.Price;
                                                for (int i = 0; i < sellkebabs.Length; i++)
                                                {
                                                    if (sellkebabs[i] != null)
                                                    {
                                                        if (sellkebabs[i].Name == keb.Name)
                                                        {
                                                            povtor = 1;
                                                            index = i;
                                                        }
                                                    }
                                                }
                                                if (povtor == 1)
                                                {
                                                    sellkebabs[index].Quantity = sellkebabs[index].Quantity + 1;
                                                }
                                                else
                                                {
                                                    var keb1 = kebabs.Element(3);
                                                    sellkebabs[numkebabs] = keb1;
                                                    sellkebabs[numkebabs].Quantity = 1;
                                                    numkebabs++;
                                                }
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                kebabs.Spisok();
                                            }
                                            break;
                                        }
                                    case '5':
                                        {
                                            if (kebabs.Nalichie(4) == true)
                                            {
                                                int povtor = 0;
                                                int index = 0;
                                                var keb = kebabs.Element(4);
                                                fullprice += keb.Price;
                                                for (int i = 0; i < sellkebabs.Length; i++)
                                                {
                                                    if (sellkebabs[i] != null)
                                                    {
                                                        if (sellkebabs[i].Name == keb.Name)
                                                        {
                                                            povtor = 1;
                                                            index = i;
                                                        }
                                                    }
                                                }
                                                if (povtor == 1)
                                                {
                                                    sellkebabs[index].Quantity = sellkebabs[index].Quantity + 1;
                                                }
                                                else
                                                {
                                                    var keb1 = kebabs.Element(4);
                                                    sellkebabs[numkebabs] = keb1;
                                                    sellkebabs[numkebabs].Quantity = 1;
                                                    numkebabs++;
                                                }
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                kebabs.Spisok();
                                            }
                                            break;
                                        }
                                    case 'e':        //Кнопка назад.
                                        {
                                            Console.Clear();
                                            Console.WriteLine("1) Пицца\n" + "2) Шаурма\n" + "3) Напитки\n" + "4) Корзина\n" + "e) Выход");
                                            break;
                                        }
                                }
                            };
                            char ch;
                            do
                            {
                                Console.Write("Выберите нужную позицию из меню: ");
                                ConsoleKeyInfo key;
                                key = Console.ReadKey();
                                ch = key.KeyChar;
                                @event.OnKeyDown(key.KeyChar);
                            }
                            while (ch != 'e');
                            break;
                        }
                    case '3':                   //Вкладка c напитками.
                        {
                            Console.WriteLine("\n");
                            drinks.Spisok();
                            KeyEvent @event = new KeyEvent();
                            @event.KeyDown += (sender, e) =>
                            {
                                switch (e.ch)
                                {
                                    case '1':       //Добавляем в корзину, все остальные аналогично.
                                        {
                                            if (drinks.Nalichie(0) == true)
                                            {
                                                int povtor = 0;
                                                int index = 0;
                                                var drink = drinks.Element(0);
                                                fullprice += drink.Price;
                                                for (int i = 0; i < selldrinks.Length; i++)
                                                {
                                                    if (selldrinks[i] != null)
                                                    {
                                                        if (selldrinks[i].Name == drink.Name)
                                                        {
                                                            povtor = 1;
                                                            index = i;
                                                        }
                                                    }
                                                }
                                                if (povtor == 1)
                                                {
                                                    selldrinks[index].Quantity = selldrinks[index].Quantity + 1;
                                                }
                                                else
                                                {
                                                    var drink1 = drinks.Element(0);
                                                    selldrinks[numdrinks] = drink1;
                                                    selldrinks[numdrinks].Quantity = 1;
                                                    numdrinks++;
                                                }
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                drinks.Spisok();
                                            }
                                            break;
                                        }
                                    case '2':
                                        {
                                            if (drinks.Nalichie(1) == true)
                                            {
                                                int povtor = 0;
                                                int index = 0;
                                                var drink = drinks.Element(1);
                                                fullprice += drink.Price;
                                                for (int i = 0; i < selldrinks.Length; i++)
                                                {
                                                    if (selldrinks[i] != null)
                                                    {
                                                        if (selldrinks[i].Name == drink.Name)
                                                        {
                                                            povtor = 1;
                                                            index = i;
                                                        }
                                                    }
                                                }
                                                if (povtor == 1)
                                                {
                                                    selldrinks[index].Quantity = selldrinks[index].Quantity + 1;
                                                }
                                                else
                                                {
                                                    var drink1 = drinks.Element(1);
                                                    selldrinks[numdrinks] = drink1;
                                                    selldrinks[numdrinks].Quantity = 1;
                                                    numdrinks++;
                                                }
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                drinks.Spisok();
                                            }
                                            break;
                                        }
                                    case '3':
                                        {
                                            if (drinks.Nalichie(2) == true)
                                            {
                                                int povtor = 0;
                                                int index = 0;
                                                var drink = drinks.Element(2);
                                                fullprice += drink.Price;
                                                for (int i = 0; i < selldrinks.Length; i++)
                                                {
                                                    if (selldrinks[i] != null)
                                                    {
                                                        if (selldrinks[i].Name == drink.Name)
                                                        {
                                                            povtor = 1;
                                                            index = i;
                                                        }
                                                    }
                                                }
                                                if (povtor == 1)
                                                {
                                                    selldrinks[index].Quantity = selldrinks[index].Quantity + 1;
                                                }
                                                else
                                                {
                                                    var drink1 = drinks.Element(2);
                                                    selldrinks[numdrinks] = drink1;
                                                    selldrinks[numdrinks].Quantity = 1;
                                                    numdrinks++;
                                                }
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                drinks.Spisok();
                                            }
                                            break;
                                        }
                                    case '4':
                                        {
                                            if (drinks.Nalichie(3) == true)
                                            {
                                                int povtor = 0;
                                                int index = 0;
                                                var drink = drinks.Element(3);
                                                fullprice += drink.Price;
                                                for (int i = 0; i < selldrinks.Length; i++)
                                                {
                                                    if (selldrinks[i] != null)
                                                    {
                                                        if (selldrinks[i].Name == drink.Name)
                                                        {
                                                            povtor = 1;
                                                            index = i;
                                                        }
                                                    }
                                                }
                                                if (povtor == 1)
                                                {
                                                    selldrinks[index].Quantity = selldrinks[index].Quantity + 1;
                                                }
                                                else
                                                {
                                                    var drink1 = drinks.Element(3);
                                                    selldrinks[numdrinks] = drink1;
                                                    selldrinks[numdrinks].Quantity = 1;
                                                    numdrinks++;
                                                }
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                drinks.Spisok();
                                            }
                                            break;
                                        }
                                    case '5':
                                        {
                                            if (drinks.Nalichie(4) == true)
                                            {
                                                int povtor = 0;
                                                int index = 0;
                                                var drink = drinks.Element(4);
                                                fullprice += drink.Price;
                                                for (int i = 0; i < selldrinks.Length; i++)
                                                {
                                                    if (selldrinks[i] != null)
                                                    {
                                                        if (selldrinks[i].Name == drink.Name)
                                                        {
                                                            povtor = 1;
                                                            index = i;
                                                        }
                                                    }
                                                }
                                                if (povtor == 1)
                                                {
                                                    selldrinks[index].Quantity = selldrinks[index].Quantity + 1;
                                                }
                                                else
                                                {
                                                    var drink1 = drinks.Element(4);
                                                    selldrinks[numdrinks] = drink1;
                                                    selldrinks[numdrinks].Quantity = 1;
                                                    numdrinks++;
                                                }
                                                Thread.Sleep(1000);
                                                Console.Clear();
                                                drinks.Spisok();
                                            }
                                            break;
                                        }
                                    case 'e':        //Кнопка назад.
                                        {
                                            Console.Clear();
                                            Console.WriteLine("1) Пицца\n" + "2) Шаурма\n" + "3) Напитки\n" + "4) Корзина\n" + "e) Выход");
                                            break;
                                        }
                                }
                            };
                            char ch;
                            do
                            {
                                Console.Write("Выберите нужную позицию из меню: ");
                                ConsoleKeyInfo key;
                                key = Console.ReadKey();
                                ch = key.KeyChar;
                                @event.OnKeyDown(key.KeyChar);
                            }
                            while (ch != 'e');
                            break;
                        }
                    case '4':                   //Вкладка корзины.
                        {
                            Console.Clear ();
                            for (int i = 0; i < MAX; i++)
                            {
                                if (pizza[i] != null)
                                {
                                    if (pizza[i].Base != false)
                                    {
                                        Console.WriteLine("pizza " + pizza[i].Size + " " + pizza[i].Price);
                                    }
                                }
                            }
                            for (int i = 0; i < sellkebabs.Length; i++)
                            {
                                if (sellkebabs[i] != null)
                                {
                                    Console.WriteLine(sellkebabs[i].ToString());
                                }
                            }
                            for (int i = 0; i < selldrinks.Length; i++)
                            {
                                if (selldrinks[i] != null)
                                {
                                    Console.WriteLine(selldrinks[i].ToString());
                                }
                            }
                            Console.WriteLine("Полная цена: " + fullprice);
                            Console.WriteLine("Купить?\n" + "y) yes\n" + "n) no\n" + "e) exit");
                            KeyEvent @event = new KeyEvent();
                            @event.KeyDown += (sender, e) =>       //Используем EventArgs для нажатия кнопок в меню.
                            {
                                switch (e.ch)
                                {
                                    case 'y':                   //Вкладка
                                        {
                                            if (customer.Balance < fullprice)
                                            {
                                                Console.WriteLine("\nУ вас недостаточно средств");
                                                for (int i = 0; i < MAX; i++)
                                                {
                                                    pizza[i] = null;
                                                }
                                                fullprice = 0;
                                                numpizza = 0;
                                            }
                                            else
                                            {
                                                customer.Balance -= fullprice;
                                                pizzeria.Balance += fullprice;
                                                Console.WriteLine("\nСпасибо за заказ!");
                                                for (int i = 0; i < MAX; i++)
                                                {
                                                    pizza[i] = null;
                                                }
                                                fullprice = 0;
                                                numpizza = 0;
                                            }
                                            break;
                                        }
                                    case 'n':                   //Вкладка
                                        {
                                            Console.WriteLine("\nЗаказ отменён.");
                                            for (int i = 0; i < MAX; i++)
                                            {
                                                pizza[i] = null;
                                            }
                                            fullprice = 0;
                                            numpizza = 0;
                                            break;
                                        }
                                    case 'e':                   //Вкладка
                                        {
                                            Console.Clear();
                                            Console.WriteLine("1) Пицца\n" + "2) Шаурма\n" + "3) Напитки\n" + "4) Корзина\n" + "e) Выход");
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("\nТакая команда не найдена!");
                                            break;
                                        }
                                }
                            };
                            char ch;
                            do
                            {
                                Console.Write("Выберите тип товара (2): ");
                                ConsoleKeyInfo key;
                                key = Console.ReadKey();
                                ch = key.KeyChar;
                                @event.OnKeyDown(key.KeyChar);
                            }
                            while (ch != 'e');
                            break;
                        }
                    case 'e':                   //Вкладка закрытия программы с сохранением.
                        {
                            kebabs.SaveKebabs();
                            drinks.SaveDrinks();
                            buyers.SaveBuyers();
                            pizzeria.SavePizzeria();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\nТакая команда не найдена!");
                            break;
                        }
                }

            };
            char ch;
            do
            {
                Console.Write("Выберите страницу в меню (1): ");
                ConsoleKeyInfo key;
                key = Console.ReadKey();
                ch = key.KeyChar;
                @event.OnKeyDown(key.KeyChar);
            }
            while (ch != 'e');
        }
    }
}
