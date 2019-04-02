using System;
using System.Collections.Generic;

namespace vs_project
{
    class Program
    {
        static void Main(string[] args)
        {
            // Список работников
            List<Worker> workers = new List<Worker>();

            // Фильтр
            Filter filter = new Filter();
            filter.init();

            // Бесконечный цикл работы программы
            do
            {
                // Вывод главного меню
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Добавить нового сотрудника");
                Console.WriteLine("2 - Вывести всех сотрудников");
                Console.WriteLine("3 - Вывести отфильтрованных сотрудников");
                Console.WriteLine("4 - Установить параметры фильтра");

                // Ожидание и обработка ввода пользователя
                switch (Console.ReadLine())
                {
                    // Добавления в список нового работника
                    case "1":
                        Worker.Add(ref workers);
                        break;

                    // Вывод всех работников
                    case "2":
                        Worker.OutAll(ref workers);
                        Console.ReadKey();
                        break;

                    // Вывод работников удовлетворяющих фильтру
                    case "3":
                        Worker.OutFilterWorkers(workers, filter);
                        Console.ReadKey();
                        break;

                    // Изменение значениий фильтра
                    case "4":
                        filter.ChangeValue();
                        break;

                    // Выход из программы
                    default: 
                        Environment.Exit(0);
                        break;
                }
            } while (true);
        }

        #region Работник

        // Работник
        struct Worker
        {
            private string name, surname, patronymic;   // Имя, Фамилия, Отчество
            private string position;                    // Должность
            private string gender;                      // Пол
            private DateTime dateReceiptOnWork;         // Дата приема на работу

            // Вывод работника
            public void Out()
            {
                Console.WriteLine($"Имя: {name}");
                Console.WriteLine($"Фамилия: {surname}");
                Console.WriteLine($"Отчество: {patronymic}");
                Console.WriteLine($"Должность: {position}");
                Console.WriteLine($"Пол: {gender}");
                Console.WriteLine($"Дата приема на работу: {dateReceiptOnWork}");
                Console.WriteLine("_______________________________________________");
            }

            /// <summary>
            /// Вывод всех работников
            /// </summary>
            /// <param name="list"> Список работников</param>
            public static void OutAll(ref List<Worker> list)
            {
                Console.Clear();
                foreach (var worker in list)  // По работникам в списке
                {
                    worker.Out();
                }
            }
            
            /// <summary>
            /// Добавление работника
            /// </summary>
            /// <param name="list"> Список работников</param>
            public static void Add(ref List<Worker> list)
            {
                // Новый работник
                Worker worker = new Worker();
                Console.Clear();

                // Ввод данных работника
                try
                {
                    // Имя
                    Console.Write("Введите имя нового работника: ");
                    worker.name = Console.ReadLine();
                    while (String.IsNullOrEmpty(worker.name.Trim()))
                    {
                        Console.Write("Повторите ввод имени:");
                        worker.name = Console.ReadLine();
                    }

                    // Фамилия
                    Console.Write("Введите фамилию нового работника: ");
                    worker.surname = Console.ReadLine();
                    while (String.IsNullOrEmpty(worker.surname.Trim()))
                    {
                        Console.Write("Повторите ввод фамилии:");
                        worker.surname = Console.ReadLine();
                    }

                    // Отчество
                    Console.Write("Введите отчество нового работника: ");
                    worker.patronymic = Console.ReadLine();
                    while (String.IsNullOrEmpty(worker.patronymic.Trim()))
                    {
                        Console.Write("Повторите ввод отчества:");
                        worker.patronymic = Console.ReadLine();
                    }

                    // Должность
                    Console.Write("Выберите должность: ");
                    worker.position = Console.ReadLine();
                    while (String.IsNullOrEmpty(worker.position.Trim()))
                    {
                        Console.Write("Повторите ввод должности:");
                        worker.position = Console.ReadLine();
                    }

                    // Пол
                    Console.Write("Выберите пол: ");
                    worker.gender = Console.ReadLine();
                    while (String.IsNullOrEmpty(worker.gender.Trim()))
                    {
                        Console.Write("Повторите ввод пола:");
                        worker.gender = Console.ReadLine();
                    }

                    // Дата приёма на работу
                    DateTime emptyTime = new DateTime();
                    while (worker.dateReceiptOnWork == emptyTime)
                    {
                        try
                        {
                            Console.Write("Введите дату приёма на работу в формате ДД.ММ.ГГГГ: ");
                            worker.dateReceiptOnWork = Convert.ToDateTime(Console.ReadLine());
                        }
                        catch
                        {
                            Console.Write("Ошибка: неверный формат\n");
                        }
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Ошибка: некорректный ввод");
                }

                // Добавление сотрудника в список
                list.Add(worker);
            }

            /// <summary>
            /// Вывод отфильтрованных работников
            /// </summary>
            /// <param name="workers"> Список работников</param>
            /// <param name="filter"> Фильтр</param>
            public static void OutFilterWorkers(List<Worker> workers, Filter filter)
            {
                Console.Clear();
                foreach (Worker w in workers) // По работникам в списке
                {
                    // Проверка имени
                    if (filter.name != "" && !w.name.Contains(filter.name)) continue;

                    // Проверка фамилии
                    if (filter.surname != "" && !w.surname.Contains(filter.surname)) continue;

                    // Проверка отчества
                    if (filter.patronymic != "" && !w.patronymic.Contains(filter.patronymic)) continue;

                    // Проверка должности
                    if (filter.position != "" && !w.position.Contains(filter.position)) continue;

                    // Проверка пола
                    if (filter.gender != "" && !w.gender.Contains(filter.gender)) continue;

                    // Проверка максимального стажа работы
                    if (w.dateReceiptOnWork < DateTime.Now.AddYears(-filter.maxAges) && filter.maxAges != 0) continue;

                    // Проверка минимального стажа работы
                    if (w.dateReceiptOnWork > DateTime.Now.AddYears(-filter.minAges) && filter.minAges != 0) continue;

                    w.Out(); // Вывод отфильтрованного работника на экран
                }
            }
        }

