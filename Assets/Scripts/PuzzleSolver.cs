using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleSolver : MonoBehaviour
{
    [SerializeField] private UnityEvent Solve; // permet d'avoir la liste des callback à un event
    [SerializeField] private UnityEvent Reset; // permet d'avoir la liste des callback à un event

    public void SolvePuzzle()
    {
        Solve?.Invoke();
    }

    public void ResetPuzzle()
    {
        Reset?.Invoke();
    }
}
