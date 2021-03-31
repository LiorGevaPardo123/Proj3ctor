using CriticalPathMethod;
using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Implementation
{
    public class CriticalPath : ICriticalPath
    {
        private const int EndState = -2;
        private const int StartState = -1;
        IMissionsBL missionBL;
        IMissionsInteractionsBL missionsInteractionsBL;

        public CriticalPath()
        {
            missionBL = new MissionsBL();
            missionsInteractionsBL = new MissionsInteractionsBL();
        }

        private Activity[] Convert(List<Mission> list)
        {
            Activity[] activityArr = new Activity[list.Count+2];
            Dictionary<String, List<Mission>> missionInteractions = new Dictionary<string, List<Mission>>();
            List<Mission> prevMissions;
            List<Mission> nextMissions;

            List<int> startActivityIndexes = new List<int>();
            List<int> endActivityIndexes = new List<int>();


            int i = 0,index;

            activityArr[list.Count + 1] = new Activity(); //the final mission
            activityArr[list.Count + 1].Duration = 1;
            activityArr[list.Count + 1].Id = "-1";

            activityArr[list.Count ] = new Activity(); //the start mission
            activityArr[list.Count ].Duration = 1;
            activityArr[list.Count ].Id = "-2";


            foreach (var mission in list)
            {
                Activity temp = new Activity();
                temp.Duration = mission.Duration;
                temp.Description = mission.Description;
                temp.Id = mission.Id.ToString();
                
                prevMissions = missionsInteractionsBL.GetPreviousMissions(mission);
                missionInteractions.Add(temp.Id, prevMissions);
                if(prevMissions.Count == 0)//connect the start missions to the global start mission
                {
                    startActivityIndexes.Add(i);
                }

                nextMissions = missionsInteractionsBL.GetNextMissions(mission);
                if (nextMissions.Count == 0)//connect the end missions to the global end mission
                {
                    endActivityIndexes.Add(i);
                }
                activityArr[i] = temp;
                i++;
            }
            activityArr[list.Count].Predecessors = new Activity[0];
            foreach (int startIndex in startActivityIndexes)
            {
                activityArr[startIndex].Predecessors = new Activity[1];
                activityArr[startIndex].Predecessors[0] = activityArr[list.Count];
                activityArr[list.Count] = activityArr[list.Count].SetSuccessors(activityArr[list.Count],activityArr[startIndex]);
            }

            activityArr[list.Count + 1].Predecessors = new Activity[endActivityIndexes.Count];
            i = 0;
            foreach (int endIndex in endActivityIndexes)
            {
                activityArr[list.Count + 1].Predecessors[i] = activityArr[endIndex];
                i++;
                activityArr[endIndex] = activityArr[endIndex].SetSuccessors(activityArr[endIndex], activityArr[list.Count + 1]);
            }

            foreach (var mission in list)
            {
                List<Mission> Predecessors = missionInteractions[mission.Id.ToString()];
                //missionsInteractionsBL.GetPreviousMissions(mission);
                int activityIndex = GetIndexWhereIdEquals(activityArr, mission.Id.ToString());
                if (!startActivityIndexes.Contains(activityIndex))
                {
                    activityArr[activityIndex].Predecessors = new Activity[Predecessors.Count];
                    for (i = 0; i < Predecessors.Count; i++)
                    {
                        index = GetIndexWhereIdEquals(activityArr, Predecessors[i].Id.ToString());
                        activityArr[activityIndex].Predecessors[i] = activityArr[index];
                        activityArr[index] = activityArr[index].SetSuccessors(activityArr[index], activityArr[activityIndex]);
                    }
                }
            }

            Activity first = activityArr[list.Count];
            activityArr[list.Count] = activityArr[0];
            activityArr[0] = first;


            return activityArr;
        }

        private int GetIndexWhereIdEquals(Activity[] activityArr,String id)
        {
            for (int i = 0; i < activityArr.Length; i++)
            {
                if (activityArr[i].Id.Equals(id))
                    return i;
            }
            return -1;
        }
        
        private static Activity[] WalkListAhead(Activity[] list)
        {
            list[0].Eet = list[0].Est + list[0].Duration;
            Queue<Activity> next = new Queue<Activity>();
            next.Enqueue(list[0]);
            while(next.Count != 0)
            {
                Activity current = next.Dequeue();
                if (current.Successors != null)
                {
                    foreach (var nextActivity in current.Successors)
                    {
                        if (nextActivity.Est < current.Eet)
                        {
                            nextActivity.Est = current.Eet;
                            nextActivity.Eet = nextActivity.Duration + nextActivity.Est;
                            next.Enqueue(nextActivity);
                        }
                    }
                }               
            }           

            return list;
        }       
       

        public int CalcLongestPath(int projectId)
        {
            List<Mission> missionsList = missionBL.GetAllMissions(new Project() {Code = projectId});
            Activity[] activities = Convert(missionsList);            

            activities = WalkListAhead(activities);           
            String msg = "";
            foreach (Activity activity in activities)
            {
                if ((activity.Eet - activity.Let == 0) && (activity.Est - activity.Lst == 0))
                    msg +=("{0} ", activity.Id);
            }

            return activities[activities.Length - 1].Eet-2;          
        }       
    }
}
