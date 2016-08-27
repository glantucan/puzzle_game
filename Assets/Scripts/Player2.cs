﻿using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {

	[SerializeField] private float moveVel = 10F;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float xInput = Input.GetAxis("Horizontal");
		float zInput = Input.GetAxis("Vertical");
		Vector3 direction = zInput * Vector3.forward + xInput * Vector3.right;   
		if (xInput != 0 && zInput != 0) {
			direction = direction.normalized;
		}
  		this.transform.position += direction * moveVel * Time.deltaTime;
	}
}
