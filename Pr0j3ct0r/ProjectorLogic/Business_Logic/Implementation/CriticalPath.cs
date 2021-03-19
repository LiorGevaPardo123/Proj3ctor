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

        public int CalcLongestPath(int projectId)
        {
            List<Mission> missionsList = missionBL.GetAllMissions(new Project() {Code = projectId});
            int countMissions = missionsList.Count;
            int[] indexToId = new int[countMissions + 2];
              

            int[] duration = new int[countMissions + 2];

            for (int i = 0; i < countMissions; i++)
            {
                indexToId[i] = missionsList[i].Id;
                duration[i] = -missionsList[i].Duration;
            }

            indexToId[countMissions] = StartState;
            indexToId[countMissions+1] = EndState;

            duration[countMissions] = 0;
            duration[countMissions+1] = 1;                

            Dictionary<int, List<int>> interactions = new Dictionary<int, List<int>>();

            List<int> next;
            List<int> previous;
            List<int> starts = new List<int>();
            List<int> ends = new List<int>();

            foreach (var mission in missionsList)
            {
                next = missionsInteractionsBL.GetNextMissions(mission).Select(cur => cur.Id).ToList();
                previous = missionsInteractionsBL.GetPreviousMissions(mission).Select(cur => cur.Id).ToList();
                if(next.Count != 0)//אם יש לו רשימה של מישמות הבאות
                    interactions.Add(mission.Id, next);

                else//אם הוא משימה סופית
                {
                    ends.Add(mission.Id);
                    interactions.Add(mission.Id, new List<int>() { EndState});
                }
                if (previous.Count == 0)
                {
                    starts.Add(mission.Id);                    
                }
               
            }
            interactions.Add(StartState, new List<int>());
            for (int i = 0; i < starts.Count; i++)
            {
                interactions[StartState].Add(starts[i]);
            }          
            
            int[,] graph = new int[countMissions + 2, countMissions + 2];
            int toIndex;
            for (int i = 0; i < indexToId.Length; i++)
            {
                if (interactions.ContainsKey(indexToId[i]))
                {
                    foreach (var toId in interactions[indexToId[i]])
                    {
                        toIndex = findInArray(toId, indexToId);
                        graph[i, toIndex] = duration[toIndex];
                    }
                }
            }

            return -dijkstra(graph, findInArray(StartState, indexToId), findInArray(EndState, indexToId), countMissions+2)+1;
        }

        private static int findInArray(int toId, int[] indexToId)
        {
            for (int i = 0; i < indexToId.Length; i++)
            {
                if (indexToId[i] == toId)
                {
                    return i;
                }
            }
            return -99;
        }        
        
        private int minDistance(int[] dist, bool[] sptSet, int V)
        {
            // Initialize min value 
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }
       
        private int dijkstra(int[,] graph, int src, int dest, int V)
        {
            int[] dist = new int[V];             
            bool[] sptSet = new bool[V];             
            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }           
            
            dist[src] = 0;
            
            for (int count = 0; count < V - 1; count++)
            {                
                int u = minDistance(dist, sptSet, V);
                
                sptSet[u] = true;
                
                for (int v = 0; v < V; v++)                    
                    if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }

            return dist[dest];            
        }
    }
}
