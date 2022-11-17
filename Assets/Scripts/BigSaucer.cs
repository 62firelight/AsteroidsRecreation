using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(DespawnOutsideView))]
public class BigSaucer : MonoBehaviour
{
    public float speed = 2.0f;
    
    public Transform projectilePrefab;

    public float projectileSpeed = 10.0f;

    public float defaultProjectileCooldown = 1.0f;

    public float defaultSpawnCooldown = 5.0f;

    private float spawnCooldown;

    private float projectileCooldown;

    private Rigidbody2D rb;

    private Collider2D collider;

    private DespawnOutsideView despawnOutsideView;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        despawnOutsideView = GetComponent<DespawnOutsideView>();

        if (transform.position.x > 0)
        {
            speed = -speed;
        }

        rb.velocity = new Vector2(speed, 0);

        projectileCooldown = defaultProjectileCooldown;
        spawnCooldown = defaultSpawnCooldown;

        despawnOutsideView.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCooldown > 0)
        {
            spawnCooldown -= Time.deltaTime;

            if (spawnCooldown <= 0)
            {
                despawnOutsideView.enabled = true;
            }
        }

        if (projectileCooldown > 0)
        {
            projectileCooldown -= Time.deltaTime;

            if (projectileCooldown <= 0)
            {
                ShootProjectile();
                projectileCooldown = defaultProjectileCooldown;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    void ShootProjectile()
    {
        // Create projectile, set its position and rotation
        Transform projectile = Instantiate(projectilePrefab);
        projectile.position = new Vector3(transform.position.x, transform.position.y, 1);

        // Ignore collisions between player and projectile
        Collider2D projectileCollider = projectile.gameObject.GetComponent<Collider2D>();
        if (projectileCollider != null && collider != null)
        {
            Physics2D.IgnoreCollision(projectileCollider, collider);
        }

        // Add force to the projectile
        Rigidbody2D projectileRb = projectile.gameObject.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(GenerateRandomDirection() * projectileSpeed);
    }

    Vector2 GenerateRandomDirection()
    {
        Vector2 randomDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        while (randomDirection.x == 0 && randomDirection.y == 0)
        {
            randomDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }
        randomDirection.Normalize();

        return randomDirection;
    }
}
