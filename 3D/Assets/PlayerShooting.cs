using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float range = 100f; // Shooting distance
    public float damage = 25f; // Damage dealt per shot
    public Camera fpsCam; // Reference to the player's camera

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left Mouse Button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Try to apply damage if the object has a script with a TakeDamage method
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
