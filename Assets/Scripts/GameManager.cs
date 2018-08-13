using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static event Action OnCubeSpawned = delegate {};
	private Spawner[] spawners;
    private int spawnerIndex;
    private Spawner currentSpawner;

    void Awake (){
		spawners = FindObjectsOfType<Spawner>();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			if (Mover.current !=null)
				Mover.current.Stop();

			spawnerIndex = spawnerIndex == 0 ? 1 : 0;
			currentSpawner = spawners[spawnerIndex];
			currentSpawner.Spawn();
			OnCubeSpawned();
		}
	}
}
