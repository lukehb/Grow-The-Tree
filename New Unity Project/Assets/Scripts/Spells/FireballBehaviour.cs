using UnityEngine;
using System.Collections;

public class FireballBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void InitTarget(Vector3 target){
		Vector3 modtarget = new Vector3(target.x, transform.position.y, target.z);
		transform.LookAt(modtarget);
		hasTarget = true;		
		startTime = Time.time;
	}
	
	// Update is called once per frame
	float TTL = 10;
	float startTime = float.MaxValue;
	float speed = 10.0f;
	bool hasTarget= false;
	void Update () {
		if((Time.time - startTime) > TTL){
			Destroy(gameObject);
		}
		if(hasTarget){
			transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
		}
	}
}
