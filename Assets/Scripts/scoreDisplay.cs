﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text myText = GetComponent<Text>();
        myText.text = ScoreKeeper.score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
