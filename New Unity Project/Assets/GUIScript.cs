using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	
	bool GUIActive;
	bool paused;
	bool gameOver;
	string nameField = "";
	string nameBox = "Input Name and Press Enter";
	GUITexture overlay;	
	Vector3 offset;
	// Use this for initialization
	void Start () {
		gameOver = false;
		GUIActive = true;
		paused = false;
		offset = new Vector3(0, 5, 0); 
		overlay = GameObject.Find("Overlay").GetComponent<GUITexture>();
		Time.timeScale = 0;
	}
	
	public void ActivateOverlay(){
		overlay.transform.Translate(-offset);
	}
	
	public void ActivateGUI(){
		GUIActive = true;
	}
	
	public void ActivateGameOver(){
		gameOver = true;
	}
	
	private void OnGUI(){
		Event e = Event.current;	
		if(paused){
			GUI.Label(new Rect((Screen.width/2)-40, (Screen.height/2)-30, 100, 20), "Game Paused");
		}
		if(gameOver){
			GUI.Label(new Rect((Screen.width/2)-40, (Screen.height/2)-40, 100, 20), "Game Over!");
			GUI.Label(new Rect((Screen.width/2)-115, (Screen.height/2)-20, 250, 20), "Press 'N' to play again with a new user");
			GUI.Label(new Rect((Screen.width/2)-70, (Screen.height/2), 140, 20), "or any key to play again");
		}
		if(gameOver){
			if(e.keyCode == KeyCode.N){
				gameOver = false;
				ActivateGUI();
				nameField = "";
			}
			if(e.isKey){
				GameObject.Find("World").GetComponent<GameLogic>().StartGame();
				GUIActive = false;
				gameOver = false;
				overlay.transform.Translate(offset);
			}
		}
		if (e.keyCode == KeyCode.Return && GUIActive && nameField != ""){
			GUIActive = false;
			Time.timeScale = 1;
			GameObject.Find("World").GetComponent<GameLogic>().SetPlayerName(nameField);
			GameObject.Find("World").GetComponent<GameLogic>().StartGame();
			overlay.transform.Translate(offset);
		} else if(GUIActive){
			GUI.Label(new Rect((Screen.width/2)-80, (Screen.height/2), 180, 20), nameBox);
			nameField = GUI.TextField(new Rect((Screen.width/2)-50, (Screen.height/2)-30, 100, 20), nameField);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!GUIActive && Input.GetKeyDown(KeyCode.Escape)){
			if(paused){
				Time.timeScale = 1;
				overlay.transform.Translate(offset);
			} else {
				Time.timeScale = 0;
				overlay.transform.Translate(-offset);
			}
			paused = !paused;
		}
	}
}
