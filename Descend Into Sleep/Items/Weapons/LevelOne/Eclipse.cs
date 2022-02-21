namespace ConsoleApp12.Items.Weapons.LevelOne
{
    public class Eclipse: Weapon
    {
        public Eclipse() : base(5, -2, 0)
        {
            SetLifeSteal(0.15);
            Name = "Eclipse";
            Description = "Strong life stealer at the cost of your defense";
        }
        
        public override double GetPrice()
        {
            return 400;
        }
    }
}