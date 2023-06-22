using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static ConnectionsV2;

public class BFSalgorithm : MonoBehaviour
{
    public bool visualize;
    public float waitTime;


    public List<ConnectionsV2.References> nodes = new List<ConnectionsV2.References>();

    //stores all the not visisted nodes
    public List<ConnectionsV2.References> notVisitedNodes = new List<ConnectionsV2.References>();

    //stores the nodes we visisted untill we go to the next group
    public List<ConnectionsV2.References> visistedNodes = new List<ConnectionsV2.References>();

    //stopres the nodes we need to visit
    public List<ConnectionsV2.References> adjacentNodes = new List<ConnectionsV2.References>();

    //stores the list of nodes groups
    public List<NodesNetworks> connectedNodes = new List<NodesNetworks>();

    //stores the list of nodes in the order they were discorvered
    public List<ConnectionsV2.References> nodeOrder = new List<ConnectionsV2.References>();

    [Serializable]
    public class NodesNetworks
    {
        public List<ConnectionsV2.References> network;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadCables();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(Visualize());
        }
    }

    //starting function of them all, it will load in all the non empty nodes and craete a list of them. 
    void LoadCables()
    {
        nodes = new List<ConnectionsV2.References>();
        notVisitedNodes = new List<ConnectionsV2.References>();
        connectedNodes = new List<NodesNetworks>();
        nodeOrder = new List<ConnectionsV2.References>();   

        foreach (Transform structure in GameObject.Find("Structures").transform)
        {
            ConnectionsV2 connections = structure.GetComponent<ConnectionsV2>();

            foreach (KeyValuePair<string, References> entry in connections.references)
            {
                if(entry.Value.structures.Count > 0)
                {
                    nodes.Add(entry.Value);
                    notVisitedNodes.Add(entry.Value);
                }
            }
        }

        Initialize();
    }

    void Initialize()
    {
        foreach(ConnectionsV2.References node in nodes)
        {
            if (notVisitedNodes.Contains(node))
            {
                visistedNodes = new List<ConnectionsV2.References>();
                adjacentNodes = new List<ConnectionsV2.References>();

                //after we reset everything we run DFS

                DFS(node);


                NodesNetworks nodesNetworks = new NodesNetworks();
                nodesNetworks.network = visistedNodes;


                connectedNodes.Add(nodesNetworks);
            }
        }

        if (visualize)
        {
            StartCoroutine(Visualize());
        }
    }

    //this is the main function, first it will remove the node from the not visisted and adjecents nodes and it will be added to the visiste list
    //It then adds all the adjecents node in the adjecents node list, then checks if the list is not empty and if is not it runs the code again
    void DFS(ConnectionsV2.References node)
    {
        notVisitedNodes.Remove(node);
        adjacentNodes.Remove(node);
        visistedNodes.Add(node);
        

        AdjecentsNodeAdder(node);


        if (visualize)
        {
            nodeOrder.Add(node);
        }


        if (adjacentNodes.Count != 0)
        {
            DFS(adjacentNodes[0]);
        }
    }
    
    //this function will add all the nodes that the give node is connected to to the adjecents node list, they will be removed from notVisitedNodes
    void AdjecentsNodeAdder(ConnectionsV2.References node)
    {
        for(int i=0;i<node.structures.Count;i++)
        {
            int nodeIndex = FindNode(notVisitedNodes, node.structures[i], node.connectedTo[i], node.color);
            if(nodeIndex != -1)
            {
                adjacentNodes.Add(notVisitedNodes[nodeIndex]);
                notVisitedNodes.RemoveAt(nodeIndex);
            }
        }
    }

    //will return the index of a specified node in a specified lsit
    int FindNode(List<ConnectionsV2.References> list ,GameObject structure, int connectionPoint, string color)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].originStructure == structure && list[i].color == color && list[i].number == connectionPoint)
            {
                return i;
            }
        }

        //if the connection does not exist the return value will be -1
        return -1;
    }

    IEnumerator Visualize()
    {

        for (int i = 0; i < nodeOrder.Count; i++)
        {
            nodeOrder[i].originStructure.GetComponent<SpriteRenderer>().color = Color.white;
        }

        Debug.Log("Visualization intiated");
        for(int i=0; i< nodeOrder.Count; i++)
        {
            yield return new WaitForSeconds(waitTime);
            nodeOrder[i].originStructure.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
