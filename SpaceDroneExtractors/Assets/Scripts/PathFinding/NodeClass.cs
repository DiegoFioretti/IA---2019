using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeClass
{

    [SerializeField] private LayerMask terrainMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask layerMask;

    private List<NodeClass> adyacentNodes;
    private bool isOpen = false;
    /*
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;*/

    private int _gridPosX;
    private int _gridPosY;
    private NodeClass parent;
    private NodeClass auxNode;
    private Vector3 nodeSize;
    private Vector3 position;
    private bool isObstructed = false;
    private int value;

    private void Awake()
    {
        adyacentNodes = new List<NodeClass>();
    }

    public Vector3 posMod
    {
        set { position = value; }
        get { return position; }
    }

    public int gridPosX
    {
        set { _gridPosX = value; }
        get { return _gridPosX; }
    }

    public int gridPosY
    {
        set { _gridPosY = value; }
        get { return _gridPosY; }
    }

    public Vector3 sizeMod
    {
        set { nodeSize = value; }
        get { return nodeSize; }
    }

    public NodeClass Parent
    {
        set { parent = value; }
        get { return parent; }
    }

    public bool Obstructed
    {
        /*set
        {
            RaycastHit hit;
            if (Physics.Raycast(posMod + new Vector3(0, 3, 0), transform.TransformDirection(Vector3.down), out hit, 3, obstacleMask))
            {
                isObstructed = true;
            }
            else
            {
                isObstructed = false;
            }
        }*/
        get { return isObstructed; }
    }

    public int Value
    {
        /*set
        {
            RaycastHit hit;
            if (Physics.Raycast(posMod + new Vector3(0, 3, 0), transform.TransformDirection(Vector3.down), out hit, 3, terrainMask))
            {
                if (hit.collider.tag == "TerrainForest")
                {
                    value = 2;
                }
                else
                {
                    value = 1;
                }
            }
        }*/
        get { return value; }
    }
    /*
    public void SearchAdyacent(List<NodeClass> nodes)
    {
        if (nodes != null || nodes.Count > 0)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].posMod.x < maxX && nodes[i].posMod.x > minX && nodes[i].posMod.z < maxZ && nodes[i].posMod.z > minZ && nodes[i] != this)
                {
                    auxNode = nodes[i];
                    adyacentNodes.Add(auxNode);
                }
            }
        }
    }*/

    public bool isOpenMod
    {
        set { isOpen = value; }
        get { return isOpen; }
    }

    public List<NodeClass> adyacentNodesMod
    {
        get { return adyacentNodes; }
    }
}