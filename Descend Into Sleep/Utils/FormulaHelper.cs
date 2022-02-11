using System;

namespace ConsoleApp12.Utils
{
    public class FormulaHelper
    {
        private double GetTheoreticalPercentage(int actsLeft, int totalActs, double epsilon = 0.125,
            double power = (double) 2 / 3)
        {
            double ratio = (double) actsLeft / totalActs;
            return (1 - epsilon) * Math.Pow(ratio, power) + epsilon;
        }

        private double GetAttackValueDifference(int actsLeft, int totalActs, double attackValue)
        {
            double previousRatio = GetTheoreticalPercentage(actsLeft + 1, totalActs);
            double currentRatio = GetTheoreticalPercentage(actsLeft, totalActs);
            double ratioDifference = previousRatio - currentRatio;
            return attackValue * ratioDifference;
        }
    }
}