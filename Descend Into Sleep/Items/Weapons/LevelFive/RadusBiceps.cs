namespace ConsoleApp12.Items.Weapons.LevelFive
{
    public class RadusBiceps: IWeapon, IObtainable, ICriticalChance
    {
        public double GetAttackValue()
        {
            return 75;
        }
        
        public string GetName()
        {
            return "Radu's Biceps";
        }

        public string GetDescription()
        {
            return "Huge attack value, but it cannot critical strike";
        }
        

        public double GetCriticalChance()
        {
            return -0.15;
        }
        
        public double GetPrice()
        {
            return 3700;
        }
    }
}