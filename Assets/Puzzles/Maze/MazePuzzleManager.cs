using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class MazePuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject mazeBall;
    [SerializeField] private GameObject mazeBoard;
    [SerializeField] private int numberOfPiecesToAttach;
    
    private Vector3 mazeBallSpawnPosition;
    private List<GameObject> currentlyAttachedPieces = new List<GameObject>();
    [SerializeField] private UnityEvent WhenSolved;

    public void Finished()
    {
        WhenSolved.Invoke();
    }

    private void Start()
    {
        if (mazeBall)
        {
            mazeBallSpawnPosition = mazeBall.transform.localPosition;  
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
            Debug.Log("Puzzle completed");
            Invoke(nameof(DeleteObjectComponent), 1f);
            // var mazeRigidBody = transform.gameObject.GetComponent<Rigidbody>();
            // mazeRigidBody.isKinematic = false;
            // mazeRigidBody.useGravity = true;
            var mazeGrab = transform.gameObject.AddComponent<XRGrabInteractable>();
            mazeGrab.selectMode = InteractableSelectMode.Multiple;
            mazeGrab.useDynamicAttach = true;
            if (mazeBoard)
            {
                mazeBoard.GetComponent<Renderer>().material.color = Color.cyan;
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
        mazeBall.transform.localPosition = mazeBallSpawnPosition;
    }
}
