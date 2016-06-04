using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using FVector2 = Microsoft.Xna.Framework.FVector2;


public class PlayerController : MonoBehaviour {
	
	Quaternion storedRotation;
	GameObject background;
	private FSBody body;
	private FSBody dummy;
	private FSFixedMouseJoint mouseJoint;
	private FSWeldJoint testJoint;
	private float time;
	
		
	// Use this for initialization
	void Start () {
		background = GameObject.Find("SpriteBase");
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.IsBullet = true;
		//dummy = BodyFactory.CreateRectangle(body.World, 1,1,1);
		//testJoint = JointFactory.CreateWeldJoint(body, dummy, body.Position);
		//body.Mass = 10000;
		//body.LinearDamping = 0.001f;
		body.FixedRotation = true;
		//body.GravityScale = 0f;
		mouseJoint = JointFactory.CreateFixedMouseJoint(body.World, body, body.Position);
		mouseJoint.DampingRatio = 0;
		mouseJoint.Frequency = 100;
		mouseJoint.MaxForce = (10000.0f * body.Mass);
		mouseJoint.CollideConnected = true;
		
		time = Time.time;
		if(body == null){
			print("null");
		}
		//storedRotation = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z,transform.rotation.w);
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		bool hasHit = background.collider.Raycast(ray, out hit, float.MaxValue);
		if(hasHit){
			mouseJoint.WorldAnchorB = new FVector2( hit.point.x, hit.point.y);
//			FVector2 vector =  new FVector2(hit.point.x - body.Position.X, hit.point.y - body.Position.Y);
//			float mFactor = 0.5f;
			//if(vector.Length() > mFactor){
			//	vector.Normalize();
			//	vector = FVector2.Multiply(vector, 30f);
			//} else if(vector.Length() > 0.1f){
			//	vector = FVector2.Multiply(vector, 0.025f/(Time.time - time));
			//} else {
			//	vector = FVector2.Zero;
			//}
			//body.LinearVelocity = vector;
			//body.SetTransform(new FVector2(hit.point.x, hit.point.y), 5*Mathf.PI/4);
		}
		time = Time.time;
		//transform.position = Input.mousePosition;
		//transform.rotation = storedRotation;
	}
}
