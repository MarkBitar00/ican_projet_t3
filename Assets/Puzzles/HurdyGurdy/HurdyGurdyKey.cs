using UnityEngine;

public class HurdyGurdyKey : MonoBehaviour
{
    [SerializeField] private HurdyGurdyLever lever;
    
    public void PlayNote()
    {
        if (!lever.isMoving)
        {
            Debug.Log("Played note " + transform.name);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/one");
        }
    }
}
