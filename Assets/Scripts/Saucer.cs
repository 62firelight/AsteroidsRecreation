using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(DespawnOutsideView))]
public class Saucer : MonoBehaviour
{
    public bool isSmallSaucer = false;

    public float speed = 2.0f;

    public Transform projectilePrefab;

    public Transform explosionPrefab;

    public float projectileSpeed = 2.5f;

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

        projectileCooldown = defaultProjectileCooldown / 2.0f;
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

    void FixedUpdate()
    {
        // The saucer has a 1% chance of changing where it moves
        int randomDiagonalRoll = Random.Range(1, 101);
        if (randomDiagonalRoll == 1)
        {
            // If already going up or down, stop going up or down
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else
            {
                // Decide whether to go up or down
                int randomUpRoll = Random.Range(0, 2);

                if (randomUpRoll > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, speed);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, -speed);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            // TODO: Check if projectile came from player
            IncreaseScore();
        }

        Destroy(gameObject);

        // Spawn explosions
        for (int i = 0; i < 5; i++)
        {
            Transform explosion = Instantiate(explosionPrefab);
            explosion.position = transform.position;
        }
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

        // Decide which direction the projectile will go
        Vector2 projectileDirection;
        if (isSmallSaucer)
        {
            Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            projectileDirection = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
            projectileDirection.Normalize();
        }
        else
        {
            projectileDirection = GenerateRandomDirection();
        }

        // Add force to the projectile
        Rigidbody2D projectileRb = projectile.gameObject.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(projectileDirection * projectileSpeed);
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

    void IncreaseScore()
    {
        if (isSmallSaucer)
        {
            GameMaster.IncreaseScore(4);
        }
        else
        {
            GameMaster.IncreaseScore(3);
        }
    }
}
