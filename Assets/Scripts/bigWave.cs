using UnityEngine;
using System.Collections;

public class bigWave : MonoBehaviour {
	public float force = 1;
	public Transform wavePos;
	public GameObject spleshPrefab;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Inst", 5,2);
		InvokeRepeating ("Inst", 6f,5f);
		InvokeRepeating ("Inst", 4,3);
		InvokeRepeating ("Inst", 6,6);
		InvokeRepeating ("Inst", 6,8);
		InvokeRepeating ("Inst", 6,9);
		InvokeRepeating ("Inst", 3f,4.4f);
	}

	public float waveLength;

	void Inst ()
	{
		Vector3 pos = wavePos.position; 
		var oldValue = GetComponent<Renderer> ().material.GetFloat("_Speed");
		pos.x -= oldValue + Time.deltaTime + 2; 
		pos.z += Random.Range(0f,22f); 
		Object splesh = Instantiate (spleshPrefab, pos, Quaternion.identity); 
		Destroy (splesh, 5); 
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer>().material.SetVector ("_WavePos", new Vector4(wavePos.position.x,wavePos.position.y,wavePos.position.z,0));
		var oldValue = GetComponent<Renderer> ().material.GetFloat("_Speed");
		oldValue %= 45;
		GetComponent<Renderer>().material.SetFloat("_Speed", oldValue + Time.deltaTime);
		 
	}
}
