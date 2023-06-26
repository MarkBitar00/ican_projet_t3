using System;
using UnityEngine;

public class MazePuzzleGoal : MonoBehaviour
{
    [SerializeField] private GameObject mazeBall;
    [SerializeField] private GameObject mazeBoard;

    private void OnTriggerEnter(Collider other)
    {
        if (mazeBall && mazeBoard && other.gameObject == mazeBall)
        {
            Debug.Log("You win !");
            mazeBoard.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
