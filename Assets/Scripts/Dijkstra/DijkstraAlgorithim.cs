using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DijkstraAlgorithim
{
    
    public static Stack<int> Dijkstra(float[][] graph, int start, int target)
    {
        //Setting up things required to do the algorithim
        Queue<int> nodesToProcess = new Queue<int>();
        int[] fromNode = new int[graph.Length];
        float[] shortestDistanceFromStart = new float[graph.Length];
        int current;
        
        nodesToProcess.Enqueue(start);
        for (int i = 0; i < graph.Length; i++)
        {
            shortestDistanceFromStart[i] = int.MaxValue;
            fromNode[i] = -1;
            if(i != start)
                nodesToProcess.Enqueue(i);
        }
        shortestDistanceFromStart[start] = 0;

        //Filling in the shortest distances from start and from nodes i.e processing each node
        while (nodesToProcess.Count > 0)
        {
            float temp;
            current = nodesToProcess.Peek();
            nodesToProcess.Dequeue();

            if(current == target && nodesToProcess.Count > 1)
            {
                nodesToProcess.Enqueue(current);
                current = nodesToProcess.Peek();
                nodesToProcess.Dequeue();
            }

            for (int i = 0; i < graph.Length; i++)
            {
                if(nodesToProcess.Contains(i))
                {
                    if(graph[current][i] >= 0)
                    {
                        temp = shortestDistanceFromStart[current] + graph[current][i];
                        if(temp < shortestDistanceFromStart[i])
                        {
                            shortestDistanceFromStart[i] = temp;
                            fromNode[i] = current;
                        }
                    }
                }
            }
        }

        //Finding the finalPath
        Stack<int> path = new Stack<int>();
        current = target;
        if(fromNode[current] >= 0 || current == start)
        {
            while (current >= 0)
            {
                path.Push(current);
                current = fromNode[current];
            }
        }

        return path;
    }
}
