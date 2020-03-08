using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SynchroText : Photon.MonoBehaviour
{   public string mex;
    private void Start() {
    
}
    private void Update() {
        
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting && GetComponent<TextMeshProUGUI>().text != "_^_"){
            stream.SendNext(GetComponent<TextMeshProUGUI>().text);
            Debug.Log("writing"+mex);
        }else if(!stream.isWriting){
            GetComponent<TextMeshProUGUI>().text = (string)stream.ReceiveNext();
            Debug.Log("reading"+mex);
        }
    }
}
