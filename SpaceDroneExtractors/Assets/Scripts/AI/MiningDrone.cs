using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiningDrone : MonoBehaviour
{
    /*
        States          Events
        0 Idle          0 MineExist
        1 GoToMine      1 Excavate
        2 GoToBase      2 MaxCarry
        3 Mine          3 CheckIn
        4 Deposit       4 NoMine
    */

    FSM fsm;

    private const int stateCount = 5;
    private const int eventCount = 5;
    private int currentState = 0;
    private RaycastHit ray;

    private NavMeshAgent nma;
    private GoldCloud _mineCheck;
    private GameObject _mine;
    private GameObject _base;

    [SerializeField] private int maxGold = 0;
    [SerializeField] private int currentGold = 0;
    [SerializeField] private bool doesMineExist = false;
    [SerializeField] private bool atMine = false;
    [SerializeField] private bool atBase = false;

    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();

        fsm = new FSM(stateCount, eventCount);
        /*
        States          Events
        0 Idle          0 MineExist
        1 GoToMine      1 Excavate
        2 GoToBase      2 MaxCarry
        3 Mine          3 CheckIn
        4 Deposit       4 NoMine
        */
        // 1 - Que Estado esta 2 - Que evento recibe 3 - A que estado cambia
        fsm.SetRelation(0, 0, 1);
        fsm.SetRelation(1, 1, 3);
        fsm.SetRelation(1, 4, 0);
        fsm.SetRelation(2, 3, 4);
        fsm.SetRelation(3, 2, 2);
        fsm.SetRelation(3, 4, 2);
        fsm.SetRelation(4, 0, 1);
        fsm.SetRelation(4, 4, 0);
        _base = GameObject.FindGameObjectWithTag("Base");
    }

    void Update()
    {
        //Check if mine exist
        if (!doesMineExist)
        {
            if (_mine = GameObject.FindGameObjectWithTag("GoldCloud"))
            {
                doesMineExist = true;
                _mineCheck = _mine.GetComponent<GoldCloud>();
            }
        }

        if (doesMineExist && currentGold == 0)
            fsm.SetEvent(0);
        if (!doesMineExist)
            fsm.SetEvent(4);
        if (atMine)
            fsm.SetEvent(1);
        if (atBase)
            fsm.SetEvent(3);
        if (currentGold >= maxGold || (currentGold > 0 && _mine.tag == "EmptyCloud"))
            fsm.SetEvent(2);

        currentState = fsm.GetState();

        switch (fsm.GetState())
        {
            case 0:
                break;
            case 1:
                //Move to mine
                nma.destination = _mine.transform.position;
                break;
            case 2:
                //Move to base
                nma.destination = _base.transform.position;
                break;
            case 3:
                //Get gold
                _mineCheck.goldAction--;
                currentGold++;
                break;
            case 4:
                //Deposit gold
                currentGold--;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Base" && atBase != true)
            atBase = true;
        if (other.tag == "GoldCloud" && atMine != true)
            atMine = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Base" && atBase)
            atBase = false;
        if (other.tag == "GoldCloud" && atMine || other.tag == "EmptyCloud" && atMine)
            atMine = false;
    }
}