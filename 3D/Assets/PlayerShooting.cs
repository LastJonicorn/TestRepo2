using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float range = 100f; // Shooting distance
    public float damage = 25f; // Damage per shot
    public Camera fpsCam; // Reference to the player's camera
    public ParticleSystem muzzleFlash; // Muzzle flash effect
    public GameObject impactEffectPrefab; // The empty GameObject containing all hit effects
    public LayerMask selectedLayers; // Only these layers will be hit

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left Mouse Button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash?.Play(); // Play muzzle flash if assigned

        RaycastHit hit;

        // Use only selected layers for the Raycast (do NOT use `~`)
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, selectedLayers))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Apply damage if the object has a health script
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // Instantiate the full impact effect prefab
            if (impactEffectPrefab != null)
            {
                GameObject impactEffect = Instantiate(impactEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));

                // Automatically destroy the effect after a short delay
                Destroy(impactEffect, 2f); // Adjust if needed
            }
        }
    }
}
