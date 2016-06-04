using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;

public class GameLogic : MonoBehaviour {
	
	int lives;
	int nameCounter;
	string playerName;
	GUIScript guiScript;
	Spawner spawnerScript;
	GUIText livesText;
	AudioSource bgMusic;
	long score; //the players score
	
	public void LifeLost(){
		lives--;
		Debug.Log ("Current Lives: " + lives);
		livesText.text = "Lives: " + lives;
		spawnerScript.eliminateRobot();
		if(lives <= 0){
			GameOver();		
		}
	}
	
	private void GameOver(){
		bgMusic.Stop();
		Debug.Log("Game Over");
		Time.timeScale = 0;
		guiScript.ActivateOverlay();
		guiScript.ActivateGameOver();
		submitScore(playerName, this.score);
	}
	
	public void SetPlayerName(string player){
		playerName = player;
	}
	
	public string GetUniqueBranchName(){
		nameCounter++;
		return ("branch"+nameCounter.ToString());
	}
	
	// Use this for initialization
	void Start () {
		livesText = GameObject.Find("LivesText").GetComponent<GUIText>();
		livesText.material.color = Color.black;
		bgMusic = (AudioSource)GameObject.Find("BGMusicObject").GetComponent<AudioSource>();
		guiScript = GameObject.Find("GUIObject").GetComponent<GUIScript>();
		spawnerScript = GameObject.Find("World").GetComponent<Spawner>();
		nameCounter = 0;
		lives = 3; 
		score = 0L; 
		GameObject.Find("ScoreText").GetComponent<GUIText>().material.color = Color.black;
	}
	
	public void StartGame(){
		bgMusic.Play();
		lives = 3;
		score = 0L; //reset score to zero
		livesText.text = "Lives: " + lives;
		Time.timeScale = 1;	
		this.GetComponent<Spawner>().resetDifficulty();
		try{
			//remove branches
			var branches = GameObject.FindGameObjectsWithTag("Branch");			
			foreach(var branch in branches){
				branch.GetComponent<BranchScript>().RemoveBranch();
			}
			//remove bombs 
			var bombs = GameObject.FindGameObjectsWithTag("Bomb");			
			foreach(var bomb in bombs){
				bomb.GetComponent<Bomb>().RemoveSelf();
			}
			//remove cogs
			var cogs = GameObject.FindGameObjectsWithTag("Cog");			
			foreach(var cog in cogs){
				cog.GetComponent<Cog>().RemoveSelf();
			}
			//
		}catch(Exception e){
			//error removing something
		}
		
		
		
		
		GameObject.Find("ScoreText").GetComponent<GUIText>().text = "Score: 0";
	}
	
	//Increments the score based on whatever calculation are done elsewhere
	public void IncrementScore(long scoreToAdd){
		try{
			this.score = checked(score + scoreToAdd);
		}catch(System.OverflowException e){
			//the score reached its max limit
			this.score = long.MaxValue; //so they must have reached the highscore
		}
		
		//update the gui
		GUIText scoreText = GameObject.Find("ScoreText").GetComponent<GUIText>();
		scoreText.text = "Score: " + score;
	}
	
	private void submitScore(string name, long submittedScore){
		try{
			string secretKey = "yoursecret"; //the server usually has the same key, if the hashes match, it was a real request
			string score = submittedScore.ToString();
			string hash = UtilityMethods.Md5Sum(name + score + secretKey);
			Debug.Log ("hash: " + hash);
			string addScoreURL = "http://growyourtree.herokuapp.com/submit_score.php"; //your server with highscore POST here
			
			//This connects to a server side php script that will add the name and score to a MySQL DB.
        	// Supply it with a string representing the players name and the players score.

	        			
			var form = new WWWForm();
			form.AddField("name", name);
			form.AddField("score", score); //score as an int
			form.AddField("hash", hash);
			var w = new WWW(addScoreURL, form);
			StartCoroutine(WaitForRequest(w));
			if (w != null && w.error != null && w.error.Length >0){
	 			Debug.Log(w.error);
			}
			else{
	 			//do whatever else here
				Debug.Log(w);
	 			Debug.Log("Score was posted: " + w.isDone);
			}
		}catch(Exception e){
			Debug.Log("something went wrong with score posting: " + e.Message);
		}
	}
	
	private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
 
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
        } else {
            Debug.Log("WWW Error: "+ www.error);
        }    
    } 
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.R)){
			//if they press "R", reset the game
			this.StartGame();
			
		}
		
	}
}
