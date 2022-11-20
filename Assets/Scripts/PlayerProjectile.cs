using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float defaultDespawnCooldown = 0.6f;

    private float despawnCooldown = 0.0f;

    void Start()
    {
        despawnCooldown = defaultDespawnCooldown;
    }

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

    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}
