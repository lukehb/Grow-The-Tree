using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class BranchScript : MonoBehaviour {
	
	Material material;
	FSFixture fixture;
	ArrayList children;
	string parent ="";
	string MATERIAL1 = "BranchMat1";
	string MATERIAL2 = "BranchMat2";
	string MATERIAL3 = "BranchMat3";
	
	//Gets the number of branches blown up by the bomb and removes them
	public int BombBranch(){
		int nBranches = 1; //this is the current branch itself
		if(children != null){
			while(children.Count > 0){
				string childBranch = (string)children[0];
				Debug.Log(childBranch);
				nBranches += GameObject.Find(childBranch).GetComponent<BranchScript>().BombBranch();
			}
		}
		RemoveBranch();
		return nBranches;
	}
	
	public void SetParentBranch(string branch){
		parent = branch;
	}
	
	public void ChildDestroyed(string branch){
		children.Remove(branch);
	}
	
	public void AddBranch(string branch){
		if(children == null){
			children = new ArrayList();
		}
		Debug.Log("received branch: " + branch);
		Debug.Log("Array: "+ children.ToString());
		children.Add(branch);
	}
	
	public void RemoveBranch(){
		if(parent != ""){
			GameObject parentObj = GameObject.Find(parent);
			if(parentObj != null){
				parentObj.GetComponent<BranchScript>().ChildDestroyed(name);
			}
		}
		var body = GameObject.Find("Tree").GetComponent<FSBodyComponent>().PhysicsBody;
		body.DestroyFixture(fixture);
		Destroy(this.gameObject);
	}
	
	// Use this for initialization
	void Start () {
		int matNum = Random.Range(1,4);
		switch(matNum){
			case 1:
				material = (Material)Resources.Load(MATERIAL1);
				break;
			case 2:
				material = (Material)Resources.Load(MATERIAL2);
				break;
			default:
				material = (Material)Resources.Load(MATERIAL3);
				break;
		}
		renderer.material = material;
		//var parentVec = transform.parent.position;
		//this.transform.Translate(new Vector3(-parentVec.x, -parentVec.y, 0));
		//this.transform.rotation = (Quaternion.Inverse(transform.parent.transform.rotation));
		var tree = GameObject.Find("Tree");
//		transform.parent = tree.transform;
		var body = tree.GetComponent<FSBodyComponent>().PhysicsBody;
		fixture = body.CreateFixture(GetComponent<FSShapeComponent>().GetShape());
		fixture.UserTag = GameObject.Find("World").GetComponent<GameLogic>().GetUniqueBranchName();
		name = fixture.UserTag;
		fixture._collisionCategories = Category.Cat2;
		if(parent != ""){
			GameObject.Find(parent).GetComponent<BranchScript>().AddBranch(fixture.UserTag);
		}
	}
	
	// Update is called once per frame
	void Update () {
//		if(children != null){
//			Debug.Log(children.Count);
//		}
	}
}
