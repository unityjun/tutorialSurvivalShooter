using UnityEngine;
using System.Collections;

public class BoltScript : MonoBehaviour {

	public float speed = 5.0f;

	private Rigidbody rigidbody3D;

	void Awake(){
		rigidbody3D = GetComponent<Rigidbody> ();
	}

	void Start(){

		rigidbody3D.velocity = transform.forward * speed;

	}
	
	void FixedUpdate(){

	}

	void OnTriggerEnter(Collider other) {

		//if player or Bolt -> return
		if (other.gameObject.tag == "Player"
		    || other.gameObject.tag == "Bolt"
		    || other.gameObject.tag == "PlayerNav") {
			return;
		}
		//take damage

		//destroy
		Destroy (this.gameObject);
	}
}
