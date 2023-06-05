using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MazePuzzlePiece : MonoBehaviour
{
    [SerializeField] private MazePuzzleManager puzzleManager;
    [SerializeField] private Transform correctPieceTransform;
    private XRSocketInteractor socket;

    private void Awake() => socket = GetComponent<XRSocketInteractor>();

    private void OnEnable()
    {
        socket.selectEntered.AddListener(PieceAttached);
        socket.selectExited.AddListener(PieceRemoved);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(PieceAttached);
        socket.selectExited.RemoveListener(PieceRemoved);
    }

    private void PieceAttached(SelectEnterEventArgs arg0)
    {
        var attachedPiece = arg0.interactableObject;
        if (attachedPiece.transform.name == correctPieceTransform.name)
        {
            puzzleManager.AttachPiece(attachedPiece.transform.gameObject);
        }
    }

    private void PieceRemoved(SelectExitEventArgs arg0)
    {
        var removedPiece = arg0.interactableObject;
        if (removedPiece.transform.name == correctPieceTransform.name)
        {
            puzzleManager.RemovePiece(removedPiece.transform.gameObject);
        }
    }
}
