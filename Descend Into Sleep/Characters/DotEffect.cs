namespace ConsoleApp12.Characters
{
    public class DotEffect
    {
        public int NumberOfTurns;
        public readonly double DamagePerTurn;
        
        public DotEffect(int numberOfTurns, double damagePerTurn)
        {
            NumberOfTurns = numberOfTurns;
            DamagePerTurn = damagePerTurn;
        }
    }
}