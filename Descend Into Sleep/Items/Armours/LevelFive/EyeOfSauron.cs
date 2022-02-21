namespace ConsoleApp12.Items.Armours.LevelFive
{
    public class EyeOfSauron: Armour
    {
        public EyeOfSauron() : base(0, 200, 0)
        {
            Name = "Eye Of Sauron";
            Description = "Strong armour with no drawbacks";
        }

        public override double GetPrice()
        {
            return 5000;
        }
    }
}