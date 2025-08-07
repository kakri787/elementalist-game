using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private Transform projectileSpawn;
    [SerializeField]
    private GameObject projectilePrefab;
    private bool canSpawn = true;

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnProjectile(float fireRate, float projectileSpeed, float projectileRange, Quaternion direction)
    {
        if (canSpawn)
        {
            // Instance of projectile prefab
            GameObject spawnedProjectile = Instantiate(projectilePrefab, projectileSpawn.position, direction);

            Projectile projectile = spawnedProjectile.GetComponent<Projectile>();
            projectile.speed = projectileSpeed;
            projectile.maxRange = projectileRange;

            canSpawn = false;
            StartCoroutine(ProjectileSpawnDelay(fireRate));
        }
    }

    IEnumerator ProjectileSpawnDelay(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        canSpawn = true;
    }
}
