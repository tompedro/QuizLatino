using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class IO : MonoBehaviour
{
    public void ReadVerbs(string path,string path2){
        StreamReader sReader = new StreamReader(path);  
        
        while(!sReader.EndOfStream){
            string line = sReader.ReadLine();
            line = line[0].ToString().ToUpper() + line.Substring(1);
            Quest q = new Quest();
            q.latin = line;
            if(GetComponent<QuestManagerSingleplayer>()){
                GetComponent<QuestManagerSingleplayer>().questsVerbs.Add(q);
            }else{
                GetComponent<QuestManagerMultiplayer>().questsVerbs.Add(q);
            }
            
        }

        sReader.Close();

        StreamReader sReader2 = new StreamReader(path2);
        int i = 0;
        while(!sReader2.EndOfStream){
            string line = sReader2.ReadLine();
            line = line[0].ToString().ToUpper() + line.Substring(1);
            if(GetComponent<QuestManagerSingleplayer>()){
                GetComponent<QuestManagerSingleplayer>().questsVerbs[i].italian = line;
            }else{
                GetComponent<QuestManagerMultiplayer>().questsVerbs[i].italian = line;
            }
            
            i++;
        }

        sReader2.Close();
    }

    public void ReadAdjs(string path,string path2){
        StreamReader sReader = new StreamReader(path);  
        
        while(!sReader.EndOfStream){
            string line = sReader.ReadLine();
            line = line[0].ToString().ToUpper() + line.Substring(1);
            Quest q = new Quest();
            q.latin = line;
            if(GetComponent<QuestManagerSingleplayer>()){
                GetComponent<QuestManagerSingleplayer>().questsAdjectives.Add(q);
            }else{
                GetComponent<QuestManagerMultiplayer>().questsAdjectives.Add(q);
            }
            
        }

        sReader.Close();

        StreamReader sReader2 = new StreamReader(path2);
        int i = 0;
        while(!sReader2.EndOfStream){
            string line = sReader2.ReadLine();
            line = line[0].ToString().ToUpper() + line.Substring(1);
            if(GetComponent<QuestManagerSingleplayer>()){
                GetComponent<QuestManagerSingleplayer>().questsAdjectives[i].italian = line;
            }else{
                GetComponent<QuestManagerMultiplayer>().questsAdjectives[i].italian = line;
            }
            
            i++;
        }

        sReader2.Close();
    }

    public void ReadNouns(string path,string path2){
        StreamReader sReader = new StreamReader(path);  
        
        while(!sReader.EndOfStream){
            string line = sReader.ReadLine();
            line = line[0].ToString().ToUpper() + line.Substring(1);
            Quest q = new Quest();
            q.latin = line;
            if(GetComponent<QuestManagerSingleplayer>()){
                GetComponent<QuestManagerSingleplayer>().questsNouns.Add(q);
            }else{
                GetComponent<QuestManagerMultiplayer>().questsNouns.Add(q);
            }
            
        }

        sReader.Close();

        StreamReader sReader2 = new StreamReader(path2);
        int i = 0;
        while(!sReader2.EndOfStream){
            string line = sReader2.ReadLine();
            line = line[0].ToString().ToUpper() + line.Substring(1);
            if(GetComponent<QuestManagerSingleplayer>()){
                GetComponent<QuestManagerSingleplayer>().questsNouns[i].italian = line;
            }else{
                GetComponent<QuestManagerMultiplayer>().questsNouns[i].italian = line;
            }
            
            i++;
        }

        sReader2.Close();
    }
}
