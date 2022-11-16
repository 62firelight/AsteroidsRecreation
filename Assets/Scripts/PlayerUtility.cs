using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShoot))]
public class PlayerUtility : MonoBehaviour
{
    private SpriteRenderer sr;

    private Collider2D collider;

    private PlayerMovement playerMovement;

    private PlayerShoot playerShoot;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    public void HidePlayer()
    {
        sr.enabled = false;
        collider.enabled = false;
        playerMovement.movementEnabled = false;
        playerShoot.enabled = false;
    }

    public void UnhidePlayer()
    {
        sr.enabled = true;
        collider.enabled = true;
        playerMovement.movementEnabled = true;
        playerShoot.enabled = true;
    }
}