        #endregion

        #region Фильтр

        struct Filter
        {
            public string name, surname, patronymic;    // Имя, Фамилия, Отчество
            public string position;                     // Должность
            public string gender;                       // Пол            
            public int minAges, maxAges;                // Минимальное и максимальное значение диапозона стажа


            public void init()
            {
                this.name = "";
                this.surname = "";
                this.patronymic = "";
                this.position = "";
                this.gender = "";
                this.minAges = 0;
                this.maxAges = 0;
            }

            // Изменение значения фильтра
            public void ChangeValue()
            {
                // Вывод подменю фильтра
                Console.Clear();
                Console.WriteLine("Выберите какое поле фильтра изменить:");
                Console.WriteLine($"1 - Имя ({name})");
                Console.WriteLine($"2 - Фамилия ({surname})");
                Console.WriteLine($"3 - Отчество ({patronymic})");
                Console.WriteLine($"4 - Должность ({position})");
                Console.WriteLine($"5 - Пол ({gender})");
                Console.WriteLine($"6 - Минимальный стаж работы ({minAges})");
                Console.WriteLine($"7 - Максимальный стаж работы ({maxAges})");
                Console.WriteLine($"Любое другое значение для выхода в главное меню");

                // Ожидание ввода пользователя и обработка
                switch (Console.ReadLine())
                {
                    // Изменение значение фильтрации по имени
                    case ("1"):
                        Console.Write("\nВведите новое значение:");
                        name = Console.ReadLine();
                        break;

                    // Изменение значение фильтрации по фамилии
                    case ("2"):
                        Console.Write("\nВведите новое значение:");
                        surname = Console.ReadLine().Trim();
                        break;

                    // Изменение значение фильтрации по отчеству
                    case ("3"):
                        Console.Write("\nВведите новое значение:");
                        patronymic = Console.ReadLine().Trim();
                        break;

                    // Изменение значение фильтрации по должности
                    case ("4"):
                        Console.Write("\nВведите новое значение:");
                        position = Console.ReadLine().Trim();
                        break;

                    // Изменение значение фильтрации по полу
                    case ("5"):
                        Console.Write("\nВведите новое значение:");
                        gender = Console.ReadLine().Trim();
                        break;

                    // Изменение минимального значения фильтрации по стажу
                    case ("6"):
                        while (true)
                        {
                            try
                            {
                                Console.Write("\nВведите новое значение: ");
                                minAges = Int32.Parse(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("\nОшибка: неверный формат");
                            }
                        }
                        break;

                    // Изменение максимального значения фильтрации по стажу
                    case ("7"):
                        while (true)
                        {
                            try
                            {
                                Console.Write("\nВведите новое значение: ");
                                maxAges = Int32.Parse(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("\nОшибка: неверный формат");
                            }
                        }
                        break;
                }
            }
        }

        #endregion
    }
}
