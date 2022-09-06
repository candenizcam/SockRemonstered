using System.Collections.Generic;
using System.Linq;

namespace MatchDots
{
    public class DotsScoreboard
    {
        private List<DotsTarget> _targets;
        
        public DotsScoreboard(DotsLevelsInfo dli)
        {
            _targets = dli.Targets.ToList();
        }
        
    }
}