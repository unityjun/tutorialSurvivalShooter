using UnityEngine;
using System.Collections;

public class CameraFolow : MonoBehaviour {

	public Transform targer;
	public float smoothing = 5f;

	Vector3 offset;


	void Start(){

		offset = transform.position - targer.position;

	}

	void FixedUpdate(){

		Vector3 targetCamPos = targer.position + offset;
		transform.position = Vector3.Lerp (transform.position,targetCamPos, smoothing* Time.deltaTime);


	}




}
