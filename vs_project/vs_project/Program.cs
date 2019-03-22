using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vs_project
{
    class Program
    {
        #region Объявление перечилений Position и Gender

        enum Position { MANAGER, DIRECTOR, PROGRAMMER };
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

            public Worker(string name, string surname, string patronymic, Position position, Gender gender, DateTime dateTime)
            {
                this.name = name;
                this.surname = surname;
                this.patronymic = patronymic;
                this.position = position;
                this.gender = gender;
                this.dateReceiptOnWork = dateTime;
            }

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
            public static void WriteAllWorkers(IList<Worker> list)
            {
                Console.Clear();
                foreach (var worker in list)
                {
                    worker.WriteWorker();
                }
            }
            //Метод для добавления работника
            public static void AddWorker (IList<Worker> list)
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
                if (pos == "1")
                {
                    worker.position = Position.MANAGER;
                }
                if (pos == "2")
                {
                    worker.position = Position.DIRECTOR;
                }
                if (pos == "3")
                {
                    worker.position = Position.PROGRAMMER;
                }

                //Ввод пола
                Console.Write("Выберите пол (1 - Мужчина, 2 - Женщина): ");
                string ven = Console.ReadLine();
                //Выбор пола сотрудника
                if (pos == "1")
                {
                    worker.gender = Gender.Male;
                }
                if (pos == "2")
                {
                    worker.gender = Gender.Female;
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

            public List<Worker> FilterWorkers(List<Worker> workers)
            {
                List<Worker> filteredWorkers = new List<Worker>();
                foreach (Worker w in workers)
                {
                    uint exp = (uint)(DateTime.Now.Year - w.GetDateReceiptOnWork().Year);
                    if (exp > ages || ages == 0)
                    {
                        filteredWorkers.Add(w);
                    }
                }

                return filteredWorkers;
            }
        }

        #endregion

        #region Определение функций для работы с списком
        static void addWorker(ref List<Worker> workers)
        {
            Console.Clear();
            Console.WriteLine("Введите имя:");
            String name=Console.ReadLine();
            Console.WriteLine("Введите фамилию:");
            String surname = Console.ReadLine();
            Console.WriteLine("Введит отчество:");
            String patronymic=Console.ReadLine();
        }
        

        static void writeWithFilter(ref List<Worker> workers)
        {

        }
        #endregion

        static void Main(string[] args)
        {
            List<Worker> workers=new List<Worker>();
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Choose action:");
                Console.WriteLine("1-Add new worker");
                Console.WriteLine("2-Write all workers");
                Console.WriteLine("3-Write with filter");
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case (ConsoleKey.D1):
                        Worker.AddWorker(workers);
                        break;
                    case (ConsoleKey.D2):
                        Worker.WriteAllWorkers(workers);
                        break;
                    case (ConsoleKey.D3):
                        writeWithFilter(ref workers);
                        break;
                    default:
                        break;
                }
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
