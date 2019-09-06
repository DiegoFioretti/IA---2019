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

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private NodeClass parent;
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

    public void setSearchRange(float range)
    {
        minX = posMod.x - range;
        maxX = posMod.x + range;
        minY = posMod.y - range;
        maxY = posMod.y + range;
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

    public void SearchAdyacent(float distance, List<NodeClass> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].posMod.x < maxX && nodes[i].posMod.x > minX && nodes[i].posMod.y < maxY && nodes[i].posMod.y > minY)
            {
                adyacentNodes.Add(nodes[i]);
            }
        }
    }

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