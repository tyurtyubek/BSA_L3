using System;
using System.Collections.Generic;
using System.Linq;
using BSA_2.Animals;

namespace BSA_2
{
    class Zoo
    {
        IList<IAnimal> _animals;

        public Zoo()
        {
            _animals = new List<IAnimal>();
            _animals.Add(new Lion("KingLion"));
            _animals.Add(new Lion("KingLion2") { AnimalCondition = State.dead, HealthPoints = 0 });
            _animals.Add(new Tiger("Tigrenok") { AnimalCondition = State.sick });
            _animals.Add(new Tiger("Tigrenok2") { AnimalCondition = State.sick });
            _animals.Add(new Tiger("Tigrenok3"));
            _animals.Add(new Elephant("Bony") { AnimalCondition = State.dead, HealthPoints = 0 });
            _animals.Add(new Elephant("Bony2") { AnimalCondition = State.dead , HealthPoints = 0 });
            _animals.Add(new Bear("Balu"));
            _animals.Add(new Fox("McFoxy"));
            _animals.Add(new Bear("Balu2"));
            _animals.Add(new Wolf("Volk"));
        }

        public void Add(IAnimal animalState)
        {
            _animals.Add(animalState);
            Console.WriteLine($"You just added new animal: {animalState.GetType().Name} - {animalState.Name}");
        }

        public void Feed(string name)
        {
            IEnumerable<IAnimal> animalfeed = _animals.Where(animal => animal.Name == name);
            if (!animalfeed.Any())
                Console.WriteLine($"Can`t feed, there is no animal with name {name}");
            else
            {
                foreach (var animal in animalfeed)
                {
                    if (animal.AnimalCondition == State.sick) animal.AnimalCondition = State.hungry;
                    if (animal.AnimalCondition == State.hungry) animal.AnimalCondition = State.full;
                    if (animal.AnimalCondition == State.full) Console.WriteLine($"{animal.GetType().Name} - {name} is already full");
                    if (animal.AnimalCondition == State.dead) Console.WriteLine($"we can`t feed dead {name}");
                }
            }
        }

        public IList<IAnimal> GetAnimals()
        {
            return _animals;
        }

        public void Cure(string name)
        {
            IEnumerable<IAnimal> animalcure = _animals.Where(animal => animal.Name == name);
            if (!animalcure.Any())
                Console.WriteLine($"Can`t cure, there is no animal with name {name}");
            else
            {
                foreach (var animal in animalcure)
                {
                    if (animal.HealthPoints < animal.MaxHealthPoints) animal.HealthPoints++;
                    Console.WriteLine($"We cured {animal.GetType().Name}-{name} and health point = {animal.HealthPoints}");
                }
            }
        }

        public void Remove()
        {
            var animalsdead = _animals.Where(animal => animal.AnimalCondition == State.dead);
            if (animalsdead.Count() >= 1)
            {
                foreach (var element in animalsdead.ToArray())
                {
                    _animals.Remove(element);
                    Console.WriteLine($"{element.Name} was removed from zoo");
                }
            }
            else Console.WriteLine("There is no dead animal in the zoo");
        }

        public void Display()
        {
            Console.WriteLine("All residents of the zoo are - {0} ", _animals.Count());
            foreach (var resident in _animals)
            {
                Console.WriteLine($"{resident.GetType().Name} - Name: {resident.Name}, " +
                    $"state: {resident.AnimalCondition}, health point: {resident.HealthPoints}");
            }
        }

        public int CountAnimals { get { return _animals.Count(); } }

        public void ShowGroupByTypeOfAnimal()
        {
            var animalbytype = _animals.GroupBy(animal => animal.GetType().Name)
                                       .Select(animal => new
                                       {
                                           AnimalType = animal.Key,
                                           AnimalName = animal.Select(s => s)
                                       });
            foreach (var elem in animalbytype)
            {
                Console.WriteLine($"Animals grouped by {elem.AnimalType}: ");
                foreach (var anim in elem.AnimalName)
                {
                    Console.WriteLine(anim.Name);
                }
            }
        }

