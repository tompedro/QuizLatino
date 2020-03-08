using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : Photon.MonoBehaviour
{
    public GameObject GameManager;
    public GameObject Loader;
    public Canvas canvas1;
    public Canvas canvas2;

    public bool allDone = false;

    private void Update() {
        if(GameManager.GetComponent<Server>().enter){
            if(PhotonNetwork.room.PlayerCount == 2){
                PhotonNetwork.playerName = PlayerPrefs.GetString("Name");
                canvas1.enabled = false;
                canvas2.enabled = true;
                allDone = true;
            }
        }
    }

    public void Retry(){
        PhotonNetwork.Disconnect();
        GameManager.GetComponent<Server>().enabled = false;
        GameManager.GetComponent<Server>().enabled = true;
    }
}
