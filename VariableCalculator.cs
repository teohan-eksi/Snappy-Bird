using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableCalculator : MonoBehaviour
{

	private bool rightTopFirstCall = true;
	private Vector3 rightTop;
	public Vector3 rightTopVector(){
		if(rightTopFirstCall){
			rightTopFirstCall = false;	
			int screenWidth = Screen.width;
			int screenHeight = Screen.height;
			Camera mCamera = Camera.main;
			//right top corner of the screen turned into world point as a Vector3
			rightTop = mCamera.ScreenToWorldPoint(
					new Vector3(
							screenWidth, 
							screenHeight, 
							mCamera.nearClipPlane
							)
					);
		
			return rightTop;
		}else{
			return rightTop;
		}
	}
}
