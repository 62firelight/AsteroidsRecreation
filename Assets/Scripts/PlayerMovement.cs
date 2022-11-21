using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerUtility))]
public class PlayerMovement : MonoBehaviour
{
    public bool movementEnabled = true;

    public float launchSpeed = 150.0f;

    public float turnSpeed = 2.0f;
    
    public float defaultTeleportCooldown = 1.0f;

    public GameObject thrustObj;

    private Rigidbody2D rb;

    private PlayerUtility playerUtility;

    private float teleportCooldown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerUtility = GetComponent<PlayerUtility>();

        thrustObj.active = false;
    }

    void Update()
    {
        if (teleportCooldown > 0)
        {
            teleportCooldown -= Time.deltaTime;

            if (teleportCooldown <= 0)
            {
                // Get coordinates for bottom-left point and top-right point of screen
                Vector2 bottomLeftPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
                Vector2 topRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));

                // Teleport the player
                Vector2 randomPos = new Vector2(Random.Range(bottomLeftPoint.x, topRightPoint.x), Random.Range(bottomLeftPoint.y, topRightPoint.y));
                transform.position = randomPos;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0.0f;

                playerUtility.UnhidePlayer();
            }
        }

        // Rotational movement
        float directionInput = Input.GetAxis("Horizontal");
        if (directionInput != 0)
        {
            rb.angularVelocity = -directionInput * turnSpeed;
        }

        // Whenever either of the Shift buttons are pressed, teleport the player to a random place
        else if (movementEnabled && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            teleportCooldown = defaultTeleportCooldown;
            playerUtility.HidePlayer();
        }
    }

    void FixedUpdate()
    {
        if (!movementEnabled)
        {
            thrustObj.active = false;
            return;
        }

        // Launch/thrust movement
        float forwardInput = Input.GetAxis("Vertical");
        if (forwardInput > 0)
        {
            thrustObj.active = true;
            rb.AddForce(transform.up * launchSpeed * Time.fixedDeltaTime);
        }
        else
        {
            thrustObj.active = false;
        }
    }
}
