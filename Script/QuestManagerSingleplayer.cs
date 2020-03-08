using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManagerSingleplayer : MonoBehaviour
{
    public List<Quest> questsNouns = new List<Quest>();
    public List<Quest> questsVerbs = new List<Quest>();
    public List<Quest> questsAdjectives = new List<Quest>();

    public Color progress;
    public Color error;

    [SerializeField]
    private Quest currentQuest;
    private float currentProgress;

    public Image progressBar;
    public TMP_InputField inputField;
    public TextMeshProUGUI questText;
    public TextMeshProUGUI hintText;
    public Image questImage;
    public Button submit;

    private int errors;
    public float numberOfQuestions;

    private string playMode;
    private int binaryImages;

    void Start()
    {
        IO a = GetComponent<IO>();
        a.ReadVerbs(Application.dataPath + "/VerbiLatino.txt",Application.dataPath + "/VerbiItaliano.txt");
        a.ReadNouns(Application.dataPath + "/NomiLatino.txt",Application.dataPath + "/NomiItaliano.txt");
        a.ReadAdjs(Application.dataPath + "/AggettiviLatino.txt",Application.dataPath + "/AggettiviItaliano.txt");

        playMode = PlayerPrefs.GetString("Singleplayer");
        binaryImages = PlayerPrefs.GetInt("Toggle");

        Ask();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return)){
            Submit();
        }     
    }

    void Ask(){
        if(playMode == "Nouns"){
            int rnd = Random.Range(0,questsNouns.Count);
            currentQuest = questsNouns[rnd];
            questsNouns.Remove(questsNouns[rnd]);

            if(binaryImages == 1){
                if(Random.Range(0,2) == 0){
                    viewQuest(currentQuest,true);
                }else{
                    viewQuest(currentQuest,false);
                }
            }else{
                viewQuest(currentQuest,false);
            }
        }
        if(playMode == "Verbs"){
            int rnd = Random.Range(0,questsVerbs.Count);
            currentQuest = questsVerbs[rnd];
            questsVerbs.Remove(questsVerbs[rnd]);
            viewQuest(currentQuest,false);
        }
        if(playMode == "Adjectives"){
            int rnd = Random.Range(0,questsAdjectives.Count);
            currentQuest = questsAdjectives[rnd];
            questsAdjectives.Remove(questsAdjectives[rnd]);
            viewQuest(currentQuest,false);
        }
    }

    void viewQuest(Quest _quest,bool image){
        progressBar.fillAmount = currentProgress/numberOfQuestions;
        if(image){
            questText.enabled = false;
            questImage.enabled = true;
            questImage = _quest.image;
        }else{
            questText.enabled = true;
            questImage.enabled = false;
            
            if(PlayerPrefs.GetInt("Ita-lat") == 0){
                questText.text = _quest.italian;
            }else{
                questText.text = _quest.latin;
            }
        }
        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void Submit(){
        string res = inputField.text.Replace(" ","").Replace("\n","").ToLower();
        if(PlayerPrefs.GetInt("Ita-lat") == 0){
            if(res == currentQuest.latin.Replace(" ","").ToLower()){
                currentProgress++;
                questText.color = progress;
                StartCoroutine(wait(2f,"addProgress"));
            }else{
                errors++;
                questText.color = error;
                StartCoroutine(wait(2f,"addErrors"));
            }
        }else{
            if(res == currentQuest.italian.Replace(" ","").ToLower()){
                currentProgress++;
                questText.color = progress;
                StartCoroutine(wait(2f,"addProgress"));
            }else{
                errors++;
                questText.color = error;
                StartCoroutine(wait(2f,"addErrors"));
            }
        }
    }

    void addProgress(){
        
        questText.color = Color.white;
        if(currentProgress >= numberOfQuestions){
            submit.interactable = false;
            hintText.text = "YOU WIN!";
            hintText.enabled = true;
            StartCoroutine(wait(10f,"returnToMenu"));
        }
        Ask();
    }

    void addErrors(string correctWord){
        
        questText.color = Color.white;
        inputField.text = "";
        inputField.ActivateInputField();
        if(errors >= 3){
            submit.interactable = false;
            hintText.text = "GAME OVER! The correct word was " + correctWord;
            hintText.enabled = true;
            StartCoroutine(wait(10f,"returnToMenu"));
            
        }
    }

    void returnToMenu(){
        Application.LoadLevel(0);
    }

    IEnumerator wait(float s,string fun){
        yield return new WaitForSeconds(s);
        Invoke(fun,0f);
    }

}
