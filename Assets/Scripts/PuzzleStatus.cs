using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleStatus : MonoBehaviour
{
    [System.Serializable]
    public class MyBoolEvent : UnityEvent<bool>
    {
    }

    [SerializeField] private UnityEvent<bool> _event = new MyBoolEvent();
    
    [SerializeField] private bool _isSolved;

    public bool Solved
    {
        get 
        {
            return _isSolved;
        }
        set
        {
            if (value != _isSolved)
            {
                _isSolved = value;
                UpdatePuzzleManager();
            }
        }
    }
    public void UpdatePuzzleManager()
    {
        if (_event != null)
        {
            _event.Invoke(_isSolved);
        }
    }

    public void AddListener(PuzzleManager puzzleManager)
    {
        _event.AddListener(puzzleManager.CountSolvedPuzzles);
    }

}
