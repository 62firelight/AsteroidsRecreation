using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    public GameObject pause;

    // Start is called before the first frame update
    void Start()
    {
        pause.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause.active == false)
            {
                pause.active = true;
                Time.timeScale = 0.0f;
            }
            else
            {
                pause.active = false;
                Time.timeScale = 1.0f;
            }
        }
    }
}
