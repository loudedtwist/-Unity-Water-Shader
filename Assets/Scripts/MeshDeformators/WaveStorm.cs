using System; 
using UnityEngine;
using System.Collections;
public class WaveStorm : MeshDeformInterface
{ 
	MeshSinDeformController ctrlr; 
	private WaveStorm(){
	}
	public WaveStorm(MeshSinDeformController ctrlr){
		this.ctrlr = ctrlr;
	}
	public Vector3[] changeMesh(Mesh mesh) 
	{ 
		Vector3[] vertices = mesh.vertices;
		for (int i = 0; i < vertices.Length; i++) {
			Vector3 v = vertices [i];
			float dist = Vector3.Distance (v, ctrlr.getWaveStartPosition());
			dist = (dist % ctrlr.getWaveLength()) / ctrlr.getWaveLength();
			v.y +=ctrlr.getAmp() * Mathf.Sin (Time.time * Mathf.PI * 2.0f * ctrlr.getFreq() + (Mathf.PI * 2.0f * dist)); 
			vertices [i] = v;
		}
		return vertices;
	}  
} 
