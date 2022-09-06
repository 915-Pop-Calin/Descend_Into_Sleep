using ConsoleApp12.Characters;
using ConsoleApp12.Exceptions;
using ConsoleApp12.Utils;

namespace ConsoleApp12.Ability.HumanAbilities.SelfHarmAbilities
{
    public class ChaosEnsues : Ability
    {
        private const double SET_HEALTH_POINTS = 0.5;
        private const int TURN_COOLDOWN = 7;

        public ChaosEnsues() : base("Chaos Ensues")
        {
            ManaCost = 50;
            TurnsUntilDecast = 3;
            Description =
                $"You are unable to life steal and your health is set to {SET_HEALTH_POINTS}, but your opponent is stunned" +
                $" for {TurnsUntilDecast} Turns\n";
        }

        public override void ResetDescription()
        {
            Description =
                $"You are unable to life steal and your health is set to {SET_HEALTH_POINTS}, but your opponent is stunned" +
                $" for {TurnsUntilDecast} Turns\n";
        }

        public override string Cast(Character caster, Character opponent, ListOfTurns listOfTurns, int turnCounter)
        {
            if (!Available)
                throw new CooldownException(Name);
            string toStr = GetCastingString(caster);
            caster.SetLifeStealStatus(false);
            caster.SetHealthPoints(SET_HEALTH_POINTS);
            opponent.Stun();
            Available = false;
            toStr +=
                $"{opponent.GetName()} is now stunned!\nYou can no longer life steal!\nYour health is set to 0.5!\n";
            AddToDecastingQueue(caster, opponent, listOfTurns, turnCounter);
            listOfTurns.Add(turnCounter + TURN_COOLDOWN, (c1, c2) => SecondDecast(c1, c2));
            return toStr;
        }

        protected override string Decast(Character caster, Character opponent)
        {
            caster.SetLifeStealStatus(true);
            opponent.Unstun();
            string toStr = $"{caster.GetName()} can now life steal!\n{opponent.GetName()} is no longer stunned!\n";
            return toStr;
        }

        private string SecondDecast(Character caster, Character opponent)
        {
            Available = true;
            string toStr = $"{Name} is now available!\n";
            return toStr;
        }
    }
}