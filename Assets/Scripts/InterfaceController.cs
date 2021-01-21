using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    [SerializeField] private Text _killsCount;

    public void UpdateDisplayUI(ScoringSystem scoringSystem)
    {
        _killsCount.text = scoringSystem.KillsCount.ToString();
    }
}
