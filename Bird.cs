using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
	//hardcoded values are determined by experimentation
	//to make the gameplay better.	
	
	private bool onFly = true;
	private float STARTSPEED = 2f;	
	
	//flying velocity of the player
	private float flyVel; //start speed is 2.0.

	//incrase factor decreases overtime, but eventually
	//comes to its initial state again.
	private float INITIALVELOCITYINCREASEFACTOR = 0.5f;
	private float velocityIncreaseFactor;

	private bool birdTrigEnter = true;
	private bool birdTrigExit = false;

	private bool snapping = false;
	
	// Start is called before the first frame update
    void Start()
    {
        flyVel = STARTSPEED;
		velocityIncreaseFactor = INITIALVELOCITYINCREASEFACTOR;
		
		StartCoroutine(firstMoveRoutine());

		StartCoroutine(conditionCheckRoutine());
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && !snapping){
			if(onFly){
				flyVel = increaseVel(flyVel);
			}else if(!onFly){
				onFly = true;
				StartCoroutine(moveRoutine());
			}	
		}
    }
	
	IEnumerator firstMoveRoutine(){
		//wait before flying so that the player
		//will be ready to action.
		yield return new WaitForSeconds(0.7f);
		yield return StartCoroutine(moveRoutine());
	}

	IEnumerator moveRoutine(){
		while(onFly){
			//player is on fly in a direction until it touches the screen
			//to change direction or snaps on a block.
			//if it touches a trap, it's dead.
			gameObject.transform.Translate(Time.deltaTime*flyVel, -Time.deltaTime*Mathf.Abs(flyVel)/4,0);

			yield return null;
		}
		yield return null;
	}

	private float increaseVel(float velocity){
		//change direction and increase speed.
		//increase factor always get smaller and smaller
		//until it gets reinitialized again in another snap to the block.
		velocity += (velocity*velocityIncreaseFactor);
		velocityIncreaseFactor /= 2;
		return -velocity;//opposite direction.
	}

	private bool changeDir = true;
	
	void OnTriggerEnter2D(Collider2D otherObj){
		if(otherObj.ToString()[0] == 'b' && birdTrigEnter){
			if(changeDir){
				Debug.Log("Bird trig enter");
				//Debug.Log(gameObject.transform.position);			
				changeDir = false;
				onFly = false;
			
				birdTrigEnter = false;
				birdTrigExit = true;

				snapping = false;

				//setParent so that the player moves with the block
				//it snapped to.
				gameObject.transform.SetParent(otherObj.transform);

				alignBirdPosition();

				//return back to initial velocity, here.
				/* moveVel => opposite unit norm.
				 * meaning, moveVel = -4.0
				 * after the operation, moveVel = +1;
				 * its unit norm = -1
				 * opposite unit norm = +1. 
				 * It's not the Quake 3 hack, but it's honest work.
				 * */
				// opposite unit norm.	
				flyVel = -flyVel/Mathf.Abs(flyVel);		
				// reassign the starting speed and
				// get the velocity  with the changed direction.
				flyVel *= STARTSPEED;
				//like i promised in the increaseVel, increase
				//factor is reinitialized on a block.
				velocityIncreaseFactor = INITIALVELOCITYINCREASEFACTOR;
			}
		}else if(otherObj.ToString()[0] == 't'){
			//Debug.Log("trap collide -> you dead");
			Debug.Log(otherObj.transform.position);
			gameOver();
		}
	}

	void OnTriggerExit2D(){
		//leaving the block to fly to your dead.
		if(onFly && birdTrigExit){
			//it flies away from a block.
			Debug.Log("Bird trig exit");
			changeDir = true;
	
			birdTrigEnter = true;
			birdTrigExit = false;

			gameObject.transform.SetParent(null);
		}
	}

	private void alignBirdPosition(){
		if(gameObject.transform.position.x > 1.2f){
				Vector3 temp = gameObject.transform.position;
				temp.x = 1.2f;
				gameObject.transform.position = temp;
		}else if(gameObject.transform.position.x < -1.2f){
				Vector3 temp = gameObject.transform.position;
				temp.x = -1.2f;
				gameObject.transform.position = temp;
		}
	}

	public void snapMe(){
		Debug.Log("snapping");
		snapping = true;

		flyVel *= 2.5f;
	}

	IEnumerator conditionCheckRoutine(){
		while(true){
			if(snapping == true && birdTrigEnter == false){
				snapping = false;
			}
					
			yield return null;	
		}	
	}

	void OnBecameInvisible() {
    	gameOver();
	}

	private void gameOver(){
			SceneManager.LoadScene("SampleScene");
	}
}
