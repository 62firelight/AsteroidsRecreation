using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerUtility))]
public class PlayerLives : MonoBehaviour
{
    public Transform explosionPrefab;

    public float defaultRespawnCooldown = 2.0f;

    private Transform spawnLocation;

    private SpawnProtection spawnProtection;

    private Rigidbody2D rb;

    private PlayerUtility playerUtility;

    private float respawnCooldown;

    private HashSet<string> collidableObjects = new HashSet<string>() { 
        "Asteroid",
        "Projectile",
        "Saucer"
    };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerUtility = GetComponent<PlayerUtility>();

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
                    playerUtility.UnhidePlayer();
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
        if (!collidableObjects.Contains(other.gameObject.tag))
        {
            return;
        }

        GameMaster.playerLives--;

        playerUtility.HidePlayer();

        for (int i = 0; i < 4; i++)
        {
            Transform explosion = Instantiate(explosionPrefab);
            explosion.position = transform.position;
            explosion.Rotate(0.0f, 0.0f, Random.Range(0.1f, 360.0f));
        }

        // Reset player rotation, linear velocity and angular velocity
        transform.position = spawnLocation.position;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;

        respawnCooldown = defaultRespawnCooldown;
    }
}
