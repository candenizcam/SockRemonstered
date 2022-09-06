using System.Collections.Generic;
using System.Linq;

namespace MatchDots
{
    public class DotsScoreboard
    {
        private List<DotsTarget> _targets;
        private List<int> _removedTypes;
        public int MoveCounter { get; private set; } = 0;
        
        public DotsScoreboard(DotsLevelsInfo dli)
        {
            _targets = dli.Targets.ToList();
        }

        public void AddToRemoved(List<int> r)
        {
            _removedTypes.AddRange(r);
            MoveCounter += 1;
        }

        public List<DotsTarget>  GetRems()
        {
            var rems = new List<DotsTarget>();

            
            

            return rems;
        }
        
        
    }
}