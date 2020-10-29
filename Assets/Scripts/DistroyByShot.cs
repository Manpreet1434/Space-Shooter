using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistroyByShot : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;


	void Start(){

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		if (gameControllerObject == null) {
			Debug.Log ("Cant find Game Object");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundry" || other.tag == "Enemy"|| other.tag == "Bosts") {
			return;
		}

		if (explosion != null) {
			Instantiate (explosion, transform.position, transform.rotation);
		}
		if (other.tag == "Player") {
			Instantiate (playerExplosion, transform.position, transform.rotation);
			gameController.GameOver ();
		}

		Destroy (other.gameObject);
		Destroy (gameObject);
		gameController.addScore (scoreValue);
	}
}
