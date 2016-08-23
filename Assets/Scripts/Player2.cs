using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {

	[SerializeField] private float moveVel = 10F;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float xInput = Input.GetAxisRaw("Horizontal");
		float zInput = Input.GetAxisRaw("Vertical");
		Vector3 direction = zInput * Vector3.forward + xInput * Vector3.right;   
		                             
  		this.transform.position += direction.normalized * moveVel * Time.deltaTime;
	}
}
