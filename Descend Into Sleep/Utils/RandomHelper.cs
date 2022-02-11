using System;

namespace ConsoleApp12.Utils
{
    public class RandomHelper
    {
        public static bool IsSuccessfulTry(double successRate, int numberOfTries = 100)
        {
            var randomObject = new Random();
            var randomChoice = randomObject.Next(numberOfTries);
            var ratio = (double) randomChoice / (double) numberOfTries;
            return ratio < successRate;
        }

        public static int GenerateRandomInInterval(int lowerBoundInclusive, int upperBoundExclusive)
        {
            var randomObject = new Random();
            var randomChoice = randomObject.Next(lowerBoundInclusive, upperBoundExclusive);
            return randomChoice;
        }
    }
}