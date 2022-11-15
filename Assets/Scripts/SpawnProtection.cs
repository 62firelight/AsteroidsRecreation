using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnProtection : MonoBehaviour
{
    private bool isSafe = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        isSafe = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        isSafe = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isSafe = true;
    }

    public bool GetIsSafe()
    {
        return isSafe;
    }
}
