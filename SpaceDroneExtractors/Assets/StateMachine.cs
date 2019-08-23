using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    int[,] fsm;
    int _state;
    int _stateCount;
    int _eventCount;

    /*
        States          Events
        0 Idle          0 MineExist
        1 GoToMine      1 Excavate
        2 GoToBase      2 MaxCarry
        3 Mine          3 CheckIn
        4 Deposit       4 NoMine
    */

    public FSM(int statesCount, int eventsCount)
    {
        fsm = new int[statesCount, eventsCount];
        for (int i = 0; i < statesCount; i++)
        {
            for (int j = 0; j < eventsCount; j++)
            {
                fsm[i, j] = -1;
            }
        }
        _stateCount = statesCount;
        _eventCount = eventsCount;
    }

    public void SetRelation(int srcState, int evt, int dirState)
    {
        if (srcState < _stateCount && srcState >= 0 && evt >= 0 && evt < _eventCount && dirState >= 0)
            fsm[srcState, evt] = dirState;
    }
    public int GetState()
    {
        return _state;
    }
    public void SetEvent(int evt)
    {
        if (fsm[_state, evt] != -1)
        {
            _state = fsm[_state, evt];
        }
    }
}