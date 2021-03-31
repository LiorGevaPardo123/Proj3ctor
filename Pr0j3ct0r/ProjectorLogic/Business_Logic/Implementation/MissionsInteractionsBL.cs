using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Data_Access_Layer.Implementaion;
using ProjectorLogic.Data_Access_Layer.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Implementation
{
    public class MissionsInteractionsBL : IMissionsInteractionsBL
    {
        IMissionsInteractionsDal missionsInteractionsDal;
        IMissionDal missionsDal;
        
        public MissionsInteractionsBL()
        {
            missionsInteractionsDal = new MissionsInteractionsDal();
            missionsDal = new MissionDal();
        }

        public void CreateInteraction(int Id1, int Id2)
        {
            missionsInteractionsDal.CreateNewInteraction(Id1, Id2);
        }

        public void CreateInteractionsGraph(int projectId)
        {
            List<Mission> allProjectMissions = missionsDal.GetAllMissions()
                .Where(mission => mission.ProjectId.Equals(projectId)).ToList();

            List<MissionInteractionEntity> allInteractions = missionsInteractionsDal
               .GetAllMissionsInteractions();

            List<MissionInteractionEntity> projectInetactions= allInteractions
                .Where(inter => allProjectMissions.Select(p => p.Id).Contains(inter.PreviousId))
                .ToList();                                 
        }

        public void DeleteInteraction(int Id1, int Id2)
        {
            missionsInteractionsDal.DeleteInteraction(Id1,Id2);
        }

        public List<Mission> GetNextMissions(Mission m)
        {
            List<Mission> allMissions = missionsDal.GetAllMissions();
            return missionsInteractionsDal
                .GetAllMissionsInteractions()
                .Where(interaction => interaction.PreviousId.Equals(m.Id))
                .Select(interaction => allMissions
                    .First(mission => mission.Id.Equals(interaction.NextId))
                    )
                .ToList();
        }

        public List<Mission> GetPreviousMissions(Mission m)
        {
            List<Mission> allMissions = missionsDal.GetAllMissions();
            return missionsInteractionsDal
                .GetAllMissionsInteractions()
                .Where(interaction => interaction.NextId.Equals(m.Id))
                .Select(interaction => allMissions
                    .First(mission => mission.Id.Equals(interaction.PreviousId))
                    )
                .ToList();
        }
    }
}
