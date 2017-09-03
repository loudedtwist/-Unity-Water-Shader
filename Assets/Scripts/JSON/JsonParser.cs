using UnityEngine;
using System.Collections;

public class JsonParser {
    static public BioData getBioData(string json){
        return JsonUtility.FromJson<BioData>(json);
    }
}
