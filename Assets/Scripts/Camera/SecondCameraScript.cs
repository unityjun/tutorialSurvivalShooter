using UnityEngine;
using System.Collections;

public class SecondCameraScript : MonoBehaviour {
	
	void Update () {
		//rotate camera
		RotateCamera ();
	}

	void RotateCamera(){

		Quaternion newQuaternion = Quaternion.Euler (0f, PlayerMovement.yEuler, 0f);
		transform.rotation = newQuaternion;

	}
}
