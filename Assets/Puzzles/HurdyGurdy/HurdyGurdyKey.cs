using System;
using UnityEngine;

public class HurdyGurdyKey : MonoBehaviour
{
    [SerializeField] private HurdyGurdyManager melodyManager;
    [SerializeField] public int note;

    private Animator animator;
    private string[] noteSoundEvents = new string[8]{"NoteOne", "NoteTwo", "NoteThree", "NoteFour", "NoteFive", "NoteSix", "NoteSeven", "NoteEight"};

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayNote()
    {
        if (!melodyManager.lever.GetIsMoving() || melodyManager.GetIsPlayingResolutionMelody()) return;
        animator.SetTrigger("PlayNote");
        FMODUnity.RuntimeManager.PlayOneShot($"event:/Diegetic/Sounds/HurdyGurdy/{noteSoundEvents[note - 1]}");
        if (melodyManager.GetIsSolved()) return;
        melodyManager.AddNoteToSequence(note);
    }
}
