namespace ConsoleApp12.Items.Weapons.LevelFive
{
    public class RadusBiceps: Weapon
    {
        public RadusBiceps() : base(75, 0, 0)
        {
            CriticalChance = -0.15;
            Name = "Radu's Biceps";
            Description = "Huge attack value, but it cannot critical strike";
        }
        
        public override double GetPrice()
        {
            return 3700;
        }
    }
}