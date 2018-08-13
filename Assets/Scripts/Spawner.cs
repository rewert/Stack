using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Mover cubePref;
	[SerializeField]
	private MoveDirection moveDirection;

	public void Spawn(){
		var cube = Instantiate(cubePref);

		if (Mover.last != null && Mover.last.gameObject != GameObject.Find("Start")){
			float x = moveDirection == MoveDirection.X ? transform.position.x : Mover.last.transform.position.x;
			float z = moveDirection == MoveDirection.Z ? transform.position.z : Mover.last.transform.position.z;

			cube.transform.position = new Vector3(x, Mover.last.transform.position.y + cubePref.transform.localScale.y, z);
		}
		else {
			cube.transform.position = transform.position;
		}
		cube.MoveDirection = moveDirection;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, cubePref.transform.localScale);
	}
}
