using UnityEngine;

public class HurdyGurdyKey : MonoBehaviour
{
    [SerializeField] private HurdyGurdyManager melodyManager;
    [SerializeField] public int note;
    
    public void PlayNote()
    {
        if (!melodyManager.lever.GetIsMoving()) return;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/one");
        if (melodyManager.GetIsSolved()) return;
        melodyManager.AddNoteToSequence(note);
    }
}
