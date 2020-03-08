using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class QuestManagerMultiplayer : Photon.MonoBehaviour
{
    public List<Quest> questsNouns = new List<Quest>();
    public List<Quest> questsVerbs = new List<Quest>();
    public List<Quest> questsAdjectives = new List<Quest>();

    [SerializeField]
    private Quest currentQuest;

    public TMP_InputField inputField;
    public TextMeshProUGUI questText;
    public TextMeshProUGUI time;
    public Image timeBar;

    [SerializeField]
    GameObject GameManager;
    [SerializeField]
    TextMeshProUGUI text1;
    [SerializeField]
    TextMeshProUGUI text2;

    bool currentLat = false;
    float timeStart = 0.0f;
    bool can = true;

    void Start()
    {
        IO a = GetComponent<IO>();
        a.ReadVerbs(Application.dataPath + "/VerbiLatino.txt",Application.dataPath + "/VerbiItaliano.txt");
        a.ReadNouns(Application.dataPath + "/NomiLatino.txt",Application.dataPath + "/NomiItaliano.txt");
        a.ReadAdjs(Application.dataPath + "/AggettiviLatino.txt",Application.dataPath + "/AggettiviItaliano.txt");

        Ask(Random.Range(0,3));
    }

    void Update()
    {
        if(GameManager.GetComponent<Server>().enter && GetComponent<MatchManager>().allDone){
            text1.text = PhotonNetwork.playerList[0].NickName + " " + PhotonNetwork.playerList[0].GetScore();
            text2.text = PhotonNetwork.playerList[1].GetScore()+ " " + PhotonNetwork.playerList[1].NickName;

            if(PhotonNetwork.playerList[0].GetScore() >= 10){
                PhotonNetwork.Disconnect();
                PlayerPrefs.SetInt("State",1);
                SceneManager.LoadScene("End");
            }else if(PhotonNetwork.playerList[1].GetScore() >= 10){
                
                PhotonNetwork.Disconnect();
                PlayerPrefs.SetInt("State",0);
                SceneManager.LoadScene("End");
            }

            if(can){
                timeStart = Time.time;
                can = false;
            }

            if(Time.time-timeStart >= 10){
                
                PhotonNetwork.Disconnect();
                PlayerPrefs.SetInt("State",0);
                SceneManager.LoadScene("End");//come sa l'altro giocatore che ha vinto??
            }else{
                timeBar.fillAmount = 1 - ((Time.time-timeStart)/10);
                time.text = "00:" + (10 - Mathf.Round(Time.time-timeStart)).ToString();
            }
            /* così=> (ma non funzia perchè subito vede 1 pl ed esce)
            if(PhotonNetwork.countOfPlayers < 2){
                PhotonNetwork.Disconnect();
                PlayerPrefs.SetInt("State",1);
                SceneManager.LoadScene("End");
            }*/

            Submit();
        }
    }

    void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("Player Disconnected " + player.NickName);
        if(player.NickName != PhotonNetwork.player.NickName)
        {
            PhotonNetwork.Disconnect();
            PlayerPrefs.SetInt("State", 1);
            SceneManager.LoadScene("End");
        }
    }

    void Ask(int choice){
        if(choice == 0){
            int rnd = Random.Range(0,questsNouns.Count);
            currentQuest = questsNouns[rnd];
            questsNouns.Remove(questsNouns[rnd]);
            viewQuest(currentQuest);
        }
        if(choice == 1){
            int rnd = Random.Range(0,questsVerbs.Count);
            currentQuest = questsVerbs[rnd];
            questsVerbs.Remove(questsVerbs[rnd]);
            viewQuest(currentQuest);
        }
        if(choice == 2){
            int rnd = Random.Range(0,questsAdjectives.Count);
            currentQuest = questsAdjectives[rnd];
            questsAdjectives.Remove(questsAdjectives[rnd]);
            viewQuest(currentQuest);
        }
    }

    void viewQuest(Quest _quest){
        questText.enabled = true;
        
        if(Random.Range(0,2) == 0){
            questText.text = _quest.italian;
            currentLat = false;
        }else{
            questText.text = _quest.latin;
            currentLat = true;
        }
        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void Submit(){
        string res = inputField.text.Replace(" ","").Replace("\n","").ToLower();
        if(currentLat){
            if(res == currentQuest.italian.Replace(" ","").ToLower()){
                PhotonNetwork.playerList[1].SetScore(PhotonNetwork.playerList[1].GetScore()+1);
                timeStart = Time.time;
                Ask(Random.Range(0,3));
            }
        }else{
            if(res == currentQuest.latin.Replace(" ","").ToLower()){
                PhotonNetwork.playerList[1].SetScore(PhotonNetwork.playerList[1].GetScore()+1);
                timeStart = Time.time;
                Ask(Random.Range(0,3));
            }
        }
    }

}
