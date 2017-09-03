using System; 
using UnityEngine;
using System.Collections;
public interface MeshDeformInterface 
{
	Vector3[] changeMesh(Mesh mesh);
} 
public interface MeshSinDeformController 
{
	float getWaveLength();
	float getFreq();
	float getAmp();
	Vector3 getWaveStartPosition();
} 
public interface MeshRotateController
{
	float getOffset();
} 
public interface SpleshController
{
	void createSplesh(Vector3 pos);
} 