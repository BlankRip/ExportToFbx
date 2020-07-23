using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public static Stack<int> AStarPath(float[][] graph, Node_AStar start, Node_AStar target)
    {
        //Setting up things required to do the algorithim
        List<Node_AStar> nodesToProcess = new List<Node_AStar>();
        bool[] visited = new bool[graph.Length];
        int[] fromNode = new int[graph.Length];
        float[] gCost = new float[graph.Length];
        float[] fCost = new float[graph.Length];
        bool createPath = false;
        
        nodesToProcess.Add(start);
        for (int i = 0; i < graph.Length; i++)
        {
            visited[i] = false;
            gCost[i] = int.MaxValue;
            fCost[i] = int.MaxValue;
            fromNode[i] = -1;
        }
        gCost[start.myIndex] = 0;
        start.myHCost = Vector3.Distance(start.transform.position, target.transform.position);
        fCost[start.myIndex] = start.myHCost;

        //Filling in the shortest distances from start and from nodes i.e processing each node
        while (nodesToProcess.Count > 0)
        {
            float least = float.MaxValue;
            Node_AStar current = null;
            foreach(Node_AStar node in nodesToProcess)
            {
                if(fCost[node.myIndex] < least)
                {
                    current = node;
                    least = fCost[node.myIndex];
                }
            }

            if(current == target)
            {
                createPath = true;
                break;
            }

            nodesToProcess.Remove(current);

            for (int i = 0; i < current.connections.Count; i++)
            {
                if(!visited[current.connections[i].myIndex])
                {
                    float temp;
                    temp = gCost[current.myIndex] + graph[current.myIndex][current.connections[i].myIndex];
                    if(temp < gCost[current.connections[i].myIndex])
                    {
                        gCost[current.connections[i].myIndex] = temp;
                        fromNode[current.connections[i].myIndex] = current.myIndex;

                        current.connections[i].myHCost = Vector3.Distance(current.connections[i].transform.position, target.transform.position);
                        fCost[current.connections[i].myIndex] = gCost[current.connections[i].myIndex] + current.connections[i].myHCost;

                        if(!nodesToProcess.Contains(current.connections[i]))
                            nodesToProcess.Add(current.connections[i]);
                    }
                }
            }
            visited[current.myIndex] = true;
        }

        if(createPath)
        {
            //Finding the finalPath
            Stack<int> path = new Stack<int>();
            int currentIndex = target.myIndex;
            if(fromNode[currentIndex] >= 0 || currentIndex == start.myIndex)
            {
                while (currentIndex >= 0)
                {
                    path.Push(currentIndex);
                    currentIndex = fromNode[currentIndex];
                }
            }

            return path;
        }
        else
        {
            Debug.Log("<color=red>PATH COULD NOT BE FOUND </color>");
            return null;
        }
    }
}
