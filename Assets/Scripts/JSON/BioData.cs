using UnityEngine;
using System;

[Serializable]
public class BioData {
    public int bpm;
    public float temperature;
    public float moisture;
    public string toJson(){
        return JsonUtility.ToJson(this);
    } 
    public static BioData getInstanceFromJson(string json){
        return JsonUtility.FromJson<BioData>(json);
    }
} 
