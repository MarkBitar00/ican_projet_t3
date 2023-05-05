using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ValveInteractable : MonoBehaviour
{
    private IXRSelectInteractor currentInteractor;
    private XRSimpleInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void Update()
    {
        if (currentInteractor != null) transform.LookAt(currentInteractor.transform.position);
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        currentInteractor = arg0.interactorObject;
        arg0.interactorObject.selectExited.AddListener(OnButtonReleased);
    }

    private void OnButtonReleased(SelectExitEventArgs arg0)
    {
        currentInteractor.selectExited.RemoveListener(OnButtonReleased);
        currentInteractor = null;
    }
}