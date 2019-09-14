using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    //[SerializeField] private GameObject nodePrefab;

    [SerializeField] private Vector3 gridDimesion;
    [SerializeField] private float nodeGap;
    [SerializeField] private Vector3 nodeSize;
    [SerializeField] private float searchDistance;
    
    int xwidth = 0;
    int yheight = 0;
//List<NodeClass> nodes;

[SerializeField] private NodeClass[,] nodes;
    //public GameObject[] nodesPosition;

    void Awake()
    {
        //maxNodes = (int)(gridDimesion.z * gridDimesion.x);
        xwidth = (int)gridDimesion.x / ((int)nodeSize.x + (int)nodeGap);
        yheight = (int)gridDimesion.z / ((int)nodeSize.z + (int)nodeGap);
        nodes = new NodeClass[xwidth,yheight];
        Vector3 auxPosition = new Vector3(0,0,0);
        auxPosition.y = transform.position.y;
        for (int i = 0; i < xwidth; i++)
        {
            auxPosition.x = transform.position.x - (gridDimesion.x / 2) + ((nodeSize.x + nodeGap) * i);
            for (int j = 0; j < yheight; j++)
            {
                auxPosition.z = transform.position.z - (gridDimesion.z / 2) + ((nodeSize.z + nodeGap) * j);
                nodes[i,j] = new NodeClass();
                nodes[i,j].posMod = auxPosition;
                nodes[i,j].gridPosX = i;
                nodes[i,j].gridPosY = j;
                //nodes[i,j].Obstructed = false;
                //nodes[i,j].Value = 1;
                nodes[i,j].sizeMod = nodeSize;
            }
        }
    }

    public NodeClass[,] GetNodes
    {
        get { return nodes; }
    }

    public int getXWidth
    {
        get { return xwidth; }
    }

    public int getYHeight
    {
        get { return yheight; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, gridDimesion);

        if (nodes != null)
        {
            foreach (NodeClass n in nodes)
            {
                //if (n.Obstructed)
                //    Gizmos.color = Color.red;
                //else
                    Gizmos.color = Color.grey;
                Gizmos.DrawCube(n.posMod, n.sizeMod);
            }
        }
    }
}