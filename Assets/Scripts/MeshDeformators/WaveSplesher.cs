using System; 
using UnityEngine;
using System.Collections;
public class WaveSplesher : MeshDeformInterface
{
	SpleshController ctrlr; 


	public WaveSplesher(SpleshController ctrlr){
		this.ctrlr = ctrlr;
	}

	#region MeshDeformInterface implementation 
	public Vector3[] changeMesh (Mesh mesh)
	{ 
		Vector3[] vertices = mesh.vertices;
		float max = 0; 
		foreach (var v in vertices) {
			if (v.y > max) max = v.y; 
		}
		if(max < 0.4f ) return  mesh.vertices;

		foreach (var v in vertices) {
			if (v.y > max -0.05 && v.y < max + 0.05) {
				Vector3 newV = v;
				newV.y -= v.y / 2;
				ctrlr.createSplesh (newV);
			}

		}
		return mesh.vertices;
	} 
	#endregion
} 
