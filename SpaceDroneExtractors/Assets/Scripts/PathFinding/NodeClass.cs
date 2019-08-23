using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeClass : MonoBehaviour
{

    [SerializeField] private LayerMask terrainMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask layerMask;

    public List<NodeClass> adyacentNodes;
    public bool isOpen = false;

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

    public bool Obstructed
    {
        set
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 3, 0), transform.TransformDirection(Vector3.down), out hit, 3, obstacleMask))
            {
                isObstructed = true;
            }
            else
            {
                isObstructed = false;
            }
        }
        get { return isObstructed; }
    }

    public int Value
    {
        set
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 3, 0), transform.TransformDirection(Vector3.down), out hit, 3, terrainMask))
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
        }
        get { return value; }
    }

    public void SearchAdyacent(float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<NodeClass>().Obstructed == false)
            {
                adyacentNodes.Add(hit.collider.gameObject.GetComponent<NodeClass>());
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<NodeClass>().Obstructed == false)
            {
                adyacentNodes.Add(hit.collider.gameObject.GetComponent<NodeClass>());
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<NodeClass>().Obstructed == false)
            {
                adyacentNodes.Add(hit.collider.gameObject.GetComponent<NodeClass>());
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<NodeClass>().Obstructed == false)
            {
                adyacentNodes.Add(hit.collider.gameObject.GetComponent<NodeClass>());
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + Vector3.left), out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<NodeClass>().Obstructed == false)
            {
                adyacentNodes.Add(hit.collider.gameObject.GetComponent<NodeClass>());
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + Vector3.right), out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<NodeClass>().Obstructed == false)
            {
                adyacentNodes.Add(hit.collider.gameObject.GetComponent<NodeClass>());
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back + Vector3.left), out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<NodeClass>().Obstructed == false)
            {
                adyacentNodes.Add(hit.collider.gameObject.GetComponent<NodeClass>());
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back + Vector3.left), out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<NodeClass>().Obstructed == false)
            {
                adyacentNodes.Add(hit.collider.gameObject.GetComponent<NodeClass>());
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (isObstructed)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
        }
        else
        {
            Gizmos.color = new Color(0, 0, 0, 0.5f);
        }
        Gizmos.DrawCube(transform.position, nodeSize);
    }
}