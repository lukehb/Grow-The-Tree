using UnityEngine;
using System.Collections;

public class RTSCameraBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	float speed = 10.0f;
	void Update () {
    	Vector3 movement = Vector3.zero; 
    	if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height-1)
        	movement.z++;
    	if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= 0)
        	movement.z--;
    	if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= 0)
        	movement.x--;
    	if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width-1)
    	    movement.x++;
 
	    transform.Translate(movement * speed * Time.deltaTime, Space.World);

	}
	
	
}
