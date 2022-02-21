namespace ConsoleApp12.Items.Weapons.LevelOne
{
    public class ToyKnife: Weapon
    {
        public ToyKnife() : base(3, 0, 0)
        {
            Name = "Toy Knife";
            Description = "Is this a game?";
        }        
        
        public override double GetPrice()
        {
            return 50;
        }
    }
}