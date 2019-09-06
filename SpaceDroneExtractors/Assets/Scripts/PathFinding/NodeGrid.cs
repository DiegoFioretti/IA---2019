﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    //[SerializeField] private GameObject nodePrefab;

    [SerializeField] private Vector3 gridDimesion;
    [SerializeField] private float nodeGap;
    [SerializeField] private Vector3 nodeSize;
    [SerializeField] private float searchDistance;

    int cantNodes = 0;
    int maxNodes = 0;
    //List<NodeClass> nodes;

    [SerializeField] private NodeClass[] nodes;
    //public GameObject[] nodesPosition;

    void Awake()
    {
        maxNodes = (int)(gridDimesion.z * gridDimesion.x);
        nodes = new NodeClass[1000];
        for (float i = (transform.position.z + (gridDimesion.z / 2) - (nodeSize.z / 2)); i > (transform.position.z - (gridDimesion.z / 2)); i = (i - nodeGap))
        {
            for (float j = (transform.position.x - (gridDimesion.x / 2) + (nodeSize.x / 2)); j < (transform.position.x + (gridDimesion.x / 2)); j = (j + nodeGap))
            {
                nodes[cantNodes] = new NodeClass();
                nodes[cantNodes].posMod = new Vector3(j, transform.position.y, i);
                //nodes[cantNodes].Obstructed = true;
                //nodes[cantNodes].Value = -1;
                nodes[cantNodes].sizeMod = nodeSize;
                cantNodes++;
            }
        }
    }

    public int GetNodeCant
    {
        get { return cantNodes; }
    }

    public NodeClass[] GetNodes
    {
        get { return nodes; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, gridDimesion);

        if (nodes != null)
        {
            foreach (NodeClass n in nodes)
            {
                if (n.Obstructed)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.grey;
                Gizmos.DrawCube(n.posMod, n.sizeMod);
            }
        }
    }
}