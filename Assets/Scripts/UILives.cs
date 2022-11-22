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
        for (int i = 0; i < GameMaster.playerMaxLives; i++)
        {
            if (i + 1 > GameMaster.playerLives)
            {
                lives[i].active = false;
            }
            else 
            {
                lives[i].active = true;
            }
        }

        if (GameMaster.playerLives <= 0)
        {
            gameOver.active = true;
        }
    }
}
