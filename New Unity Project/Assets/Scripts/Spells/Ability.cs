using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {
	
	public GameObject source; //owner of the spell
	public string name = "Ability";
	public Target target;
	public float lifespan = -1f;
	public GameObject graphic;
	
	public Ability(GameObject source){
		fireball = (GameObject)Resources.Load("Fireball");
		this.source = source;
		Vector3 fbLocation = this.source.transform.forward + new Vector3(this.source.transform.position.x, this.source.transform.position.y+(this.source.collider.bounds.size.y*0.75f), this.source.transform.position.z);
		graphic = (GameObject)Instantiate(fireball, fbLocation, Quaternion.identity);
		target = new PointTarget();
	}
	
	public Ability(GameObject source, GameObject graphic, string name, float lifespan){
		this.source = source;
		this.name = name;
		this.graphic = (GameObject)Instantiate(graphic, Vector3.zero, Quaternion.identity);
		this.lifespan = lifespan;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	GameObject fireball;
	
	// Update is called once per frame
	void Update () {
	
	}
}
