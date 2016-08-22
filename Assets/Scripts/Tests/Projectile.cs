using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector3 dir;
	public float vel;
	private Rigidbody rb;
	private bool flag = true;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > 1F && flag == true) {
			rb.velocity = vel*dir;
			rb.useGravity = true;
			flag = false;
		}
	}
}
