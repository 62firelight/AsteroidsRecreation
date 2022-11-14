using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOutsideView : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!Utility.isVisible(GetComponent<Renderer>(), Camera.main))
        {
            Destroy(gameObject);
        }
    }
    
}
