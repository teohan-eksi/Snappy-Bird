using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
	public GameObject createBlock(){
		//create a block object with the appropriate sprite.
		GameObject block = new GameObject("block");
		block.AddComponent<SpriteRenderer>();
		block.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("block");
		//add a collider to get the size (x, y) later.
		block.AddComponent<BoxCollider2D>();
		block.GetComponent<BoxCollider2D>().isTrigger = true;	
			
		return block;	
	}

	public GameObject createTrap(){
		//create a trap object with the appropriate sprite.
		GameObject trap = new GameObject("trap");
		trap.AddComponent<SpriteRenderer>();
		trap.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("trap");
		//add a collider to get the size (x, y).
		trap.AddComponent<BoxCollider2D>();	
		
		return trap;
	}
}
