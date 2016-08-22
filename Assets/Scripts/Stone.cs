using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {

	private Rigidbody rb;
	private Collider col;
	private Renderer rend;
	public Color colorStart = Color.white;
    public Color colorEnd = Color.red;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(col.isTrigger == false) {
			if (rb.IsSleeping()) {
				rb.isKinematic = true;
				rb.useGravity = false;
				col.isTrigger = true;
			} else {
				rend.material.color = Color.Lerp(colorStart, colorEnd, rb.velocity.magnitude/20F);
			}
		}
	}
}
