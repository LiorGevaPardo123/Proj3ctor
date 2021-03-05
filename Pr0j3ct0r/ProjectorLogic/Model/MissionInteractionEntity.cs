using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Model
{
    public class MissionInteractionEntity
    {
        private int previousId;
        public int PreviousId
        {
            get { return previousId; }
            set { previousId = value; }
        }

        private int nextId;
        public int NextId
        {
            get { return nextId; }
            set { nextId = value; }
        }

        public MissionInteractionEntity()
        {

        }

        public MissionInteractionEntity(int previousId, int nextId)
        {
            PreviousId = previousId;
            NextId = nextId;
        }
    }
}
