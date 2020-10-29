using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicManover : MonoBehaviour {

	public float dodge;
	public float smoothing;
	public float tilt;
	public Vector2 startWaits;
	public Vector2 manoverTime;
	public Vector2 manoverWait;
	public Boundry boundry;

	private float targetManover;
	private Rigidbody rb;
	private float currentSpeed;

	void Start () {
		
		rb = GetComponent<Rigidbody> ();
		currentSpeed = rb.velocity.z;
		StartCoroutine (Evade ());
	}
	
	IEnumerator Evade(){
		yield return new WaitForSeconds(Random.Range(startWaits.x,startWaits.y));

		while (true) {
		
			targetManover = Random.Range (1,dodge)*-Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds (Random.Range(manoverTime.x,manoverTime.y));
			targetManover = 0;
			yield return new WaitForSeconds (Random.Range(manoverWait.x,manoverWait.y));
		}
	}

	void FixedUpdate () {
		float newMaover = Mathf.MoveTowards (rb.velocity.x,targetManover,Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newMaover,0.0f,currentSpeed);
		rb.position = new Vector3
		(
				Mathf.Clamp(rb.position.x,boundry.minX,boundry.maxX),
				0.0f,
				Mathf.Clamp(rb.position.z,boundry.minZ,boundry.maxZ)
		);
		rb.rotation = Quaternion.Euler (0.0f,0.0f,rb.velocity.x*-tilt);
	}
}
