using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas singleplayerCanvas;
    public InputField inputField;
    public Canvas menuCanvas;
    public Toggle toggle;
    public Toggle toggle_1;

    public void Singleplayer(){
        singleplayerCanvas.enabled = true;
        menuCanvas.enabled = false;
    }
    public void exitSingleplayer(){
        singleplayerCanvas.enabled = false;
        menuCanvas.enabled = true;
    }
    public void singleplayerType(string type){
        PlayerPrefs.SetString("Singleplayer",type);
        if(toggle.isOn){
            PlayerPrefs.SetInt("Toggle",1);
        }else{
            PlayerPrefs.SetInt("Toggle",0);
        }
        if(toggle_1.isOn){
            PlayerPrefs.SetInt("Ita-Lat",1);
        }else{
            PlayerPrefs.SetInt("Ita-Lat",0);
        }
        SceneManager.LoadScene("Singleplayer");
    }

    public void Multiplayer(){
        if(inputField.text != "_^_"){
            PlayerPrefs.SetString("Name",inputField.text);
            SceneManager.LoadScene("Multiplayer");
        }else{
            //todo
        }
        
    }
}
