using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private int score;
	private Text text;
	void Start () {
		text = GetComponent <Text>();
		GameManager.OnCubeSpawned += GameManager_OnCubeSpawned;
	}
	void OnDestroy(){
		GameManager.OnCubeSpawned -= GameManager_OnCubeSpawned;
	}

    private void GameManager_OnCubeSpawned()
    {
        score++;
		text.text = "Score: " + score;
    }
}
