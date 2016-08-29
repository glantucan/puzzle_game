﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Movement related properties
	[SerializeField] private float moveVel = 10F;
	private Rigidbody rb;

	// Switches properties
	private bool isNearSwitch;
	private GameObject nearestButton;
	private bool isRampOn;
	private bool isBridgeOn;

	/// <summary>
	/// Initialization function. Executes just once when the GameObject is created at runtime.
	/// </summary>
	void Start () {
		// Initialize the rigidbody rb property
		this.rb = this.GetComponent<Rigidbody>();

		// Not near a switch initially.
		this.isNearSwitch = false;
	}

	void Update () {
		// Calculate the movement direction from the Unity input axes.
		float xInput = Input.GetAxisRaw("Horizontal");
		float zInput = Input.GetAxisRaw("Vertical");
		Vector3 direction = zInput * Vector3.forward + xInput * Vector3.right;   

		// Only move the player if there is an input
		if (direction != Vector3.zero) {
			// Note that we use the rigidbody to move the player, and only 
			// when there is input, so we do not interfere with the Physics engine
			this.rb.velocity = direction.normalized * moveVel;
		}


		// SWITCH BUTTONS
		if (isNearSwitch) {
			if (Input.GetKeyUp(KeyCode.Space)) {
				Transform buttonLight = nearestButton.transform.Find("ButtonLight");
				Transform glimmerLight = nearestButton.transform.Find("GlimmerLight");
				buttonLight.gameObject.SetActive(!buttonLight.gameObject.activeSelf);
				glimmerLight.gameObject.SetActive(!glimmerLight.gameObject.activeSelf);

				Debug.Log("Player activated a switch: " + nearestButton.name);
				if (nearestButton.name == "RampSwitch") {
					
				} else if (nearestButton.name == "BridgeSwitch") {
					if (isBridgeOn) {
						GameObject bridge = GameObject.Find("BridgeAnimation");
						Animation bridgeAnimation = bridge.GetComponent<Animation>();
						bridgeAnimation.Play("BridgeOut");
						isBridgeOn = false;
					} else {
						GameObject bridge = GameObject.Find("BridgeAnimation");
						Animation bridgeAnimation = bridge.GetComponent<Animation>();
						bridgeAnimation.Play("BridgeIn");
						isBridgeOn = true;
					}
				}
			}	
		}
	}

	void OnTriggerEnter(Collider other) {
		GameObject otherGO = other.gameObject;
		if (otherGO.CompareTag("Switch")) {
			Debug.Log("Player entered the trigger");
			isNearSwitch = true;
			nearestButton = otherGO;
		}
	}

	void OnTriggerExit(Collider other) {
		GameObject otherGO = other.gameObject;
		if (otherGO.CompareTag("Switch")) {
			Debug.Log("Player went out of the trigger");
			isNearSwitch = false;
			nearestButton = null;
		}
	}


}
