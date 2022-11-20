using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Explosion : MonoBehaviour
{
    public float speed = 100.0f;

    public float defaultDespawnCooldown = 0.5f;

    private Rigidbody2D rb;

    private float despawnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Assign random direction and speed
        Vector2 direction = GenerateRandomDirection();
        float explosionSpeed = Random.Range(speed - 25.0f, speed + 50.0f);
        rb.AddForce(direction * explosionSpeed);

        // Assign random timer for despawn
        despawnCooldown = Random.Range(defaultDespawnCooldown - 0.5f, defaultDespawnCooldown + 0.5f) ;
    }

    // Update is called once per frame
    void Update()
    {
        if (despawnCooldown > 0)
        {
            despawnCooldown -= Time.deltaTime;

            if (despawnCooldown <= 0)
            {
                Destroy(gameObject);
            }
        }
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
