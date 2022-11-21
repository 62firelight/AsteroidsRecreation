using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILives : MonoBehaviour
{
    public GameObject[] lives;

    public GameObject gameOver;

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.playerLives < 3)
        {
            lives[GameMaster.playerLives].active = false;
        }

        if (GameMaster.playerLives <= 0)
        {
            gameOver.active = true;
        }
    }
}
