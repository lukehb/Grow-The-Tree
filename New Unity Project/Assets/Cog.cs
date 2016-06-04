using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using AssemblyCSharp;

public class Cog : MonoBehaviour, IFallingItem  {
	
	private GameObject branch;
	private FSBody body;
	private bool collisionLock;
	// Use this for initialization
	void Start () {
		branch = (GameObject)Resources.Load("TreeBranch");
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.OnCollision += CollisionHandler;
	}
	
		
	// Update is called once per frame
	void Update () {
		
	}

	#region IFallingItem implementation
	public bool CollisionHandler (FSFixture fixA, FSFixture fixB, Contact contact)
	{
		//Debug.Log(fixB._collisionCategories);
		//Debug.Log(fixA._collisionCategories);
		if(fixB._collisionCategories == Category.Cat2 && !collisionLock){
			collisionLock = true;
			FVector2 normalVec;
			FixedArray2<FVector2> points;
			contact.GetWorldManifold(out normalVec, out points);
			normalVec = fixA.Body.LinearVelocity;
			
			//we can catch the case where the linear velocity is unknown
			if(normalVec.X == 0 && normalVec.Y == 0){
				normalVec = new FVector2(0,1);
			}else{
				normalVec.Normalize(); //normalise the vector
			}
			
			//normalVec.Normalize();
			FVector2 spawnPoint = FVector2.Add(points[0], FVector2.Multiply(normalVec, -2.5f));
			if(float.IsNaN(spawnPoint.X) || float.IsNaN(spawnPoint.Y)){
				Debug.Log("errorbaddie");
			}
			
			//Debug.Log("normal Vector "+normalVec.ToString());
			GameObject branchInstance = (GameObject)Instantiate(branch, new Vector3(spawnPoint.X ,spawnPoint.Y ,-0.5f), Quaternion.identity);
			float angle = 90 + Mathf.Atan2(normalVec.Y, normalVec.X) * 180 / Mathf.PI;
			//Debug.Log(angle);
			branchInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			//if(fixB.UserTag == "branch"){
			//	branchInstance.transform.parent = ((GameObject)fixB.UserData).transform;
			//} else {
			if(fixB.UserTag.Contains("branch")){
				//Debug.Log(((GameObject)fixB.UserData).transform.position.ToString());
				branchInstance.GetComponent<BranchScript>().SetParentBranch(fixB.UserTag);
			}
			var bTransform = GameObject.Find("BranchTransform");
			branchInstance.transform.parent = bTransform.transform;
			//}
//			var tree = GameObject.Find("Tree");
//			branchInstance.transform.parent = tree.transform;
//			var body = tree.GetComponent<FSBodyComponent>().PhysicsBody;
//			var fixture = body.CreateFixture(branchInstance.GetComponent<FSShapeComponent>().GetShape());
//			fixture.UserTag = "branch";
//			fixture._collisionCategories = Category.Cat2;
			Destroy(this.gameObject);
		} else if(fixB._collisionCategories == Category.Cat3 && !collisionLock){
			collisionLock = true;
			var world = GameObject.Find("World");
			world.GetComponent<GameLogic>().LifeLost();
			Destroy(this.gameObject);	
		}
		
		return true;
	}

	public void RemoveSelf ()
	{
		Destroy(this.gameObject);
	}
	#endregion
}
