﻿using ProjectorLogic.Business_Logic.Logic;
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
            //indexToId[0] = 23;
            //indexToId[1] = 24;
            //indexToId[2] = 25;
            //indexToId[3] = 26;
            //indexToId[4] = 27;
            //indexToId[5] = 28;
            //indexToId[6] = 29;
            //indexToId[7] = 30;
            //indexToId[8] = -1;//s
            //indexToId[9] = -2;//e

            //duration[0] = 6;
            //duration[1] = 8;
            //duration[2] = 4;
            //duration[3] = 2;
            //duration[4] = 7;
            //duration[5] = 4;
            //duration[6] = 3;
            //duration[7] = 2;
            //duration[8] = 0;//s
            //duration[9] = 1;//e      

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
            //interactions.Add(27, new List<int>() { 28 });
            //interactions.Add(26, new List<int>() { 24 });
            //interactions.Add(24, new List<int>() { 28 });
            //interactions.Add(28, new List<int>() { 25 });
            //interactions.Add(25, new List<int>() { 29, 30 }); //from db

            //interactions.Add(-1, new List<int>() { 23 });
            //interactions.Add(29, new List<int>() { -2 }); //add your self
            //interactions.Add(30, new List<int>() { -2 });


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

            return -dijkstra(graph, findInArray(StartState, indexToId), findInArray(EndState, indexToId), countMissions+2);
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