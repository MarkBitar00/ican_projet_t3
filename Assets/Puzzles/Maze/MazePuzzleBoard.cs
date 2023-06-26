using UnityEngine;

public class MazePuzzleBoard : MonoBehaviour
{
    [SerializeField] private MazePuzzleManager manager;
    [SerializeField] private GameObject mazeBall;

    private void OnCollisionEnter(Collision collision)
    {
        if (mazeBall && manager && collision.collider.gameObject == mazeBall)
        {
            Debug.Log("Ball collision");
            manager.SpawnMazeBall();
        }
    }
}
