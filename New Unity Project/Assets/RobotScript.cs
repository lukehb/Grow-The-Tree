using UnityEngine;
using System.Collections;

public class RobotScript : MonoBehaviour {
	
	private float minX; //minimum x bound of background/world
	private float maxX; //maximum x bound of background/world
	private float xVelocity;
	private float yVelocity;
	private bool upStep;
	private float timeCount;
	
	// Use this for initialization
	void Start () {
		var background = (GameObject)GameObject.Find("SpriteBase");
		Bounds bgBounds = background.collider.bounds;
		minX = bgBounds.min.x+1f;
		maxX = bgBounds.max.x-1f;
		xVelocity = (Random.Range(0,2)==1) ? 1 : -1;
		upStep = false;
	}
	
	// Update is called once per frame
	void Update () {
		float frameVelocity = xVelocity*Time.deltaTime;
		timeCount += Time.deltaTime;
		if(timeCount > 0.25f){
			timeCount = 0;
			if(upStep){
				yVelocity = -0.05f;
			} else {
				yVelocity = 0.05f;
			}	
			upStep = !upStep;
		} else {
			yVelocity = 0;
		}
		if(transform.position.x - frameVelocity > maxX || transform.position.x - frameVelocity < minX){
			Debug.Log("velocity: " + frameVelocity);
			//Debug.Log("change direction");
			xVelocity = -xVelocity;
			frameVelocity = -frameVelocity;
			//transform.Translate (new Vector3(frameVelocity*1.2f, 0, 0));
		}
		transform.Translate (new Vector3(frameVelocity, yVelocity, 0));
	}
}
