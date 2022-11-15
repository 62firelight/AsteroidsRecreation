using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerLives : MonoBehaviour
{
    public float defaultRespawnCooldown = 1.5f;

    private SpriteRenderer sr;

    private Rigidbody2D rb;

    private Collider2D collider;

    private float respawnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnCooldown > 0 && GameMaster.playerLives > 0)
        {
            respawnCooldown -= Time.deltaTime;

            if (respawnCooldown < 0)
            {
                // Reset player rotation, linear velocity and angular velocity
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0.0f;

                sr.enabled = true;
                collider.enabled = true;        
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Asteroid")
        {
            return;
        }

        GameMaster.playerLives--;

        sr.enabled = false;
        collider.enabled = false;

        respawnCooldown = defaultRespawnCooldown;
    }
}
