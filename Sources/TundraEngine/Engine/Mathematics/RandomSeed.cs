using System;

namespace Engine.Mathematics
{
    public struct RandomSeed
    {
        private readonly uint _seed;

        private const double GelfondConst = 23.1406926327792690;            // e to the power of Pi = (-1) to the power of -i
        private const double GelfondSchneiderConst = 2.6651441426902251;    // 2 to the power of sqrt(2)
        private const double Numerator = 123456789;
        private const uint UnderflowGuard = 0xFFFF;

        public RandomSeed(uint seed)
        {
            _seed = (seed & UnderflowGuard);
        }

        public double GetDouble(uint offset)
        {
            var dRand = (double)(unchecked(_seed + offset)); // We want it to overflow
            var dotProduct = Math.Cos(dRand) * GelfondConst + Math.Sin(dRand) * GelfondSchneiderConst;
            var denominator = 1e-7 + 256 * dotProduct;
            var remainder = Numerator % denominator;
            return (remainder - Math.Floor(remainder));
        }

        public float GetFloat(uint offset) => (float)GetDouble(offset); 
    }
}