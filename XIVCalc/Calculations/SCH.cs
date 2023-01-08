namespace XIVCalc.Calculations
{
    internal static class SCH
    {
        //potencies for our spells
        const int R2 = 220;
        const int B4 = 295;
        const int BioDot = 70;
        const int energyDrain = 100;

        public static double GetPSch(double shortGCD, double R2s, int sps, double ED, double filler, double cycle, int lvl)
        {
            double result = 0;
            result += ED * energyDrain * 60 / cycle;
            // 1 bio + x B4
            if (B4 > BioDot / 3 * StatEquations.CalcAADotMultiplier(sps, lvl) * Math.Floor(30 / shortGCD) * (shortGCD - 30 % shortGCD))
            {
                result += 6 * (Math.Ceiling((30) / (shortGCD)) - 1) * B4;
                result -= (B4 - R2) * R2s * cycle / 60;
                result += 6 * 10 * StatEquations.CalcAADotMultiplier(sps, lvl) * BioDot;
            }
            else
            {
                result += 6 * (Math.Floor((30) / (shortGCD)) - 1) * B4;
                result -= (B4 - R2) * R2s * 60 / cycle;
                result += 6 * 9 * StatEquations.CalcAADotMultiplier(sps, lvl) * BioDot;
                result += 6 * ((3 - (30 % shortGCD)) / 3) * StatEquations.CalcAADotMultiplier(sps, lvl) * BioDot;
            }
            result -= filler * B4 * cycle / 60;
            return result;
        }

        public static double GetMPSch(int sps, double shortGCD, double LDs, bool ether, double suc, double adlo, double ED, double rezz, double cycle, int lvl)
        {
            double result = 0;
            if (B4 > BioDot / 3 * StatEquations.CalcAADotMultiplier(sps, lvl) * Math.Floor(30 / shortGCD) * (shortGCD - 30 % shortGCD))
            {
                result += 6 * (Math.Ceiling((30) / (shortGCD))) * 400;
            }
            else
            {
                result += 6 * (Math.Floor((30) / (shortGCD))) * 400;
            }
            result += 600 * suc * cycle / 60;
            result += 600 * adlo * cycle / 60;
            result += 2000 * rezz * cycle / 60;
            //AF
            result -= 2000 * cycle / 60;
            //LD
            result -= (3850) * LDs * cycle / 60;
            if (ether)
                result -= 1400 / 270 * cycle;
            return result;
        }
        // Actual time taken by a 180s rotation
        public static double GetCycleSch(double shortGCD, int sps, int lvl)
        {
            double result = 0;
            //1 bio + x Broils
            if (B4 > BioDot / 3 * StatEquations.CalcAADotMultiplier(sps, lvl) * Math.Floor(30 / shortGCD) * (shortGCD - 30 % shortGCD))
            {
                result += 6 * (Math.Ceiling((30) / (shortGCD)) * (shortGCD));
            }
            else
            {
                result += 6 * (Math.Floor((30) / (shortGCD)) * (shortGCD));
            }
            return result;
        }

        public static double PPSSch(int sps, double shortGCD, double R2s, double fillers, double ED, int lvl)
        {
            double cycle = GetCycleSch(shortGCD, sps, lvl);
            return GetPSch(shortGCD, R2s, sps, ED, fillers, cycle, lvl) / cycle;
        }

        public static double MPPSSch(int sps, double shortGCD, double LDs, bool ether, double suc, double adlo, double ED, double rezz, int lvl)
        {
            double cycle = GetCycleSch(shortGCD, sps, lvl);
            return GetMPSch(sps, shortGCD, LDs, ether, suc, adlo, ED, rezz, cycle, lvl) / cycle;
        }

        public static double MPTimeSch(int pie, int lvl, int sps, double shortGCD, double LDs, bool ether, double suc, double adlo, double ED, double rezz)
        {
            double result = 0;
            result += StatEquations.CalcMPPerSecond(pie, lvl) / 3;
            result -= MPPSSch(sps, shortGCD, LDs, ether, suc, adlo, ED, rezz, lvl);
            return Math.Floor(-10000 / result);
        }
    }
}
