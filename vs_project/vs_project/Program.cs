﻿using System;
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

        struct Worker
        {
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

        static void writeAllWorkers(ref List<Worker> workers)
        {
            Console.Clear();
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
                        addWorker(ref workers);
                        break;
                    case (ConsoleKey.D2):
                        writeAllWorkers(ref workers);
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
