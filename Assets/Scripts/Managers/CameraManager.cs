using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public Transform secondPersonCamera;
	public Transform thirdPresonCamera;

	public PlayerHealth playerHealth;

	public static int numOfCam = 3;
	public static Transform thatCameraTransform;
	
	void Awake(){

		SwitchSecondPersonCamera ();

	}
	
	// Update is called once per frame
	void Update () {

		//change camera
		if (Input.GetKeyDown (KeyCode.I)) {
			ChangeCamera();
		}

		if (playerHealth.currentHealth <= 0)
		{
			SwitchThirdPersonCamera ();
		}
	}

	void ChangeCamera(){

		numOfCam++;
		numOfCam = numOfCam >3 ?2:numOfCam;
		
		if(numOfCam == 3){
			SwitchThirdPersonCamera();
		}
		else if (numOfCam == 2){
			SwitchSecondPersonCamera();
		}

	}
	
	void SwitchThirdPersonCamera(){

		thatCameraTransform = thirdPresonCamera;
		numOfCam = 3;

		//2
		Camera cam2 = secondPersonCamera.gameObject.GetComponent<Camera>();
		cam2.enabled = false;
		AudioListener al2 = secondPersonCamera.gameObject.GetComponent<AudioListener>();
		al2.enabled = false;
		//3
		Camera cam3 = thirdPresonCamera.gameObject.GetComponent<Camera>();
		cam3.enabled = true;
		AudioListener al3 = thirdPresonCamera.gameObject.GetComponent<AudioListener>();
		al3.enabled = true;

	}

	void SwitchSecondPersonCamera(){

		thatCameraTransform = secondPersonCamera;
		numOfCam = 2;

		//2
		Camera cam2 = secondPersonCamera.gameObject.GetComponent<Camera>();
		AudioListener al2 = secondPersonCamera.gameObject.GetComponent<AudioListener>();
		cam2.enabled = true;
		al2.enabled = true;
		//3
		Camera cam3 = thirdPresonCamera.gameObject.GetComponent<Camera>();
		AudioListener al3 = thirdPresonCamera.gameObject.GetComponent<AudioListener>();
		cam3.enabled = false;
		al3.enabled = false;
	}
	
}
