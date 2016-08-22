using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* TODO:
	[x] Switches actions
	[ ] Throw rock in the end of level hole (Must be tall)
	[x] Follow Camera
	[x] Rotate camera 90º
*/

public class Player : MonoBehaviour {
	//[SerializeField] private GameObject baseGround;

	// Movement related properties
	private Rigidbody rb;
	[SerializeField] private float moveVel = 2F;
	private float drag;
	[SerializeField] private List<Collider> groundContacts;
	private bool isGrounded = true; // This is necessary to find out when it should fall

	// Inventory properties
	[SerializeField] private GameObject[] inventory; // Inventory array
	[SerializeField] private int inventorySize = 5;  // Slots count on the inventory
	private int firstEmpty;	// First empty slot of the inventory

	// Switches properties
	private bool isNearSwitch;
	private GameObject nearestButton;
	// specific switches flags.
	private bool rampOn;
	private bool bridgeOn;

	// Camera related properties
	[SerializeField] private GameObject mainCamera;
	private Transform tr;
	private Transform cameraTr;
	[SerializeField] private float followSpeed;
	[SerializeField] private float rotationSpeed;
	private Vector3 camDistanceVec;

	[SerializeField] private int[] cameraAngles;
	[SerializeField] private int rotatedControls; // To rotate the controls when we rotate the camera. Can be 0, 90, 180 or 360

	/// <summary>
	/// Initialization function. Executes just once when the GameObject is created at runtime.
	/// </summary>
	void Start () {
		// Initialize the rigidbody and the drag property
		this.rb = this.GetComponent<Rigidbody>();
		this.drag = rb.drag;

		// Initialize the groundContacts and the grounded flag so the falling sytem works.
		// On each collision with a go tagged as Ground, that collider is added to the list. On each collision exit
		// that collider is removed. When tha list is empty the player is not touching the ground.
		this.groundContacts = new List<Collider>();
		this.isGrounded = true;

		// Our inventory is limited;
		this.inventory = new GameObject[this.inventorySize];
		this.firstEmpty = 0;

		// Not near a switch initially.
		this.isNearSwitch = false;

		// Camera transform initialization
		this.tr = this.transform;
		this.cameraTr = this.mainCamera.transform;
		this.camDistanceVec = cameraTr.position - this.tr.position;

		// Initialize camera angles array 
		cameraAngles = new int[4]{0, 90, 180, 270};
		// Controls rotation initial value 
		this.rotatedControls = 0;
	}

