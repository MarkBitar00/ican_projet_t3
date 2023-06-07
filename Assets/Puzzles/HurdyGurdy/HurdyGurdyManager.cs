using System.Linq;
using UnityEngine;

public class HurdyGurdyManager : MonoBehaviour
{
    private int[] puzzleMelody = { 1, 3, 4, 2, 5 };
    private int[] playedMelody = new int[5];
    private int noteIndex = 0;

    [SerializeField] public HurdyGurdyLever lever;
    public bool isSolved = false;

    public void AddNoteToSequence(int note)
    {
        playedMelody[noteIndex] = note;
        noteIndex++;
        if (noteIndex < puzzleMelody.Length) return;
        if (playedMelody.SequenceEqual(puzzleMelody))
        {
            Debug.Log($"Played melody was : {string.Join(", ", playedMelody)}");
            Debug.Log("You win!");
            isSolved = true;
        }
        else
        {
            Debug.Log($"Played melody was : {string.Join(", ", playedMelody)}");
            Debug.Log("You lose...");
            playedMelody = new int[5];
            noteIndex = 0;
        }
    }
}
