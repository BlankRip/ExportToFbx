using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public static Stack<int> Dijkstra(float[][] graph, Node_AStar start, Node_AStar target)
    {
        //Setting up things required to do the algorithim
        List<Node_AStar> nodesToProcess = new List<Node_AStar>();
        bool[] visited = new bool[graph.Length];
        int[] fromNode = new int[graph.Length];
        float[] shortestDistanceFromStart = new float[graph.Length];
        float[] finalCost = new float[graph.Length];
        Node_AStar current = new Node_AStar();
        
        nodesToProcess.Add(start);
        for (int i = 0; i < graph.Length; i++)
        {
            visited[i] = false;
            shortestDistanceFromStart[i] = int.MaxValue;
            finalCost[i] = int.MaxValue;
            fromNode[i] = -1;
        }
        shortestDistanceFromStart[start.myIndex] = 0;
        if(start.myHCost < 0)
            start.myHCost = Vector3.Distance(start.transform.position, target.transform.position);
        finalCost[start.myIndex] = start.myHCost;

        //Filling in the shortest distances from start and from nodes i.e processing each node
        while (nodesToProcess.Count > 0)
        {
            float least = int.MaxValue;
            foreach(Node_AStar node in nodesToProcess)
            {
                if(finalCost[node.myIndex] < least)
                {
                    current = node;
                    if(node.myHCost < 0)
                        node.myHCost = Vector3.Distance(node.transform.position, target.transform.position);
                }
            }

            if(current == target)
                break;

            nodesToProcess.Remove(current);

            for (int i = 0; i < current.connections.Count; i++)
            {
                if(!visited[current.connections[i].myIndex])
                {
                    float temp;
                    if(graph[current.myIndex][current.connections[i].myIndex] >= 0)
                    {
                        temp = shortestDistanceFromStart[current.myIndex] + graph[current.myIndex][current.connections[i].myIndex];
                        if(temp < shortestDistanceFromStart[current.connections[i].myIndex])
                        {
                            shortestDistanceFromStart[current.connections[i].myIndex] = temp;
                            fromNode[current.connections[i].myIndex] = current.myIndex;

                            if(current.connections[i].myHCost < 0)
                                current.connections[i].myHCost = Vector3.Distance(current.connections[i].transform.position, target.transform.position);
                            finalCost[current.connections[i].myIndex] = shortestDistanceFromStart[current.connections[i].myIndex] + current.connections[i].myHCost;

                            if(!nodesToProcess.Contains(current.connections[i]))
                                nodesToProcess.Add(current.connections[i]);
                        }
                    }
                }
            }

            visited[current.myIndex] = true;
        }

        //Finding the finalPath
        Stack<int> path = new Stack<int>();
        int currentIndex = target.myIndex;
        if(fromNode[currentIndex] >= 0 || current == start)
        {
            while (currentIndex >= 0)
            {
                path.Push(currentIndex);
                currentIndex = fromNode[currentIndex];
            }
        }

        return path;
    }
}
