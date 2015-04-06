using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static float yEuler;

	public float speed = 6f;

	private float deltaAngle = 0f;

	private float yPlayerAngel = 0f;
	private float w2 = Screen.width / 2;

	Vector3		 movement;
	Animator	 anim;
	Rigidbody	 playerRigitbody;
	int			 floorMask;

	float		 camRayLenth = 100f;

	/// <summary>
	/// Обработчики событий объекта
	/// </summary>

	void Awake(){

		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();

		playerRigitbody = GetComponent<Rigidbody> ();
	}

	void Update(){

	}

	void FixedUpdate(){

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		bool j = Input.GetButtonDown ("Jump");

		if (CameraManager.numOfCam == 2) {
			//rotate to cursor
			RotatePlayer ();
		}
		else if(CameraManager.numOfCam == 3){
			if(transform.rotation.eulerAngles.y !=0f){
				//transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
			}
		}

		Animating (h, v);
		Move (h , v);
		Jump (j);
		Turning ();

	}

	/// <summary>
	/// Основные процедуры и функции
	/// </summary>

	void RotatePlayer (){
		Vector3 mousePos = Input.mousePosition;
		if (mousePos.x > w2) {
			yPlayerAngel = (mousePos.x - w2) / w2 * 90;

		} else if (mousePos.x < w2) {
			yPlayerAngel = (w2 - mousePos.x) / w2 * -90;

		};

		yPlayerAngel = yPlayerAngel + PlayerMovement.yEuler;
		Quaternion target = Quaternion.Euler (0f, yPlayerAngel, 0f);
		playerRigitbody.rotation = Quaternion.Slerp (playerRigitbody.rotation, target, Time.deltaTime * 10);
	}

	void Move(float h, float v){

		//for third person camera
		if (CameraManager.numOfCam == 3) {
			movement.Set (h, 0f, v);
		} 
		//for second person camera
		else {
			Vector3 movementForward = CameraManager.thatCameraTransform.transform.forward;

			/*
			Vector3 movementRight = CameraManager.thatCameraTransform.transform.right;
			movement = (v * movementForward + h * movementRight);
			*/

			movement = (v * movementForward);
		}
		//normalization
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigitbody.MovePosition (transform.position + movement);
	}

	void Jump(bool j){

		//jump
		if(j){
			playerRigitbody.velocity = transform.up * 50;
		}

	}

	void Turning(){

		//for third person camera
		if (CameraManager.numOfCam == 3) {
			
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit floorHit;

			if (Physics.Raycast (camRay, out floorHit, camRayLenth, floorMask)) {

				Vector3 playerToMouse = floorHit.point - transform.position;
				playerToMouse.y = 0f;

				Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

				playerRigitbody.MoveRotation (newRotation);
			}
		} 
		//for second & first person camera
		else {

			float delta = 0f;

			//rotate to key A & D
			//deltaAngle = deltaAngle + Input.GetAxis("Mouse X")/100;
			if(Input.GetKey(KeyCode.A)){
				delta = delta -0.01f;
			}
			if(Input.GetKey(KeyCode.D)){
				delta = delta + 0.01f;
			}

			deltaAngle = deltaAngle + delta;

			yEuler = 180 * deltaAngle;

			//
			Quaternion newRotation = Quaternion.Euler (0f,yEuler,0f);
			playerRigitbody.rotation = Quaternion.Slerp (playerRigitbody.rotation,newRotation,Time.deltaTime * 10);
		}
	}

	void Animating(float h, float v){

		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking",walking);

	}

}
