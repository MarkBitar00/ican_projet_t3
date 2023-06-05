using UnityEngine;
using UnityEngine.Events;

public class MazePuzzleManager : MonoBehaviour
{
    [SerializeField] private int numberOfPiecesToAttach;
    [SerializeField] private int currentlyAttachedPieces = 0;

    [Header("Completion Events")] public UnityEvent onPuzzleCompletion;

    public void AttachPiece()
    {
        currentlyAttachedPieces++;
        CheckForPuzzleCompletion();
    }

    private void CheckForPuzzleCompletion()
    {
        if (currentlyAttachedPieces < numberOfPiecesToAttach) return;
        Debug.Log("Puzzle completed !");
        onPuzzleCompletion.Invoke();
    }

    public void RemovePiece()
    {
        currentlyAttachedPieces--;
    }
}
