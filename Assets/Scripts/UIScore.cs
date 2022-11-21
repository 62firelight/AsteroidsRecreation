using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    //private TMP_FontAsset m_FontAsset;

    private const string label = "The <#0050FF>count is: </color>{0:2}";
    private float m_frame;


    void Start()
    {
        // Add new TextMesh Pro Component
        tmp = GetComponent<TextMeshProUGUI>();

        tmp.autoSizeTextContainer = true;

        // Set various font settings.
        tmp.fontSize = 24;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.enableWordWrapping = false;
    }


    void Update()
    {
        tmp.text = GameMaster.score.ToString();
    }
}
