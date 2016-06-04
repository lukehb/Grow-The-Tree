using UnityEngine;
using System.Collections;
using Box2DX;

public class Box2DTest : MonoBehaviour {
	
	Box2DX.Dynamics.World myWorld;
	// Use this for initialization
	void Start () {
		myWorld = new Box2DX.Dynamics.World(new Box2DX.Collision.AABB(), new Box2DX.Common.Vec2(0f,5f), false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
