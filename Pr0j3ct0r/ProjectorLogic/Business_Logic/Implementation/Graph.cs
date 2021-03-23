using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Business_Logic.Implementation
{
    class Graph
    {
        public int vertices;
        public List<int>[] edge;

        public Graph(int vertices)
        {
            this.vertices = vertices;
            edge = new List<int>[vertices + 1];
            for (int i = 0; i <= vertices; i++)
            {
                edge[i] = new List<int>();
            }
        }
        public void addEdge(int a, int b)
        {
            edge[a].Add(b);
        }

        public void dfs(int node, List<int>[] adj,
                        int[] dp, Boolean[] visited)
        {
            // Mark as visited 
            visited[node] = true;

            // Traverse for all its children 
            for (int i = 0; i < adj[node].Count; i++)
            {

                // If not visited 
                if (!visited[adj[node][i]])
                    dfs(adj[node][i], adj, dp, visited);

                // Store the max of the paths 
                dp[node] = Math.Max(dp[node], 1 +
                                    dp[adj[node][i]]);
            }
        }

        // Function that returns the longest path 
        public int findLongestPath(int n)
        {
            List<int>[] adj = edge;
            // Dp array 
            int[] dp = new int[n + 1];

            // Visited array to know if the node 
            // has been visited previously or not 
            Boolean[] visited = new Boolean[n + 1];

            // Call DFS for every unvisited vertex 
            for (int i = 1; i <= n; i++)
            {
                if (!visited[i])
                    dfs(i, adj, dp, visited);
            }

            int ans = 0;

            // Traverse and find the maximum of all dp[i] 
            for (int i = 1; i <= n; i++)
            {
                ans = Math.Max(ans, dp[i]);
            }
            return ans;
        }
    }
}
