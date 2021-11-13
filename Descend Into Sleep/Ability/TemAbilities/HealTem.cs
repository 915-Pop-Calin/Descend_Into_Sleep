using System;
using System.Collections.Generic;
using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;

namespace ConsoleApp12.Ability.TemAbilities
{
    public class HealTem: Ability
    {
        private readonly double HealthHealed;
           
        public HealTem() : base("Heal Tem")
        {
            HealthHealed = 1;
        }

        public override string Cast(Character caster, Character opponent, Dictionary<int, List<Func<Character, Character, string>>> listOfTurns, int turnCounter)
        {
            var casterName = caster.GetName();
            caster.Heal(HealthHealed);
            var toStr = casterName + " heals for " + HealthHealed + "!\n";
            toStr += casterName + " is now at " + caster.GetHealthPoints() + " health!\n";
            return toStr;
        }

        public override string Decast(Character caster, Character opponent)
        {
            throw new InexistentDecastException(Name);
        }   
    }
}