using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MazePuzzleManager : MonoBehaviour
{
    [SerializeField] private int numberOfPiecesToAttach;
    private List<GameObject> currentlyAttachedPieces = new List<GameObject>();

    public void AttachPiece(GameObject obj)
    {
        currentlyAttachedPieces.Add(obj);
        CheckForPuzzleCompletion();
    }

    private void CheckForPuzzleCompletion()
    {
        if (currentlyAttachedPieces.Count >= numberOfPiecesToAttach)
        {
            // TODO Play ball spawn sound
            Invoke(nameof(DeleteObjectComponent), 1f);
            Debug.Log("Puzzle Completed");
        };
    }
    
    public void RemovePiece(GameObject obj)
    {
        currentlyAttachedPieces.Remove(obj);
    }

    public void DeleteObjectComponent()
    {
        foreach (GameObject puzzlePiece in currentlyAttachedPieces.ToArray())
        {
            puzzlePiece.GetComponent<XRGrabInteractable>().enabled = false;
        }
        // TODO spawn ball inside maze
    }
}
