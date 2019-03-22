using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vs_project
{
    class Program
    {
        #region Объявление перечислений Position и Gender

        
        // Должность работника
        enum Position { MANAGER, DIRECTOR, PROGRAMMER };
        // Пол работника
        enum Gender { Male, Female };

        #endregion

        #region Объявление структуры Worker
        //Хранит информацию о работнике и методы для работы с ними.
        struct Worker
        {
            /*Поля: 
             * name=Имя
             * surname=Фамилия
             * patronymic=Отчество
             * position=Должность
             * gender=Пол работника
             * dateReceiptOnWork=Дата приема на работу
            */
            private string name, surname, patronymic;
            private Position position;
            private Gender gender;
            private DateTime dateReceiptOnWork;

             /* Создание нового сотрудника
             * Поля: 
             *  name=Имя
             *  surname=Фамилия
             *  patronymic=Отчество
             *  position=Должность
             *  gender=Пол работника
             *  dateTime=Дата приема на работу
            */
            public Worker(string name, string surname, string patronymic, Position position, Gender gender, DateTime dateTime)
            {
                this.name = name;
                this.surname = surname;
                this.patronymic = patronymic;
                this.position = position;
                this.gender = gender;
                this.dateReceiptOnWork = dateTime;
            }
                        
            // Получение даты поступления работника
            public DateTime GetDateReceiptOnWork() {
                return dateReceiptOnWork;
            }

            public override string ToString()
            {
                return "";
            }
            
            //Метод для вывода работника
            public void WriteWorker()
            {
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Name: {surname}");
                Console.WriteLine($"Name: {patronymic}");
                Console.WriteLine($"Name: {position}");
                Console.WriteLine($"Name: {gender}");
                Console.WriteLine($"Name: {dateReceiptOnWork}");
                Console.WriteLine("______________________*_______________________");
            }
            
            //Метод для вывода всех работников
            public static void WriteAllWorkers(ref List<Worker> list)
            {
                Console.Clear();
                foreach (var worker in list)
                {
                    worker.WriteWorker();
                }
            }
            
            //Метод для добавления работника
            public static void AddWorker (ref List<Worker> list)
            {
                //Структура для добавления
                Worker worker = new Worker();
                Console.Clear();
                //Ввод имения, фамилии, отчества работника
                Console.Write("Введите имя нового работника: ");
                worker.name = Console.ReadLine();
                Console.Write("Введите фамилию нового работника: ");
                worker.surname = Console.ReadLine();
                Console.Write("Введите отчество нового работника: ");
                worker.patronymic = Console.ReadLine();

                //Ввод должности
                Console.Write("Выберите должность (1 - Менеджер, 2 - Директор, 3 - Программист): ");
                string pos = Console.ReadLine();
                //Выбор должности для сотрудника
                switch (pos)
                {
                    case "1":
                        worker.position = Position.MANAGER;
                        break;
                    case "2":
                        worker.position = Position.DIRECTOR;
                        break;
                    case "3":
                        worker.position = Position.PROGRAMMER;
                        break;
                }

                //Ввод пола
                Console.Write("Выберите пол (1 - Мужчина, 2 - Женщина): ");
                string ven = Console.ReadLine();
                //Выбор пола сотрудника
                switch(ven)
                {
                    case "1":
                        worker.gender = Gender.Male;
                        break;
                    case "2":
                        worker.gender = Gender.Female;
                        break;
                }

                //Ввод даты приёма на работу
                Console.Write("Введите дату приёма на работу в формате ДД.ММ.ГГГГ: ");
                worker.dateReceiptOnWork = Convert.ToDateTime(Console.ReadLine());

                //Добавление сотрудника в лист
                list.Add(worker);
            }
        }

        #endregion

        #region структура фильтра по опыту работы сотрудника

        struct ExperienceFilter
        {
            private uint ages;
            
            public ExperienceFilter(uint ages)
            {
                this.ages = ages;
            }

            public void ChangeFilterValue(uint ages)
            {
                this.ages = ages;
            }

            // Метод фильтрации данных
            public List<Worker> FilterWorkers(List<Worker> workers)
            {
                Console.Clear();
                List<Worker> filteredWorkers = new List<Worker>();
                foreach (Worker w in workers)
                {
                    uint exp = (uint)(DateTime.Now.Year - w.GetDateReceiptOnWork().Year);
                    if (exp > ages || ages == 0)
                    {
                        filteredWorkers.Add(w);
                        w.WriteWorker();
                    }
                }

                return filteredWorkers;
            }
        }

        #endregion

        static void Main(string[] args)
        {
            //Объвление списка работников
            List<Worker> workers=new List<Worker>();
            
            //Объвление переменной для работы с меню
            ConsoleKeyInfo key;
            ExperienceFilter filter = new ExperienceFilter(10);
            
            //Цикл для взаимодействия пользователя с программой
            do
            {
                // Вывод информации в консоль для пользователя
                Console.Clear();
                Console.WriteLine("Choose action:");
                Console.WriteLine("1- Add new worker");
                Console.WriteLine("2- Write all workers");
                Console.WriteLine("3- Write with filter");
                Console.WriteLine("4- Set value for filter");
                Console.WriteLine("Escape- Exit");
                // Считывание нажатой клавиши в key
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case (ConsoleKey.D1):
                        // Вызов метода для добавления в список нового работника
                        Worker.AddWorker(ref workers);
                        break;
                    case (ConsoleKey.D2):
                        // Вызов метода для вывода всех работников
                        Worker.WriteAllWorkers(ref workers);
                        Console.ReadKey();
                        break;
                    case (ConsoleKey.D3):
                        // Вызов метода для вывода работников удовлетворяющих фильтру
                        Worker.FiltrWorker(ref workers);
                        Console.ReadKey();
                        break;
                    case (ConsoleKey.D4):
                        // Вызов метода для изменения значения фильтра
                        uint age = UInt32.Parse(Console.ReadLine());
                        filter.ChangeFilterValue(age);
                        break;
                    default:
                        break;
                }
                //Сравнение key с клавишей Escape
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
