using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Movement related properties
	[SerializeField] private float moveVel = 10F;
	private Rigidbody rb;

	/// <summary>
	/// Initialization function. Executes just once when the GameObject is created at runtime.
	/// </summary>
	void Start () {
		// Initialize the rigidbody rb property
		this.rb = this.GetComponent<Rigidbody>();
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
	}
}
