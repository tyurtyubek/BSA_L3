using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using BSA_2.Animals;

namespace BSA_2
{
    class ZooAlive
    {
        private Zoo _zoo;
        private System.Timers.Timer aTimer;
        public ZooAlive()
        {
            _zoo = new Zoo();
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 5000;
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }
        public void LetsZooAlive()
        {
            while (Timer5() == 1)
            {
                Console.WriteLine("Enter 1 for displaing all animal\n" +
                    "Enter 2 for adding new animal\n" +
                    "Enter 3 for feeding animal\n" +
                    "Enter 4 for curing animal\n" +
                    "Enter 5 for removing animal\n" +
                    "Enter 6 for starting quering\n");

                try
                {
                    int number = Convert.ToInt32(Console.ReadLine());

                    switch (number)
                    {
                        case 1:
                            {
                                _zoo.Display();
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Enter animal type: 1-lion 2-tiger 3-elephant 4-bear 5-wolf 6-fox");
                                aTimer.Enabled = false;
                                int number2 = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Please enter animal name");
                                string name = Console.ReadLine();
                                aTimer.Enabled = true;
                                switch (number2)
                                {
                                    case 1: _zoo.Add(new Lion(name)); break;
                                    case 2: _zoo.Add(new Tiger(name)); break;
                                    case 3: _zoo.Add(new Elephant(name)); break;
                                    case 4: _zoo.Add(new Bear(name)); break;
                                    case 5: _zoo.Add(new Wolf(name)); break;
                                    case 6: _zoo.Add(new Fox(name)); break;
                                }
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Please enter name animal for feeding\n");
                                aTimer.Enabled = false;
                                string name = Console.ReadLine();
                                aTimer.Enabled = true;
                                _zoo.Feed(name);
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("Please enter name animal for curing\n");
                                aTimer.Enabled = false;
                                string name = Console.ReadLine();
                                aTimer.Enabled = true;
                                _zoo.Cure(name);
                                break;
                            }
                        case 5:
                            {
                                _zoo.Remove();
                                break;
                            }
                        case 6:
                            {
                                Console.WriteLine("Chose query you need:\n" +
                                    " 1- Group by Type of Animal\n" +
                                    " 2- Show animals by State\n" +
                                    " 3- Show all sick Tigers \n" +
                                    " 4- Show Elephant by Name \n" +
                                    " 5- Show all Hungry animals\n" +
                                    " 6- Show The Healthiest animals by type of animals\n" +
                                    " 7- Show number of Dead Animals by every type of animals\n" +
                                    " 8- Show all Wolfes and Bears wich health more than 3\n" +
                                    " 9- Show Animals With Min and Max health\n" +
                                    " 10- Show average animals health in the zoo\n");
                                aTimer.Enabled = false;
                                int chose = Convert.ToInt32(Console.ReadLine());
                                aTimer.Enabled = true;
                                switch (chose)
                                {
                                    case 1:
                                        {
                                            _zoo.ShowGroupByTypeOfAnimal();
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.WriteLine("Please, chose State: 1 - full, 2- hungry, 3 - sick, 4 - dead");
                                            aTimer.Enabled = false;                                           
                                            int chose2 = Convert.ToInt32(Console.ReadLine());
                                            aTimer.Enabled = true;
                                            switch (chose2)
                                            {
                                                case 1: { _zoo.ShowAnimalByState(State.full); break; }
                                                case 2: { _zoo.ShowAnimalByState(State.hungry); break; }
                                                case 3: { _zoo.ShowAnimalByState(State.sick); break; }
                                                case 4: { _zoo.ShowAnimalByState(State.dead); break; }
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            _zoo.ShowAllSickTigers();
                                            break;
                                        }
                                    case 4:
                                        {
                                            Console.WriteLine("Please, enter animal name");
                                            aTimer.Enabled = false;
                                            string tmp = Console.ReadLine();
                                            aTimer.Enabled = true;
                                            _zoo.ShowElephantByName(tmp);
                                            break;
                                        }
                                    case 5:
                                        {
                                            _zoo.ShowAllHungryAnimalNames();
                                            break;
                                        }
                                    case 6:
                                        {
                                            _zoo.ShowHealthiestByTypeAnimals();
                                            break;
                                        }
                                    case 7:
                                        {
                                            _zoo.ShowNumerOfDeadAnimalsByType();
                                            break;
                                        }
                                    case 8:
                                        {
                                            _zoo.ShowAllWolfesAndBearsHealthMoreThan3();
                                            break;
                                        }
                                    case 9:
                                        {
                                            _zoo.AnimalsWIthMinAndMaxHealth();
                                            break;
                                        }
                                    case 10:
                                        {
                                            _zoo.AverageHealthInAnimals();
                                            break;
                                        }
                                }                              
                                break;
                            }
                    }
                }
                catch
                {
                    Console.WriteLine("Eror entering");
                }
            }

        }
        public int Timer5()
        {
            if (_zoo.CountAnimals == 0) { return 0; aTimer.Enabled = false; }
            return 1;
        }
        public void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            var rand = new Random();
            int random = rand.Next(0, _zoo.CountAnimals);
            if (_zoo.CountAnimals != 0)
            {
                var animal = _zoo.GetAnimals().ElementAt(random);
                if (animal.AnimalCondition == State.full)
                {
                    animal.AnimalCondition = State.hungry;
                    Console.WriteLine($"{animal.Name} change state from full -> hungry ");
                }

                else if (animal.AnimalCondition == State.hungry)
                {
                    animal.AnimalCondition = State.sick;
                    Console.WriteLine($"{animal.Name} change state from hungry ->  sick");
                }

                else if (animal.AnimalCondition == State.sick)
                {
                    animal.HealthPoints--;
                    if (animal.HealthPoints == 0)
                    {
                        animal.AnimalCondition = State.dead;
                        //_zoo.Remove();
                        Console.WriteLine($"{animal.Name} died, enter 5 if you wanna remove her or him from zoo");

                    }
                    Console.WriteLine($"{animal.Name} Lost health position and now health is {animal.HealthPoints}");
                }
            }
        }


    }
}
