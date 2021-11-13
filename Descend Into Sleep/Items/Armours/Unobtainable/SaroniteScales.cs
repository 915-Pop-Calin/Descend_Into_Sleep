namespace ConsoleApp12.Items.Armours.Unobtainable
{
    public class SaroniteScales: Armour
    {
        public SaroniteScales() : base(0, 100)
        {
            HealthPoints = 100;
            Name = "Saronite Scales";
            Broken = false;
            SetReflector();
        }

        public override string TakeHit(double attackValue)
        {
            HealthPoints -= attackValue;
            if (HealthPoints <= 0)
            {
                SetDefenseValue(0);
                Name = "Broken Saronite Scales";
                Broken = true;
            }

            var toStr = Name + " have taken " + attackValue + " damage!\n";
            toStr += Name + " are left with " + HealthPoints + " health!\n";
            return toStr;
        }
    }
}