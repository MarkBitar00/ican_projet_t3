using UnityEngine;

public class HurdyGurdyKey : MonoBehaviour
{
    [SerializeField] private HurdyGurdyManager melodyManager;
    [SerializeField] public int note;
    
    private string[] noteSoundEvents = new string[8]{"HurdyGurdy1", "HurdyGurdy2", "HurdyGurdy3", "HurdyGurdy4", "HurdyGurdy5", "HurdyGurdy6", "HurdyGurdy7", "HurdyGurdy8"};
    
    public void PlayNote()
    {
        if (!melodyManager.lever.GetIsMoving()) return;
        FMODUnity.RuntimeManager.PlayOneShot($"event:/Sounds/{noteSoundEvents[note - 1]}");
        if (melodyManager.GetIsSolved()) return;
        melodyManager.AddNoteToSequence(note);
    }
}
