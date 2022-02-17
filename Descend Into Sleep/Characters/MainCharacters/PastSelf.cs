using ConsoleApp12.Ability.PastSelfAbilities;
using ConsoleApp12.Items;

namespace ConsoleApp12.Characters.MainCharacters
{
    public class PastSelf: Character
    {
        public PastSelf(string name, double innateAttack, double innateDefense, Weapon weapon, Armour armour,
            double health,
            string description, int level) : base(name, innateAttack, innateDefense, weapon, armour, health, description)
        {
            Level = 7;
            AddAbility(new Condemn(level));
            AddAbility(new Judge(level));
        }

        public override double GetOddsOfAttacking()
        {
            return 0.60;
        }
    }
}