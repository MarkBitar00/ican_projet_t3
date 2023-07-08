using UnityEngine;

public class HurdyGurdyKey : MonoBehaviour
{
    [SerializeField] private HurdyGurdyManager melodyManager;
    [SerializeField] public int note;
    
    private string[] noteSoundEvents = new string[8]{"NoteOne", "NoteTwo", "NoteThree", "NoteFour", "NoteFive", "NoteSix", "NoteSeven", "NoteEight"};
    
    public void PlayNote()
    {
        if (!melodyManager.lever.GetIsMoving()) return;
        FMODUnity.RuntimeManager.PlayOneShot($"event:/Sounds/HurdyGurdy/{noteSoundEvents[note - 1]}");
        if (melodyManager.GetIsSolved()) return;
        melodyManager.AddNoteToSequence(note);
    }
}
