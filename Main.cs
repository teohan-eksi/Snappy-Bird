using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Main : MonoBehaviour
{
	private Text uiMsg;

	private GameObject mapBuilder;
	private GameObject quitObject;

	// Start is called before the first frame update
    void Start()
    {

		StartCoroutine(mainRoutine());
			
		//uiMsg.text = "Loading";
		//do some loading animation with another script
		//until the map is ready.
	}

    // Update is called once per frame
    void Update()
	{

	}

	IEnumerator mainRoutine(){
		yield return null;//skips the firts frame. fn 2

/*		uiMsg = GameObject.Find("Text").GetComponent<Text>();
		uiMsg.text = "start game";
		Debug.Log("UI message from Main " + Time.frameCount.ToString());
		*/

		yield return null;//fn 3

		mapBuilder = new GameObject("MapBuilder");
		mapBuilder.AddComponent<MapBuilder>();
//		Debug.Log("MapBuilder from Main " + Time.frameCount.ToString());

		yield return new WaitForSeconds(0.5f); // fn 30
		
		quitObject = new GameObject("QuitObject");
		quitObject.AddComponent<QuitScript>();
//		Debug.Log("QuitScript from Main " + Time.frameCount.ToString());
	}

}
