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
            auxNode = grid.GetNodes[i];
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
        float nodoDest = (existingNodes[0].posMod - dest).magnitude;
        float nodoOrig = (existingNodes[0].posMod - orig).magnitude;
        int indexD = 0;
        int indexO = 0;
        for (int i = 0; i < existingNodes.Count; i++)
        {
            if ((existingNodes[i].posMod - dest).magnitude < nodoDest)
            {
                nodoDest = (existingNodes[i].posMod - dest).magnitude;
                indexD = i;
            }
            if ((existingNodes[i].posMod - orig).magnitude < nodoOrig)
            {
                nodoOrig = (existingNodes[i].posMod - orig).magnitude;
                indexO = i;
            }
        }
        NodeClass origin = existingNodes[indexO];
        NodeClass objective = existingNodes[indexD];
        openNodes.Add(origin);
        origin.isOpenMod = true;
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
            for (int i = 0; i < nodo.adyacentNodesMod.Count; i++)
            {
                if (!nodo.adyacentNodesMod[i].isOpenMod)
                {
                    openNodes.Add(nodo.adyacentNodesMod[i]);
                    nodo.adyacentNodesMod[i].Parent = nodo;
                    nodo.adyacentNodesMod[i].isOpenMod = true;
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
            existingNodes[i].isOpenMod = false;
        }
        openNodes.Clear();
        closedNodes.Clear();
    }

    NodeClass SelectNodes()
    {
        if (algo == Algoritmos.BreathFirst)
        {
            return openNodes[0];
        }
        else if (algo == Algoritmos.DepthFirst)
        {
            return openNodes[openNodes.Count - 1];
        }
        else
        {
            return openNodes[0];
        }
    }
}