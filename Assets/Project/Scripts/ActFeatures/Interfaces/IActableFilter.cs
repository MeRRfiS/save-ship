using System.Collections.Generic;

namespace Assets.Project.Scripts.ActFeatures.Interfaces
{
    public interface IActableFilter
    {
        public IActable GetCorrectActable(List<IActable> allActable);
    }
}