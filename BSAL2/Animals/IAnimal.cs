using BSA_2.Animals;

namespace BSA_2
{
    interface IAnimal
    {
        int MaxHealthPoints { get; }
        int HealthPoints { get; set; }
        string Name { get; set; }
        State AnimalCondition { get; set; }
    }
}
