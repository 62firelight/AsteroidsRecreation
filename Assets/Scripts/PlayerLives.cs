using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShoot))]
public class PlayerLives : MonoBehaviour
{
    public float defaultRespawnCooldown = 1.5f;

    private Transform spawnLocation;

    private SpawnProtection spawnProtection;

    private SpriteRenderer sr;

    private Rigidbody2D rb;

    private Collider2D collider;

    private PlayerMovement playerMovement;

    private PlayerShoot playerShoot;

    private float respawnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();

        // Create a temporary box collider that auto calculates the player size
        BoxCollider2D tempCollider = gameObject.AddComponent<BoxCollider2D>();
        Vector2 playerSize = tempCollider.size;

        // Create an empty game object that acts as the player's spawn location
        GameObject spawnLocationObj = new GameObject("SpawnLocation");
        spawnLocation = spawnLocationObj.transform;

        // Add relevant components to the empty game object to intelligently spawn in player
        BoxCollider2D spawnLocationCollider = spawnLocationObj.AddComponent<BoxCollider2D>();
        spawnLocationCollider.size = playerSize;
        spawnLocationCollider.isTrigger = true;
        spawnProtection = spawnLocationObj.AddComponent<SpawnProtection>();

        // Get rid of the temporary box collider
        Destroy(tempCollider);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.playerLives > 0)
        {
            if (respawnCooldown > 0)
            {
                respawnCooldown -= Time.deltaTime;

                if (respawnCooldown < 0 && spawnProtection.GetIsSafe())
                {
                    sr.enabled = true;
                    collider.enabled = true;
                    playerMovement.enabled = true;
                    playerShoot.enabled = true;
                }
                else if (!spawnProtection.GetIsSafe())
                {
                    respawnCooldown = defaultRespawnCooldown / 4.0f;
                }
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
        playerMovement.enabled = false;
        playerShoot.enabled = false;

        // Reset player rotation, linear velocity and angular velocity
        transform.position = spawnLocation.position;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;

        respawnCooldown = defaultRespawnCooldown;
    }
}
