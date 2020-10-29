using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValue;
	public int hazardCount;
	public float startWait;
	public float spawnWait;
	public float waveWait;

	public Text scoreText;
	//public Text RestartText;
	public Text GameOverText;
	public GameObject restartButton;

	private bool gameOver;
	private bool restart;

	private int score;


	void Start () {
		score = 0;
		StartCoroutine (SpawnWave ());
		gameOver = false;
		restart = false;
		restartButton.SetActive (false);
		scoreText.text = "Score : " + score;
		//RestartText.text = "";
		GameOverText.text = "";
		//scoreText = GetComponent<GUIText> ();
	}

	/*void Update(){
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene() .buildIndex); 
			}
		}
	}*/

	IEnumerator SpawnWave(){
		yield return new WaitForSeconds (startWait);
		while(true){
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards[Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-6, 6), spawnValue.y, spawnValue.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				//RestartText.text = "Press 'R' to restart Game ";
				restartButton.SetActive (true);
				restart = true;
				break;
			}
		}
	}


	
	public void addScore(int newScore){
		score += newScore;
		UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = "Score : " + score;
	}

	public void GameOver(){
		GameOverText.text = "Game Over";
		gameOver = true;
	}

	public void RestartGame(){
	
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
