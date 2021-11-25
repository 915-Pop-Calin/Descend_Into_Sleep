namespace ConsoleApp12.Items.Weapons.LevelFour
{
    public class GiantSlayer: Weapon
    {
        public GiantSlayer() : base(20, 0, 0)
        {
            Name = "Giant Slayer";
            Description = "Great against high armour monsters";
            ArmorPenetration = 0.4;
        }        
    }
}