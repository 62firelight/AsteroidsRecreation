using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMaster
{
    
    public static int score = 0;

    public static int playerLives = 3;

    public static int levelsCompleted = 0;

    public static int asteroidsLeft = 1;

    public static void Reset()
    {
        score = 0;
        playerLives = 3;
        levelsCompleted = 0;
        asteroidsLeft = 1;
    }

}
