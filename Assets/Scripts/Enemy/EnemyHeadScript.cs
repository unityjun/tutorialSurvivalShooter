using UnityEngine;
using System.Collections;

public class EnemyHeadScript : MonoBehaviour {

	public EnemyHealth enemyHealth;

	void OnTriggerEnter(Collider other) {
		
		//take damage
		if (other.gameObject.tag == "Bolt") {
			enemyHealth.TakeDamage (other.transform.position,80,"Head"); //Body
		}

	}
}