	/// <summary>
	/// Gameloop function. Executed every frame by the Unity engine.
	/// </summary>
	void Update () {

		// MOVEMENT:
		// -------------------------------------------------------------------------------------------------------------
		// -> See OnCollisionEnter and OnCollisionExit for the isGrounded flag
		if (this.isGrounded) {
			// Set the drag so it simulates floor friction.
			this.rb.drag = drag;
			// Calculate the movement direction from the Unity input axes. We use GetAxisRaw cause we want the Physics 
			// engine to do the movement smoothing, not the Input Unity system
			float hInput = Input.GetAxisRaw("Horizontal");
			float vInput = Input.GetAxisRaw("Vertical");
			Vector3 rawDisplacement;
			if (cameraAngles[this.rotatedControls] == 0) {
				rawDisplacement = new Vector3(hInput, 0, vInput);
			} else if (cameraAngles[this.rotatedControls] == 90) {
				rawDisplacement = new Vector3(-vInput, 0, hInput);
			} else if (cameraAngles[this.rotatedControls] == 180) {
				rawDisplacement = new Vector3(-hInput, 0, -vInput);
			} else { // rotatedControls == 270
				rawDisplacement = new Vector3(vInput, 0, -hInput);
			}
	        // Only move the player if there is an input
			if (rawDisplacement != Vector3.zero) {
				// Note we use the rigidbody to move the player, and only when there is input so we do not interfere with 
				// the Physics engine
				this.rb.velocity = rawDisplacement * this.moveVel;
	        } 
		} else {
			// Player is falling -> no friction.
			this.rb.drag = 0;
		}


		// ROTATION:
		// -------------------------------------------------------------------------------------------------------------
		// Create the ray starting at the camera with the direction corresponding to the 2D position
		// of the mouse pointer on the screen.
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		// Create a plane, parallel to the ground and at the height of the player gameobject 
		// to intersect the camera ray. This way we avoid inconsitencies produced 
		// by different game object heights in the scene.
		Plane viewPlane = new Plane(Vector3.up, rb.position); 	// 1st paramenter is the vector defining orientation of 
																// the plane. 2nd is just a point the plane must include
        // Define a float to hold the distance to the intersection point
        float rayDistance;
        // Cast the ray from the plane and check if there is an intersection
        if (viewPlane.Raycast(mouseRay, out rayDistance)) {
        	// Get the intersection point between the ray and the plane
            Vector3 intersectionPoint = mouseRay.GetPoint(rayDistance);
            // Draw a line in the editor so we cans see the ray and check 
            // whether it's all right
            Debug.DrawLine(mouseRay.origin, intersectionPoint, Color.green);
            // Finally rotate the player so it looks to the intersection point
            //rotator.rotate(intersectionPoint);
            this.transform.LookAt(intersectionPoint);
        }


		// PICK THINGS
		// -------------------------------------------------------------------------------------------------------------
		// -> See OnTriggerEnter


		// THROW THINGS
		// -------------------------------------------------------------------------------------------------------------
		if (Input.GetMouseButtonUp(0)) {
			if(firstEmpty > 0) {
				// Get the last non empty slot
				int throwableSlot = firstEmpty - 1;
				GameObject throwable = inventory[throwableSlot]; 
				if (throwable != null) {
					// Try to get the throwable Rigidbody
					Rigidbody throwableRb = throwable.GetComponent<Rigidbody>();
					// If it has a Rigidbody we can throw it
					if (throwableRb != null) {
						// Ensure we put it outside the player collider to avoid bad side effects.
						throwable.transform.position = transform.position + 1F * transform.forward + .8F * transform.up;
						throwable.SetActive(true);
						// Make the object obbey physics laws
						throwableRb.GetComponent<Collider>().isTrigger = false;
						throwableRb.isKinematic = false;
						throwableRb.useGravity = true;
						// Give its initial velocity
						throwableRb.velocity = 20F*(transform.forward + transform.up * 0.2F); 
						// Remove it from the inventory
						inventory[throwableSlot] = null;
						firstEmpty--;
					}
				}
			}
		}

		// SWITCH BUTTONS
		// -> See OnTriggerEnter and OnTriggerExit for the isNearSwitch flag
		if (isNearSwitch) {
			if (Input.GetKeyUp(KeyCode.Space)) {
				Transform buttonLight = nearestButton.transform.FindChild("ButtonLight");
				Transform glimmerLight = nearestButton.transform.FindChild("GlimmerLight");
				buttonLight.gameObject.SetActive(!buttonLight.gameObject.activeSelf);
				glimmerLight.gameObject.SetActive(!glimmerLight.gameObject.activeSelf);

				switch (nearestButton.name) {
					case "RampSwitch":
						GameObject ramp = GameObject.Find("RampAnimation");
						Animator rampAnimator = ramp.GetComponent<Animator>();
						rampAnimator.SetTrigger("switch");
					break;
					case "BridgeSwitch":
						if (bridgeOn) {
							GameObject bridge = GameObject.Find("BridgeAnimation");
							Animation bridgeAnimation = bridge.GetComponent<Animation>();
							bridgeAnimation.Play("BridgeOut");
							bridgeOn = false;
						} else {
							GameObject bridge = GameObject.Find("BridgeAnimation");
							Animation bridgeAnimation = bridge.GetComponent<Animation>();
							bridgeAnimation.Play("BridgeIn");
							bridgeOn = true;
						}

					break;
					default:
						print("WARNING: The '" + nearestButton + "' switch doesn't have an action associated");
					break;
				}
			}
		}

		// CAMERA ROTATION
		// SHOUL'D THE CAMERA HAVE A KINEMÁTIC RIGIDBODY??? it doesn't have a collider!!
		// Although we don't actually move the camera here all user input must be handled on the Update function always
		if (Input.GetKeyUp(KeyCode.Q)) {
			if (rotatedControls != 0) {
				rotatedControls--;
			} else {
				rotatedControls = 3;
			}
			this.camDistanceVec = new Vector3(this.camDistanceVec.z, this.camDistanceVec.y, -this.camDistanceVec.x);
		}
		if (Input.GetKeyUp(KeyCode.E)) {
			if (rotatedControls != 3) {
				rotatedControls++;
			} else {
				rotatedControls = 0;
			}
			this.camDistanceVec = new Vector3(-this.camDistanceVec.z, this.camDistanceVec.y, this.camDistanceVec.x);
		}
	}

	// We use this to avoid hickups ine the camera movement.
	void FixedUpdate() {
		// CAMERA FOLLOW
		cameraTr.LookAt(this.tr);
		Vector3 wantedCamPosition = this.tr.position + this.camDistanceVec;
		// Iterpolate the camera postion between the current value and the wanted value
		this.cameraTr.position = Vector3.Lerp(this.cameraTr.position, wantedCamPosition, 0.1F);
	}

	void OnTriggerEnter(Collider other) {
		GameObject otherGO = other.gameObject;
		// If it is a pickable object pick it
		if (otherGO.CompareTag("Pickable")) {
			if (firstEmpty < inventory.Length) {
				otherGO.SetActive(false);
				inventory[firstEmpty] = otherGO;
				firstEmpty++;
			} else {
				print("WARNING: Inventory is full");
			}
		}
		// See if we are near a swich button 
		else if (otherGO.CompareTag("Switch")) {
			isNearSwitch = true;
			nearestButton = otherGO;
		}
	}

	void OnTriggerExit(Collider other) {
		GameObject otherGO = other.gameObject;
		// If it is a pickable object pick it
		if (otherGO.CompareTag("Switch")) {
			isNearSwitch = false;
		}
	}

	void OnCollisionEnter(Collision collision) {
		Collider otherCol = collision.collider;
		// If it is the ground set the grounded flag so that we can control de movement
		if (otherCol.CompareTag("Ground")) {
			groundContacts.Add(otherCol);
			isGrounded = true;
			//print("Grounded");
		}
	}

	void OnCollisionExit(Collision collision) {
		Collider otherCol = collision.collider;
		// If it is the ground set the grounded flag so that the Physics engine control de movement
		if (otherCol.CompareTag("Ground")) {
			groundContacts.Remove(otherCol);
			if (groundContacts.Count == 0) {
				isGrounded = false;
				//print("Should fall");
			}
		}
	}
}
