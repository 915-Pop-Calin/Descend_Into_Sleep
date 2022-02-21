namespace ConsoleApp12.Items.Weapons.LevelFive
{
    public class InfinityEdge: Weapon
    {
        public InfinityEdge() : base(20, 0, 0)
        {
            CriticalChance = 0.35;
            Name = "Infinity Edge";
            Description = $"Increases your critical strike chance by {CriticalChance * 100}%";
        }        
        
        public override double GetPrice()
        {
            return 2500;
        }
    }
}