using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMaster
{
    
    public static int score = 0;

    public static int playerMaxLives = 3;

    public static int playerLives = playerMaxLives;

    public static int levelsCompleted = 0;

    public static int asteroidsLeft = 1;

    public static int extraLifeThreshold = 10000;

    public static void Reset()
    {
        score = 0;
        playerLives = 3;
        levelsCompleted = 0;
        asteroidsLeft = 1;
        extraLifeThreshold = 10000;
    }

    public static void IncreaseScore(int source)
    {
        // Sources:
        // 0 - Big Asteroid (+20)
        // 1 - Medium Asteroid (+50)
        // 2 - Small Asteroid (+100)
        // 3 - Big Saucer (+200)
        // 4 - Small Saucer (+1000)

        switch (source)
        {
            case 0:
                score += 20;
                break;
            case 1:
                score += 50;
                break;
            case 2:
                score += 100;
                break;
            case 3:
                score += 200;
                break;
            case 4:
                score += 1000;
                break;
            default:
                break;
        }

        if (score > extraLifeThreshold)
        {
            playerLives++;
            extraLifeThreshold += 10000;
        }
    }

}
