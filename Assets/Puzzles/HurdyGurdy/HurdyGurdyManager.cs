using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HurdyGurdyManager : MonoBehaviour
{
    private int[][] puzzleMelody = new int[3][]{new int[]{3, 5, 6, 2, 3}, new int[]{7, 4, 3, 6, 5}, new int[]{1, 5, 1, 2, 3}};
    private int melodyIndex = 0;
    private List<int> playedMelody = new List<int>();
    private bool isSolved = false;

    [SerializeField] public HurdyGurdyLever lever;

    public void AddNoteToSequence(int note)
    {
        var currentMelody = puzzleMelody[melodyIndex];
        playedMelody.Add(note);
        if (playedMelody.Count < currentMelody.Length) return;
        if (playedMelody.SequenceEqual(currentMelody))
        {
            Debug.Log($"Played melody was : {string.Join(", ", playedMelody)}");
            Debug.Log("You win this round!");
            CheckIfAllMelodiesCompleted();
        }
        else
        {
            Debug.Log($"Played melody was : {string.Join(", ", playedMelody)}");
            Debug.Log("You lose this round...");
            playedMelody = new List<int>();
        }
    }

    private void CheckIfAllMelodiesCompleted()
    {
        melodyIndex++;
        if (melodyIndex < puzzleMelody.Length)
        {
            playedMelody = new List<int>();
        }
        else
        {
            Debug.Log("You won EVERY round!");
            isSolved = true;
        }
    }

    public bool GetIsSolved()
    {
        return isSolved;
    }
}
