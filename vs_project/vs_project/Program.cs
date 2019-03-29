﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vs_project
{
    class Program
    {
        static void Main(string[] args)
        {
            // Объвление списка работников
            List<Worker> workers = new List<Worker>();
            
            // Объвление переменной для работы с меню
            ConsoleKeyInfo key;
            Filter filter = new Filter("", "", "", "", "", 0);
            
            // Цикл для взаимодействия пользователя с программой
            do
            {
                // Вывод информации в консоль для пользователя
                Console.Clear();
                Console.WriteLine("Choose action:");
                Console.WriteLine("1 - Add new worker");
                Console.WriteLine("2 - Write all workers");
                Console.WriteLine("3 - Write with filter");
                Console.WriteLine("4 - Set value for filter");
                Console.WriteLine("Escape - Exit");

                // Считывание нажатой клавиши в key
                key = Console.ReadKey();
                switch (key.Key)
                {
                    // Вызов метода для добавления в список нового работника
                    case (ConsoleKey.D1):
                        Worker.AddWorker(ref workers);
                        break;

                    // Вызов метода для вывода всех работников
                    case (ConsoleKey.D2):
                        Worker.WriteAllWorkers(ref workers);
                        Console.ReadKey();
                        break;

                    // Вызов метода для вывода работников удовлетворяющих фильтру
                    case (ConsoleKey.D3):
                        Worker.FilterWorkers(workers, filter);
                        Console.ReadKey();
                        break;

                    // Вызов метода для изменения значения фильтра
                    case (ConsoleKey.D4):
                        filter.ChangeFilterValue();
                        break;
                }

            // Сравнение key с клавишей Escape (выход из программы)
            } while (key.Key != ConsoleKey.Escape);
        }

        #region Объявление структуры Worker
        // Хранит информацию о работнике и методы для работы с ними.
        struct Worker
        {
            private string name, surname, patronymic; // Имя, Фамилия, Отчество
            private string position; // Должность
            private string gender; // Пол
            private DateTime dateReceiptOnWork; // Дата приема на работу

            /* 
             Создание нового сотрудника
             Поля: 
              name=Имя
              surname=Фамилия
              patronymic=Отчество
              position=Должность
              gender=Пол работника
              dateReceiptOnWork=Дата приема на работу
            */
            public Worker(string name, string surname, string patronymic, string position, string gender, DateTime dateReceiptOnWork)
            {
                this.name = name;
                this.surname = surname;
                this.patronymic = patronymic;
                this.position = position;
                this.gender = gender;
                this.dateReceiptOnWork = dateReceiptOnWork;
            }

            public DateTime GetDateReceiptOnWork()
            {
                return dateReceiptOnWork; // Возврат даты поступления работника
            }

            // Метод для вывода работника
            public void WriteWorker()
            {
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Surname: {surname}");
                Console.WriteLine($"Patronymic: {patronymic}");
                Console.WriteLine($"Position: {position}");
                Console.WriteLine($"Gender: {gender}");
                Console.WriteLine($"Date receipt on work: {dateReceiptOnWork}");
                Console.WriteLine("_______________________________________________");
            }

            // Метод для вывода всех работников
            public static void WriteAllWorkers(ref List<Worker> list)
            {
                Console.Clear();
                foreach (var worker in list)
                {
                    worker.WriteWorker();
                }
            }

            // Метод для добавления работника
            public static void AddWorker(ref List<Worker> list)
            {
                // Структура для добавления
                Worker worker = new Worker();
                Console.Clear();

                // Ввод имения, фамилии, отчества работника
                Console.Write("Введите имя нового работника: ");
                worker.name = Console.ReadLine();
                Console.Write("Введите фамилию нового работника: ");
                worker.surname = Console.ReadLine();
                Console.Write("Введите отчество нового работника: ");
                worker.patronymic = Console.ReadLine();
                
                // Ввод должности
                Console.Write("Выберите должность: ");
                worker.position = Console.ReadLine();

                // Ввод пола
                Console.Write("Выберите пол: ");
                worker.gender = Console.ReadLine();

                // Ввод даты приёма на работу
                DateTime emptyObject = new DateTime(); // Пустое значение времени для проверки выхода из цикла ввода
                while (worker.dateReceiptOnWork == emptyObject)  {
                    try
                    {
                        Console.Write("Введите дату приёма на работу в формате ДД.ММ.ГГГГ: ");
                        worker.dateReceiptOnWork = Convert.ToDateTime(Console.ReadLine());
                    } catch
                    {

                    }
                }

                // Добавление сотрудника в лист
                list.Add(worker);
            }


            // Метод вывода отфильтрованных данных
            public static void FilterWorkers(List<Worker> workers, Filter filter)
            {
                Console.Clear();
                foreach (Worker w in workers)
                {
                    // Проверка имени
                    if (w.name != filter.name && filter.name != "") { continue; }

                    // Проверка фамилии
                    if (w.surname != filter.surname && filter.surname != "") { continue; }

                    // Проверка отчества
                    if (w.patronymic != filter.patronymic && filter.patronymic != "") { continue; }

                    // Проверка должности
                    if (w.position != filter.position && filter.position != "") { continue; }

                    // Проверка пола
                    if (w.gender != filter.gender && filter.gender != "") { continue; }

                    // Проверка опыта работы
                    uint exp = (uint)(DateTime.Now.Year - w.GetDateReceiptOnWork().Year);
                    if (exp > filter.ages || filter.ages == 0)
                    {
                        w.WriteWorker(); // Вывод отфильтрованного работника на экран
                    }
                }
            }
        }

        #endregion

        #region структура фильтра по опыту работы сотрудника

        struct Filter
        {
            public string name, surname, patronymic; // Имя, Фамилия, Отчество
            public string position; // Должность
            public string gender; // Пол            
            public uint ages; // Количество лет стажа для фильтрации

            // Инициализация фильтра
            public Filter(string name, string surname, string patronymic, string position, string gender, uint ages)
            {
                this.name = name;
                this.surname = surname;
                this.patronymic = patronymic;
                this.position = position;
                this.gender = gender;
                this.ages = ages;
            }

            // Метод изменения значения фильтра
            public void ChangeFilterValue()
            {
                Console.WriteLine($"\nCurrent filter value: {ages}");
                ages = UInt32.Parse(Console.ReadLine());
            }
        }

        #endregion
    }
}
