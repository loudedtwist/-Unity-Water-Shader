using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class OceanWave : MonoBehaviour, MeshSinDeformController, MeshRotateController, SpleshController {

	public float force = 1;
	public Transform wavePos;
	public Transform spleshAreaPos;
	public GameObject spleshPrefab;
    public GameObject particleContainer;
	// Use this for initialization

	private Mesh mesh  ;
	private Vector3[] vertices ;
	private MeshFilter mf;
	public List<MeshDeformInterface> meshMods = new List<MeshDeformInterface>();  

	public float freq = 0.1f;
	[Range(0, 2)]
	public float amp  = 0.01f;
	public float waveLength = 0.05f; 
	public float rotationX = 1.6f; 

	public float spleshAreaRadius = 100f;

	#region MeshSinDeformController implementation

	public float getWaveLength ()
	{
		return waveLength;
	}

	public float getFreq ()
	{
		return  freq;
	}

	public float getAmp ()
	{
		return amp;
	}
	public void setAmp (float amp)
	{
		this.amp = amp;
	}
	public Vector3 getWaveStartPosition ()
	{
		return wavePos.position;
	}

	#endregion


	#region MeshRotateController implementation

	public float getOffset ()
	{
		return rotationX;
	}

	#endregion

	public void setRotationX (float offset)
	{
		rotationX = offset;
	}

	Vector3 lastInstParticlePos = new Vector3(0,0,0);
	#region SpleshController implementation
	public void createSplesh (Vector3 pos)
	{
        if (amp < 0.45) return;
		float distSqr = (spleshAreaPos.position - pos).sqrMagnitude;
		if (distSqr < spleshAreaRadius) {
			pos.x +=   getOffset();
			pos.z -=   getOffset();
			if ((pos -lastInstParticlePos).sqrMagnitude < 80.0f) return;
			lastInstParticlePos = pos;
            GameObject splesh = (GameObject)Instantiate (spleshPrefab, pos + Random.insideUnitSphere, Quaternion.identity);
            splesh.transform.parent = particleContainer.transform;
			Destroy (splesh, 2);
		}
	}
	#endregion

	void InitComponents ()
	{
		meshMods.Add(new WaveStorm(this));
		meshMods.Add(new WaveRotation(this));
		meshMods.Add(new WaveSplesher(this));

		mf = GetComponent<MeshFilter> ();
		if (mf == null) {
			Debug.Log ("No mesh filter");
			return;
		}
	}

	void Start () {
		InvokeRepeating ("updOffset",0, 0.05f);
		InitComponents ();

	}

	void UpdateMesh ()
	{
		if (mf == null)
			return;
		mesh = mf.mesh;
		foreach(var meshMod in meshMods){
			mesh.vertices = meshMod.changeMesh(mesh);
		}
		mesh.RecalculateNormals ();
		mesh.MarkDynamic (); 
	}
 
	private float ampOffset = 0;
	private float ampOffsetDir = 0.01f;

	public float maxRotation = 1.6f;
	public float maxAmp = 0.6f;
	public float speedWaveDown = -0.003f;
	public float speedWaveUp = 0.002f;
	public bool automateWave = false;
	void LateUpdate() {
		UpdateMesh ();	
	}

	private void updOffset(){
		if (automateWave == false) return; 
		if (ampOffset > maxAmp)	ampOffsetDir = speedWaveDown;
		if (ampOffset < 0f)	ampOffsetDir = speedWaveUp; 
		ampOffset += ampOffsetDir;
		float newRot = ampOffset.Remap (0.0f, maxAmp, 0f, maxRotation);
		setRotationX(newRot-0.005f);
		setAmp(ampOffset);
	}


}
