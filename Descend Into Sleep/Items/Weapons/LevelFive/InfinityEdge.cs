namespace ConsoleApp12.Items.Weapons.LevelFive
{
    public class InfinityEdge: Weapon
    {
        public InfinityEdge() : base(20, 0)
        {
            CriticalChance = 0.35;
            Name = "Infinity Edge";
            Description = "Increases your critical strike chance by 35%";
        }        
    }
}