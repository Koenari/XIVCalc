namespace XIVCalc.Calculations
{
    internal static class WHM
    {
        //potencies for our spells
        const int glare = 310;
        const int dia = 60;
        const int diaDot = 60;
        const int assize = 400;
        const int misery = 1240;

        static double AfflatusTime(double shortGcd, double cycle)
        {
            // 6 = something to do with lillies?
            return 6 * shortGcd * (cycle / 360 - 1);
        }

        // Average potency of a 360s rotation
        static double GetPWHM(int sps, double shortGcd, double filler, double cycle, int lvl)
        {
            double result = 0;
            result += 8 * assize * cycle / 360;
            result += 6 * misery * cycle / 360;
            if (glare - dia > diaDot / 3 * StatEquations.CalcAADotMultiplier(sps, lvl) * Math.Floor(30 / shortGcd) * (shortGcd - 30 % shortGcd))
            {
                result += 12 * (Math.Ceiling(30 / shortGcd) - 1) * glare + 12 * dia - 24 * glare;
                result += 12 * 10 * StatEquations.CalcAADotMultiplier(sps, lvl) * diaDot;
            }
            else
            {
                result += 12 * (Math.Floor(30 / shortGcd) - 1) * glare + 12 * dia - 24 * glare;
                result += 12 * 9 * StatEquations.CalcAADotMultiplier(sps, lvl) * diaDot;
                result += 12 * ((3 - (30 % shortGcd)) / 3) * StatEquations.CalcAADotMultiplier(sps, lvl) * diaDot;
            }
            result -= filler * glare * cycle / 60;
            return result;
        }

        static double GetMPWHM(int sps, double shortGcd, double LDs, bool ether, double m2s, double c3s, double rezz, double cycle, int lvl)
        {
            double result = 0;

            if (glare - dia > diaDot / 3 * StatEquations.CalcAADotMultiplier(sps, lvl) * Math.Floor(30 / shortGcd) * (shortGcd - 30 % shortGcd))
            {
                result += 12 * (Math.Ceiling(30 / shortGcd)) * 400;
            }
            else
            {
                result += 12 * (Math.Floor(30 / shortGcd)) * 400;
            }
            //misery + lillies
            result -= 400 * cycle / 15;
            //assize
            result -= (500) * cycle / 45;
            result -= (3850) * LDs * cycle / 60;
            result += rezz * 2000 * cycle / 60;
            result += 600 * m2s * cycle / 60;
            result += 1100 * c3s * cycle / 60;
            //thin air
            result -= 400 * cycle / 60;
            if (ether)
                result -= 1400 / 270 * cycle;
            return result;
        }

        // Actual time taken by a 360s rotation
        public static double GetCycleWHM(double shortGCD, int sps, int lvl)
        {
            double result = 0;
            //1 dia + x glares/lily/misery
            if (glare - dia > diaDot / 3 * StatEquations.CalcAADotMultiplier(sps, lvl) * Math.Floor(30 / shortGCD) * (shortGCD - 30 % shortGCD))
            {
                result += 12 * (Math.Ceiling(30 / shortGCD) * shortGCD);
            }
            else
            {
                result += 12 * (Math.Floor(30 / shortGCD) * shortGCD);
            }
            // POM as multiplier normalized over 360s
            result *= 360 / ((45 / 0.80) + 315);
            return result;
        }
        public static double PPSWHM(int sps, double shortGCD, double fillers, int lvl)
        {
            double cycle = GetCycleWHM(shortGCD, sps, lvl);
            double afflatusT = AfflatusTime(shortGCD, cycle);
            cycle += afflatusT;
            return GetPWHM(sps, shortGCD, fillers, cycle, lvl) / cycle;
        }

        public static double MPPSWHM(int sps, double shortGCD, double LDs, bool ether, double c3s, double m2s, double rezz, int lvl)
        {
            double cycle = GetCycleWHM(shortGCD, sps, lvl);
            double afflatusT = AfflatusTime(shortGCD, cycle);
            cycle += afflatusT;
            return GetMPWHM(sps, shortGCD, LDs, ether, m2s, c3s, rezz, cycle, lvl) / cycle;
        }

        public static double MPTimeWHM(int pie, int lvl, int sps, double shortGCD, double LDs, bool ether, double c3s, double m2s, double rezz)
        {
            double result = 0;
            result += StatEquations.CalcMPPerSecond(pie, lvl) / 3;
            result -= MPPSWHM(sps, shortGCD, LDs, ether, c3s, m2s, rezz, lvl);
            return Math.Floor(-10000 / result);
        }
    }
}

