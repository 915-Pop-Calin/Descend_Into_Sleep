namespace ConsoleApp12.Items.Weapons.LevelOne
{
    public class Words: Weapon
    {
        public Words() : base(0, 0, 0)
        {
            Name = "Words";
            Description = "It is said that words cannot hurt you";
        }        
        
        public override double GetPrice()
        {
            return 50;
        }
    }
}