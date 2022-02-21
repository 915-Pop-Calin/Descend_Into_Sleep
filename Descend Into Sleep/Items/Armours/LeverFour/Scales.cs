namespace ConsoleApp12.Items.Armours.LeverFour
{
    public class Scales: Armour
    {
        public Scales() : base(0, 20, 100)
        {
            Name = "Scales";
            Description = "Scales of C'Thulhu. Does not serve as a great armour";
        }        
        
        public override double GetPrice()
        {
            return 1000;
        }
    }
}