namespace ConsoleApp12.Items.Armours.LevelFive
{
    public class NinjaYoroi: Armour
    {
        public NinjaYoroi() : base(0, 0, 0)
        {
            Name = "Ninja Yoroi";
            Dodge = 0.5;
            Description = "Armour with no defense points but gives 50% dodge chance";
        }
    }
}