using JetBrains.Annotations;

namespace AD.Mathematics.Tests
{
    [PublicAPI]
    public class WeightedRegressionObservation
    {
        public int Age { get; set; }

        public double Experience { get; set; }
        
        public double Income { get; set; }

        public double IncomeSquared => Income * Income;
        
        public bool OwnRent { get; set; }
        
        public bool SelfEmployed { get; set; }
    }
}