using UnityEngine;
using System.Collections;

public class ShootFireball : MonoBehaviour {

	// Use this for initialization
	void Start () {
		fireball = (GameObject)Resources.Load("Fireball");
	}
	
	// Update is called once per frame
	GameObject fireball;
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			var ground = GameObject.Find("GroundPlane");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			bool hasHit = ground.collider.Raycast(ray, out hit, float.MaxValue);
			if(hasHit){
				Ability test = new Ability(gameObject);
				Vector3 fbLocation = transform.forward + new Vector3(transform.position.x, transform.position.y+(collider.bounds.size.y*0.75f), transform.position.z);
				GameObject fbInstance = (GameObject)Instantiate(fireball, fbLocation, Quaternion.identity);
				FireballBehaviour scriptInstance = (FireballBehaviour)fbInstance.GetComponent(typeof(FireballBehaviour));
				scriptInstance.InitTarget(hit.point);
			}
		}
	}
}
