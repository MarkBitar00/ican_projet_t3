using UnityEngine;

public class HurdyGurdyKey : MonoBehaviour
{
    [SerializeField] private HurdyGurdyManager melodyManager;
    [SerializeField] public int note;
    
    public void PlayNote()
    {
        if (!melodyManager.lever.isMoving) return;
        Debug.Log("Played note " + transform.name);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/one");
        if (melodyManager.isSolved) return;
        melodyManager.AddNoteToSequence(note);
    }
}
