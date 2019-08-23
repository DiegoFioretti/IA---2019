using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    [SerializeField] private GameObject nodeGrid;
    [SerializeField] private float nodeSearchDistance;
    //[SerializeField] private GameObject originNode;
    //[SerializeField] private GameObject objectiveNode;


    List<NodeClass> existingNodes;
    List<NodeClass> openNodes;
    List<NodeClass> closedNodes;

    void Start()
    {

        existingNodes = new List<NodeClass>();
        openNodes = new List<NodeClass>();
        closedNodes = new List<NodeClass>();
        NodeGrid grid = nodeGrid.GetComponent<NodeGrid>();

        NodeClass auxNode;
        for (int i = 0; i < grid.GetNodeCant; i++)
        {
            auxNode = grid.GetNodes[i].GetComponent<NodeClass>();
            existingNodes.Add(auxNode);
        }

        for (int i = 0; i < existingNodes.Count; i++)
        {
            existingNodes[i].SearchAdyacent(nodeSearchDistance);
        }

    }

    private enum Algoritmos { BreathFirst = 0, DepthFirst = 1, Dijkstra = 2, Estrella = 3 }
    [SerializeField] private Algoritmos algo = Algoritmos.BreathFirst;

    public List<NodeClass> ChartRoute(Vector3 orig, Vector3 dest)
    {
        float nodoDest = (existingNodes[0].gameObject.transform.position - dest).magnitude;
        float nodoOrig = (existingNodes[0].gameObject.transform.position - orig).magnitude;
        int indexD = 0;
        int indexO = 0;
        for (int i = 0; i < existingNodes.Count; i++)
        {
            if ((existingNodes[i].gameObject.transform.position - dest).magnitude < nodoDest)
            {
                nodoDest = (existingNodes[i].gameObject.transform.position - dest).magnitude;
                indexD = i;
            }
            if ((existingNodes[i].gameObject.transform.position - orig).magnitude < nodoOrig)
            {
                nodoOrig = (existingNodes[i].gameObject.transform.position - orig).magnitude;
                indexO = i;
            }
        }
        NodeClass origin = existingNodes[indexO];
        NodeClass objective = existingNodes[indexD];
        openNodes.Add(origin);
        origin.isOpen = true;
        origin.Parent = null;

        while (openNodes.Count > 0)
        {
            NodeClass nodo = SelectNodes();
            if (nodo == objective)
            {
                CleanUp();
                return ChartList(nodo);
            }
            closedNodes.Add(nodo);
            openNodes.Remove(nodo);
            for (int i = 0; i < nodo.adyacentNodes.Count; i++)
            {
                if (!nodo.adyacentNodes[i].isOpen)
                {
                    openNodes.Add(nodo.adyacentNodes[i]);
                    nodo.adyacentNodes[i].Parent = nodo;
                    nodo.adyacentNodes[i].isOpen = true;
                }
            }
        }
        CleanUp();
        return null;
    }

    public List<NodeClass> ChartList(NodeClass destino)
    {
        List<NodeClass> nodePath = new List<NodeClass>();
        NodeClass parentAux = destino;
        while (parentAux != null)
        {
            nodePath.Add(parentAux);
            parentAux = parentAux.Parent;
        }
        return nodePath;
    }

    private void CleanUp()
    {
        for (int i = 0; i < existingNodes.Count; i++)
        {
            existingNodes[i].isOpen = false;
        }
        openNodes.Clear();
        closedNodes.Clear();
    }

    NodeClass SelectNodes()
    {
        if (algo == Algoritmos.breathfirst)
        {
            return openNodes[0];
        }
        else if (algo == Algoritmos.depthfirst)
        {
            return openNodes[openNodes.Count - 1];
        }
        else
        {
            return openNodes[0];
        }
    }
}