using UnityEngine;
using System.Collections;

public class JsonTest : MonoBehaviour {

	// Use this for initialization
	void Start () { 
        BioData bdOld = new BioData();
        bdOld.bpm = 128;
        bdOld.temperature = 36.6f;
        bdOld.moisture = 0.1f;
        Debug.Log(bdOld.toJson()); 
        string json = bdOld.toJson();
        BioData bd = BioData.getInstanceFromJson(json);   

        Debug.Log("Recreated"); 
        Debug.Log(bd.toJson()); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
