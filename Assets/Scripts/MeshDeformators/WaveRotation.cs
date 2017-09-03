using System; 
using UnityEngine;
using System.Collections;
public class WaveRotation : MeshDeformInterface
{
	MeshRotateController ctrlr; 
	#region MeshDeformInterface implementation
	public Vector3[] changeMesh (Mesh mesh)
	{ 
		Vector3[] vertices = mesh.vertices;
		float max = 0;
		float min = 0;
		foreach (var v in vertices) {
			if (v.y > max) max = v.y;
			if (v.y < min) min = v.y; 
		}

		for (int i = 0; i < vertices.Length; i++) {
			Vector3 v = vertices [i];  
			float surfOffset =  v.y.Remap(min, max, 0, 1);
			vertices [i].x += surfOffset * ctrlr.getOffset();
			vertices [i].z -= surfOffset * ctrlr.getOffset();
		}
		return vertices;
	}
	#endregion

	public WaveRotation(MeshRotateController ctrlr){
		this.ctrlr = ctrlr;
	}
} 

