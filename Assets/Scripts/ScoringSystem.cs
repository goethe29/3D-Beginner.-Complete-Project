using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    private int _killsCount = 0;

    public int KillsCount
    {
        get
        {
            return _killsCount;
        }
    }

    public void AddKill() 
    {
        _killsCount +=1;
    }
}
