using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	GameObject background;
	GameObject cog;
	GameObject bomb;
	GameObject robotSprite;
	ArrayList robotsArray;
	private float timerTime;
	private float timerInterval;
	private float timerDifficultyCurve;
	private int difficultyCounter;
	private float minX; //minimum x bound of background/world
	private float maxX; //maximum x bound of background/world	
	private float maxY; //maximum y bound of background/world
	
	// Use this for initialization
	void Start () {
		robotSprite = (GameObject)Resources.Load("RobotObject");
		robotsArray = new ArrayList();
		background = (GameObject)GameObject.Find("SpriteBase");
		cog = (GameObject)Resources.Load("CogObject");
		bomb = (GameObject)Resources.Load("Bomb");
		timerTime = Time.time;
		timerInterval = 4;
		timerDifficultyCurve = 0.85f;
		difficultyCounter = 0;
		Bounds bgBounds = background.collider.bounds;
		minX = bgBounds.min.x+1f;
		maxX = bgBounds.max.x-1f;
		maxY = bgBounds.max.y;
	}
	
	public void eliminateRobot(){
		try{
		GameObject robotToEliminate = (GameObject)robotsArray[0];
		robotsArray.RemoveAt(0);
		Destroy(robotToEliminate);
		} catch(System.Exception e){
		}
	}
	
	public void resetDifficulty(){
		difficultyCounter = 0;
		timerInterval = 4;
		//clear it incase we did reset.... not actual lose
		while(robotsArray.Count > 0){
			eliminateRobot();
		}
		for (int i = 0; i < 3; i++) {
			robotsArray.Add(Instantiate(robotSprite, new Vector3(Random.Range(minX+0.5f,maxX-0.5f), 4f, -0.5f), Quaternion.identity));
		}
	}
	
	float fastestDifficulty = 0.85f;
	// Update is called once per frame
	void Update () {
		if(Time.time - timerTime >= timerInterval){
			float x = Random.Range(minX, maxX);
			if(Random.Range(0f,1f) > 0.7f){
				Instantiate(bomb, new Vector3(x, maxY, -0.5f), Quaternion.identity);
			} else {
				Instantiate(cog, new Vector3(x, maxY, -0.5f), Quaternion.identity);
			}
			timerTime = Time.time;
			difficultyCounter++;
			if(difficultyCounter >=10){
				difficultyCounter = 0;
				if(timerInterval > fastestDifficulty)
				{
					timerInterval *= timerDifficultyCurve;
				}
				
			}
		}
	}
}
