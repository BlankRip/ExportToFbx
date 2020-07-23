using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public static Stack<int> AStarPath(float[][] graph, Node_AStar start, Node_AStar target)
    {
        //Setting up things required to do the algorithim
        List<Node_AStar> nodesToProcess = new List<Node_AStar>();
        bool createPath = false;
        
        nodesToProcess.Add(start);
        // foreach (Node_AStar node in allNodes)
        // {
        //     node.visited = false;
        //     node.gcost = float.MaxValue;
        //     node.fCost = float.MaxValue;
        //     node.fromNodeIndex = null;
        // }
        start.gcost = 0;
        start.hCost = Vector3.Distance(start.transform.position, target.transform.position);
        start.fCost = start.hCost;

        //Filling in the shortest distances from start and from nodes i.e processing each node
        while (nodesToProcess.Count > 0)
        {
            float least = float.MaxValue;
            Node_AStar current = null;
            foreach(Node_AStar node in nodesToProcess)
            {
                if(node.fCost < least)
                {
                    current = node;
                    least = node.fCost;
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
                if(!current.connections[i].visited)
                {
                    float temp;
                    temp = current.gcost + graph[current.myIndex][current.connections[i].myIndex];
                    if(temp < current.connections[i].gcost)
                    {
                        current.connections[i].gcost= temp;
                        current.connections[i].fromNodeIndex = current;

                        current.connections[i].hCost = Vector3.Distance(current.connections[i].transform.position, target.transform.position);
                        current.connections[i].fCost = current.connections[i].gcost + current.connections[i].hCost;

                        if(!nodesToProcess.Contains(current.connections[i]))
                            nodesToProcess.Add(current.connections[i]);
                    }
                }
            }
            current.visited = true;
        }

        if(createPath)
        {
            //Finding the finalPath
            Stack<int> path = new Stack<int>();
            Node_AStar currentNode = target;
            int currentIndex = currentNode.myIndex;
            if(currentIndex >= 0 || currentIndex == start.myIndex)
            {
                while (currentNode != null)
                {
                    currentIndex = currentNode.myIndex;
                    path.Push(currentIndex);
                    currentNode =  currentNode.fromNodeIndex;
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
