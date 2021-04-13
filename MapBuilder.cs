using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{

	void Start(){
	
		StartCoroutine(buildRightSide());
	}

	IEnumerator buildRightSide(){
		yield return null; //fn 4

		gameObject.AddComponent<RightSide>();
//		Debug.Log("RightSide from MapBuilder "+ Time.frameCount.ToString());
	
		gameObject.AddComponent<LeftSide>();
//		Debug.Log("LeftSide from MapBuilder "+ Time.frameCount.ToString());

		yield return null;
		
		gameObject.AddComponent<VariableCalculator>();
		gameObject.AddComponent<ObjectCreator>();
		
	}
}
