using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using AssemblyCSharp;

public class Bomb : MonoBehaviour, IFallingItem {

	private bool collisionLock;
	private FSBody body;
	
	// Use this for initialization
	void Start () {
		collisionLock = false;
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
		var world = GameObject.Find("World").GetComponent<GameLogic>(); //get the gamelogic object, it will be needed
		
		if(fixB._collisionCategories == Category.Cat2 && !collisionLock){
			collisionLock = true;
			FVector2 normalVec;
			FixedArray2<FVector2> points;
			contact.GetWorldManifold(out normalVec, out points);
			normalVec = fixA.Body.LinearVelocity;
			if(fixB.UserTag.Contains("branch")){
				//how many branches got blown up
				int branchesBombed = GameObject.Find(fixB.UserTag).GetComponent<BranchScript>().BombBranch();
				//calculate the score
				long pointsEarned = ScoreCalculator.CalculateBranchesScore(branchesBombed);
				//update the score
				world.IncrementScore(pointsEarned);
			}	
			Destroy(this.gameObject);
		} else if(fixB._collisionCategories == Category.Cat3 && !collisionLock){
			//this means the bomb hit the ground
			collisionLock = true;
			//decrement a life
			world.LifeLost();
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
