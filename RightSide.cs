using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/* TODO do the explanations,
 * 		continue with applying the determined game logic.
 */
public class RightSide : MonoBehaviour
{
	private GameObject rightContainer;
	
	private GameObject block;
	private GameObject trap;
	private Vector2 blockSize;//size of a block as (width, height)
	private Vector2 trapSize;//size of a trap as (width, height)
	
	private Vector3 rightTop;//right top point of the world

    // Start is called before the first frame update
    void Start()
    {   

		StartCoroutine(fillRightContainer());//fn 4
    }

	IEnumerator fillRightContainer(){

		//create a container to put all the blocks and traps into.
		//later, i just move the container and everything in it moves, too.
		rightContainer = new GameObject("RightContainer");

		yield return null; // fn 5
		//Debug.Log("RightContainer created from RightSide "+ Time.frameCount.ToString());
			
		block = gameObject.GetComponent<ObjectCreator>().createBlock();
		block.transform.SetParent(rightContainer.transform);
		blockSize = block.GetComponent<BoxCollider2D>().size;
		
		yield return null; // fn 6
		//Debug.Log("block created from RightSide "+ Time.frameCount.ToString());

		trap = gameObject.GetComponent<ObjectCreator>().createTrap();	
		trap.transform.SetParent(rightContainer.transform);
		trapSize = trap.GetComponent<BoxCollider2D>().size;
		
		//remove the next 2 yields to get rid of the ugly load at the beginning
		yield return null; // fn 7
		//Debug.Log("trap created from RightSide "+ Time.frameCount.ToString());

		rightTop = gameObject.GetComponent<VariableCalculator>().rightTopVector();

		yield return null; // fn 8
		//Debug.Log("rightTop calculated from RightSide "+ Time.frameCount.ToString());

		//setting blocks and traps
		int randInt;
		var rand = new Random();
		bool moving = false; //container doesn't move until it's ready.

		/* put the original objects one 'trapSize.y' left and 
		 * one blockSize.y up from the top right corner in order
		 * to align them into their correct places while producing them in bulk.
		 *
		 * trapSize.y left because traps are lied down vertically.
		 * blockSize.y up because blocks are shifted down and they
		 * overlap if i don't shift that first block one length up.
		 */
		block.transform.position = rightTop - new Vector3(blockSize.x/2 + trapSize.y, 
															-blockSize.y/2, 
															0);
		trap.transform.position = block.transform.position;	

		yield return null; // fn 9
		//Debug.Log("initial positioning of objects completed from RightSide "+ Time.frameCount.ToString());

		while(true){
			/* Explain the logic here
			 * */
			//put the block one 'blockSize.y' lower than the initial position.
			block.transform.position += new Vector3(0, -blockSize.y, 0); 
			
			//put 'randInt' blocks in a vertical line 
			//set parent to 'rightContainer' game object.
			randInt = rand.Next(1,4);
			for(int i = 0; i<randInt; i++){
				Instantiate(block, rightContainer.transform);
				block.transform.position += new Vector3(0,
													    -blockSize.y,
													    0);
				//this may cause the glitchiness while moving.
				yield return null;//take a breather.
			}

			/* create a trap hole and put it after the last block line.
			 * i wrote position parameters in a way that it gives the look 
			 * you see in the game. Tweak the x, y parameters a little bit 
			 * if you want to go crazy.
			 */
			trap.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0,0,1));
			trap.transform.position = block.transform.position + new Vector3(trapSize.y/2, trapSize.y/2,0); 

			Instantiate(trap, rightContainer.transform);
			trap.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0,0,1));
			trap.transform.position += new Vector3(trapSize.y/2, 
												   -trapSize.y/2,
												   	0);
			yield return null;//take a breather.
			Instantiate(trap, rightContainer.transform);		
			trap.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(0,0,1));
			trap.transform.position += new Vector3(-trapSize.y/2, -trapSize.y/2, 0);

			yield return null;//take a breather.
			Instantiate(trap, rightContainer.transform);			

			/* let's build a balancer to keep the block and 
			 * trap hole numbers in check. */

			/* rightContainer starts to move after the block
			 * at the bottom is below a certain limit.
			 * this limit was chosen as a best fit after
			 * doing some observations. 
			 * The limit is affected by the size of the blocks and traps
			 * and maybe the height of the world but i'm not sure. */
			if(!moving && block.transform.position.y < -2){
				moving = true;
				StartCoroutine(moveRightContainer());
			}
		
			/* this while loop keeps the block at the bottom
			 * above some limit by stoping (yield) the production
			 * of blocks and trap holes (right map).
			 * So, the bottom block never drops to infinity.
			 * with the right amount of destruction from the
			 * upper side, a nice balance is kept and 
			 * the game can run forever with managable
			 * number of blocks and trap holes. */
			while(block.transform.position.y < -7){
				yield return null;
			}		

			/* these two while loops below destroy excess game objects
			 * that go up in the world. The limit was determined
			 * by careful observations :D. */
			while(GameObject.Find("block(Clone)").transform.position.y > 6){
				Destroy(GameObject.Find("block(Clone)"));
				yield return null;
			}
			
			while(GameObject.Find("trap(Clone)").transform.position.y > 6){
				Destroy(GameObject.Find("trap(Clone)"));
				Destroy(GameObject.Find("trap(Clone)"));
				Destroy(GameObject.Find("trap(Clone)"));
				yield return null;
			}
			yield return null;
		}
		//code doesn't reach here, but just in case.
		yield return null;
	}

	IEnumerator moveRightContainer(){
		while(true){
			rightContainer.transform.Translate(0, 1.5f*Time.deltaTime, 0);
			yield return null;
		}
		//code doesn't reach here, but just in case.
		yield return null;
	}

}