        public void ShowAnimalByState(State state)
        {
            var animalsByState = _animals.Where(a => a.AnimalCondition == state);
            foreach (var elem in animalsByState)
            {
                Console.WriteLine(elem.Name + " " + elem.GetType().Name + " " + elem.AnimalCondition);
            }
        }

        public void ShowAllSickTigers()
        {
            var sicktigers = _animals
                .Where(animal => animal.GetType().Name == "Tiger" && animal.AnimalCondition == State.sick);
            foreach (var item in sicktigers)
            {
                Console.WriteLine($"{item.Name} {item.GetType().Name} is sick now");
            }
        }

        public void ShowElephantByName(string name)
        {
            string elephantbyname = "";
            var elephants = _animals.Where(animal => animal.GetType().Name == "Elephant" && animal.Name == name);
            foreach (var item in elephants)
            {
                if (elephants.Count() == 1)
                    Console.WriteLine($"There is one Elephant with name {item.Name}");
                else elephantbyname = " " + item.Name;
            }
            if (elephants.Count() > 1)
                Console.WriteLine($"There are {elephants.Count()} elephants wih names:{elephantbyname}");
        }

        public void ShowAllHungryAnimalNames()
        {
            var hungryAnimals = _animals.Where(anymal => anymal.AnimalCondition == State.hungry);
            foreach (var item in hungryAnimals)
            {
                Console.WriteLine(item.Name);
            }
        }

        public void ShowHealthiestByTypeAnimals()
        {
            var healthiestanimals = _animals.OrderByDescending(animal => animal.HealthPoints)
                .GroupBy(animal => animal.GetType().Name)
                .Select(a => new
                {
                    AnimalType = a.Key,
                    Animals = a.Select(animal => animal).FirstOrDefault()
                });
            foreach (var element in healthiestanimals)
            {
                Console.WriteLine(element.Animals.GetType().Name + " " + element.Animals.Name + " " + element.Animals.HealthPoints);
            }
        }

        public void ShowNumerOfDeadAnimalsByType()
        {
            var nuberdeadanimals = _animals.Where(param => param.AnimalCondition == State.dead)
                                            .GroupBy(param => param.GetType().Name)
                                            .Select(a => new
                                            {
                                                AnimalType = a.Key,
                                                DeadAnimals = a.Count(b => b.AnimalCondition == State.dead)
                                            });
            foreach (var animal in nuberdeadanimals)
            {
                Console.WriteLine($"Dead {animal.AnimalType} in numer of {animal.DeadAnimals}");
            }
        }

        public void ShowAllWolfesAndBearsHealthMoreThan3()
        {
            var healthmore3 = _animals.Where(param => param.HealthPoints > 3)
                                      .Where(param => param.GetType().Name == "Bear" || param.GetType().Name == "Wolf")
                                      .Select(param => new
                                      {
                                          AnimalName = param.Name,
                                          AnimalType = param.GetType().Name,
                                          AnimalHealth = param.HealthPoints
                                      });

            foreach (var item in healthmore3)
            {
                Console.WriteLine(item.AnimalType + " " + item.AnimalName + " " + item.AnimalHealth);
            }

        }

        public void AnimalsWIthMinAndMaxHealth()
        {
            var minmaxhealth = _animals.Select(par => new
                {
                    Max = _animals.OrderBy(p => p.HealthPoints).First(),
                    Min = _animals.OrderBy(p => p.HealthPoints).Last()
                })
                .First();

            Console.WriteLine(minmaxhealth.Max.GetType().Name + " " + minmaxhealth.Max.Name + " " + minmaxhealth.Max.HealthPoints);
            Console.WriteLine(minmaxhealth.Min.GetType().Name + " " + minmaxhealth.Min.Name + " " + minmaxhealth.Min.HealthPoints);
        }

        public void AverageHealthInAnimals()
        {
            var avg = _animals.Average(par => par.HealthPoints);
            Console.WriteLine("Average healthpoints in the zoo are {0}", avg);
        }
    }
}
