using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSnapper : MonoBehaviour
{
	private GameObject Bird;
	private Bird birdScript;

    // Start is called before the first frame update
    void Start()
    {
		Bird = GameObject.Find("Bird");
		birdScript = Bird.GetComponent<Bird>();

		StartCoroutine(followRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator followRoutine(){
		while(true){
			gameObject.transform.position = Bird.transform.position;
			yield return null;
		}
	}

	private bool snapping = true;
	private bool exiting = false;
	void OnTriggerEnter2D(Collider2D otherObj){
		if(otherObj.ToString()[0] == 'b' && snapping){
			Debug.Log("BirdSnapper collide");
			birdScript.snapMe();	
			snapping = false;
			exiting = true;
		}
	}

	void OnTriggerExit2D(Collider2D otherColl){
		if(otherColl.ToString()[0] == 'b' && exiting){
			Debug.Log("BirdSnapper exit");
			snapping = true;
			exiting = false;
		}
	}


}
