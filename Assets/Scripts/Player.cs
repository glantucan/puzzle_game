using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField] private float moveVel = 10F;
	private Rigidbody rb;

	void Start () {
		this.rb = this.GetComponent<Rigidbody>();
	}

	void Update () {
		float xInput = Input.GetAxis("Horizontal");
		float zInput = Input.GetAxis("Vertical");
		Vector3 direction = zInput * Vector3.forward + xInput * Vector3.right;   
		if (direction != Vector3.zero) {
			this.rb.velocity = direction.normalized * moveVel;
		}
	}
}
