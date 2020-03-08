using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MultiplayerEndManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = PlayerPrefs.GetInt("Status").ToString();
    }
}
