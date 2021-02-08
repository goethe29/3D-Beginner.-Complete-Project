using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<PuzzleStatus> _puzzles;
    [SerializeField] private int _solvedPuzzleCount = 0;
    private bool _isSolved = false;
    [SerializeField] private int _puzzleCount = 0;
    [SerializeField] UnityEvent _response;
    
    public void CountSolvedPuzzles(bool solved)
    {
        if(solved)
        {
            _solvedPuzzleCount++;
        } else 
        {
            _solvedPuzzleCount--;
        }
        SolvedOrNot();
    }

    public void SolvedOrNot()
    {
        bool solved = _solvedPuzzleCount == _puzzleCount;
        if (_isSolved != solved)
        {
            _isSolved = solved;
            _response.Invoke();
        }
    }

    private void Start() 
    {
        _puzzleCount = _puzzles.Count;

        foreach (var puzzle in _puzzles)
        {
            puzzle.AddListener(this);
        }
    }
}
