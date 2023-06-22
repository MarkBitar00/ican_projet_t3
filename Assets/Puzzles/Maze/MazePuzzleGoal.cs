using UnityEngine;

public class MazePuzzleGoal : MonoBehaviour
{
    [SerializeField] private GameObject mazeBall;

    private void OnCollisionEnter(Collision collision)
    {
        if (mazeBall && collision.collider.gameObject == mazeBall)
        {
            Debug.Log("You win !");
        }
    }
}
