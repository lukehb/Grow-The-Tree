using UnityEngine;
using System.Collections;

public class PointTarget : Target {
	
	private Vector3 target;
	
	public PointTarget(){
		((UtilityMethods)GameObject.Find("Utilities").GetComponent(typeof(UtilityMethods))).GetWorldVectorFromMouse(out target);
	}
	
	public Vector3 ResolveTargetLocation(){
		return target;		
	}
}
