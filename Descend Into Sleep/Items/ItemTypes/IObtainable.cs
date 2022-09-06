namespace ConsoleApp12.Items.ItemTypes
{
    public interface IObtainable : IItem
    {
        public double GetPrice();
        public int AvailabilityLevel();
    }
}