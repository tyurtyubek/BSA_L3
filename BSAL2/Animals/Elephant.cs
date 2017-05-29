namespace BSA_2.Animals
{
    public class Elephant : IAnimal
    {
        int _maxHealthspoints;
        int _healthPoints;
        string _name;

        public Elephant(string name)
        {
            _name = name;
            AnimalCondition = State.full;
            _maxHealthspoints = 7;
            _healthPoints = 7;
        }

        public int MaxHealthPoints { get { return _maxHealthspoints; } }
        public int HealthPoints
        {
            get { return _healthPoints; }
            set { if (_healthPoints > 0 && _healthPoints <= _maxHealthspoints) _healthPoints = value; }
        }
        public string Name { get { return _name; } set { _name = value; } }
        public State AnimalCondition { get; set; }
    }
}
