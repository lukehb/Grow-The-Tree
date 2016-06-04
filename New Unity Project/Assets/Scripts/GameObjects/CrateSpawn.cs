using UnityEngine;
using System.Collections;

public class CrateSpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	 crate = (GameObject)Resources.Load("Crate");
	}
	
	// Update is called once per frame
	GameObject crate;
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(2)){
			var ground = GameObject.Find("GroundPlane");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			bool hasHit = ground.collider.Raycast(ray, out hit, float.MaxValue);
			if(hasHit){
				GameObject fbInstance = (GameObject)Instantiate(crate, hit.point, Quaternion.identity);
			}
		}
	}
}
