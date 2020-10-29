using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundry{
	
	public float minX,maxX,minZ,maxZ;
}

public class PlayerController : MonoBehaviour {


	public float speed;
	public float tilt;
	public Boundry boundry;
	public GameObject shot;
	public Transform[] shotSpawns;
	public float fireRate;
	public TouchPad touchPad;
	public FirePad firePad;

	private Rigidbody rb;
	private float nextFire;
	private AudioSource audioSource;
	private Quaternion calibrationQuaternion;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	void Update(){
		if (/*firePad.CanFire()*/Input.GetButtonDown("Fire1") /*Input.GetMouseButtonDown (0)*/ && Time.time > nextFire) {

			nextFire = Time.time + fireRate;
			foreach (var shotSpawn in shotSpawns){
			   Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				audioSource.Play ();
			}
		}
	}

	void FixedUpdate () {
		//float moveHoriznotal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");

		//Vector3 acceleration = Input.acceleration;

		//Vector3 movement = new Vector3 (moveHoriznotal,0.0f,moveVertical);	
		//Vector3 movement = new Vector3 (acceleration.x,0.0f,acceleration.y);

		Vector2 direction = touchPad.GetDirection ();

		Vector3 movement = new Vector3 (direction.x,0.0f,direction.y);

		rb.velocity = movement * speed;

		rb.position = new Vector3 
		(
				Mathf.Clamp(rb.position.x,boundry.minX,boundry.maxX),
				0.0f,
				Mathf.Clamp(rb.position.z,boundry.minZ,boundry.maxZ)
		);
		rb.rotation = Quaternion.Euler (0.0f,0.0f,rb.velocity.x*-tilt);
	}

	void CalibrateAccelerometer(){
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3(0.0f,0.0f,-1.0f),accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration(Vector3 accelration){
		Vector3 fixedAcceleration = calibrationQuaternion * accelration;
		return fixedAcceleration;
	}
}
