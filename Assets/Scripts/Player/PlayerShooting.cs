using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	public GameObject gunBarrelGameObject;

    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 1000f;

	public GameObject bolt;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

	Transform gunBarrelTransform;

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");

		gunBarrelTransform = gunBarrelGameObject.transform;

		gunParticles = gunBarrelGameObject.GetComponent<ParticleSystem> ();
		gunAudio = gunBarrelGameObject.GetComponent<AudioSource> ();
		gunLight = gunBarrelGameObject.GetComponent<Light> ();
    }

	void Update(){
		Shooting ();
	}

	void FixedUpdate(){

	}

    public void DisableEffects ()
    {
        gunLight.enabled = false;
    }

	void Shooting(){

		timer += Time.deltaTime;
		
		if(Input.GetButton ("Fire1") 
		   && timer >= timeBetweenBullets 
		   && Time.timeScale != 0)
		{
			Shoot ();
		}
		
		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}

	}

    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

		shootRay = Camera.main.ScreenPointToRay (Input.mousePosition);

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
			//{
			Vector3 dir = shootHit.point - gunBarrelTransform.position;
			shootigBolt(dir);

			//}

        }
        else
        {
			//gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
			shootigBolt(shootRay.direction);
        }

    }

	void shootigBolt(Vector3 direction){

		Instantiate(bolt
		            ,gunBarrelTransform.position
                       ,Quaternion.LookRotation(direction)
                   );

	}
}
