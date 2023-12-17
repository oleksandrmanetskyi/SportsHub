using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.RecommendationModule.Abstractions
{
    public interface IComparer
    {
        double CompareVectors(double[] userFeaturesOne, double[] userFeaturesTwo);
    }
}
