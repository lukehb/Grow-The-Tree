using UnityEngine;
using System.Collections;

public class ClickMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	float speed = 10.0f;
	Vector3 target = Vector3.zero;
	bool hasTarget = false;
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			//ground = GetComponent("GroundPlane");
			var ground = GameObject.Find("GroundPlane");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			bool hasHit = ground.collider.Raycast(ray, out hit, float.MaxValue);
			if(hasHit){
				target = hit.point;
				hasTarget = true;
			}
			//var test = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			//test.transform.Translate(target);
			//test.transform.localScale.Scale(new Vector3(0.1f,0.1f,0.1f));
		}
		if(hasTarget){
			transform.LookAt(target);
			var distance = Vector3.Distance(target, transform.position);
			if(distance <= speed * Time.deltaTime){
				transform.Translate(Vector3.forward * distance, Space.Self);
				hasTarget = false;
			} else{
				transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
			}
		}
	}
}
