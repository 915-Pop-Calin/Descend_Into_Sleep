namespace ConsoleApp12.Items.Armours.LevelThree
{
    public class BootsOfDodge: Armour
    {
        public BootsOfDodge() : base(0, 10, 0)
        {
            Dodge = 0.15;
            Name = "Boots Of Dodge";
            Description = "Gives you a small change of dodging auto attacks";
        }        
    }
}