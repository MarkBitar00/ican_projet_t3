using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MazePuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject mazeBall;
    [SerializeField] private GameObject mazeBase;
    [SerializeField] private int numberOfPiecesToAttach;
    
    private Vector3 mazeBallSpawnPosition;
    private List<GameObject> currentlyAttachedPieces = new List<GameObject>();

    private void Start()
    {
        if (mazeBall)
        {
            mazeBallSpawnPosition = mazeBall.transform.position;  
        }
    }

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
            if (mazeBase)
            {
                mazeBase.GetComponent<XRGrabInteractable>().enabled = true;
            }
            if (mazeBall)
            {
                SpawnMazeBall();
            }
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
            Destroy(puzzlePiece.GetComponent<XRGrabInteractable>());
            Destroy(puzzlePiece.GetComponent<BoxCollider>());
            Destroy(puzzlePiece.GetComponent<Rigidbody>());
        }
    }

    public void SpawnMazeBall()
    {
        mazeBall.SetActive(true);
        mazeBall.GetComponent<Rigidbody>().useGravity = true;
        mazeBall.transform.position = mazeBallSpawnPosition;
    }
}
