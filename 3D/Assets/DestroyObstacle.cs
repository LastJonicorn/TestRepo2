using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{

    public GameObject replacementPrefab;

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Player"))
        {

            // Spawn the replacement prefab at the same position and rotation
            Instantiate(replacementPrefab, transform.position, transform.rotation);

            // Destroy the current player object
            Destroy(gameObject);
        }
    }
}
