namespace ConsoleApp12.Items.Armours.LevelTwo
{
    public class WillPower: Armour
    {
        public WillPower() : base(0, 10, 0)
        {
            Name = "Will Power";
            Sanity = 50;
            Description = $"Gives you {Sanity} extra sanity";
        }
    }
}