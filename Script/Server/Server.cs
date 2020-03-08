using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Server : Photon.MonoBehaviour
{   
    int key = 0;
    public bool enter = false;

    private void Start() {
        tryServer();
    }

    void tryServer()
    {
        key++;
        PhotonNetwork.ConnectUsingSettings(key.ToString());

        PhotonNetwork.autoJoinLobby = true;
        Debug.LogError("asd");
        Debug.Log("TRY CONNECT TO SERVER : " + key);
    }

    public void OnConnetingToServer()
    {
        PhotonNetwork.ConnectUsingSettings(key.ToString());
        PhotonNetwork.autoJoinLobby = true;
    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100), PhotonNetwork.connectionState.ToString());
    }
    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    void OnPhotonRandomJoinFailed()
    {
        RoomOptions r = new RoomOptions();
        r.MaxPlayers = 2;
        Debug.Log("CREATE ROOM");
        PhotonNetwork.CreateRoom(key.ToString(),r,TypedLobby.Default);
    }
    void OnJoinedRoom()
    {
        enter = true;
    }
    void OnConnectionFail()
    {
        tryServer();
    }

    void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        PhotonNetwork.Disconnect();
    }
    public string Shuffle(string str)
    {
        char[] array = str.ToCharArray();
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
    }
}