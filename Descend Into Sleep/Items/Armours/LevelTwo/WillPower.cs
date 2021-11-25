namespace ConsoleApp12.Items.Armours.LevelTwo
{
    public class WillPower: Armour
    {
        public WillPower() : base(0, 10, 0)
        {
            Name = "Will Power";
            Description = "Gives you a little sanity";
            Sanity = 50;
        }
    }
}