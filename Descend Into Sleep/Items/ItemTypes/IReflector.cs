namespace ConsoleApp12.Items.ItemTypes
{
    public interface IReflector
    {
        public string TakeHit(double attackValue);

        public bool IsBroken();
    }
}