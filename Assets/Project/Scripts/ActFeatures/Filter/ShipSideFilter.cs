using Assets.Project.Scripts.ActFeatures.Actable;
using Assets.Project.Scripts.ActFeatures.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Project.Scripts.ActFeatures.Filter
{
    public class ShipSideFilter : IActableFilter
    {
        public IActable GetCorrectActable(List<IActable> allActable)
        {
            if (allActable == null || allActable.Count == 0)
                return null;

            IActable shipSide = allActable.FirstOrDefault(a => a is ShipSide);

            return shipSide;
        }
    }
}
