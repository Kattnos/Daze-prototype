using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
	//Fix toggle crouch bug with timer
    private CharacterController charControl;
	public float speed; //Current speed
	public float defaultSpeed; //Speed when walking
	public float gravity;
	private Vector3 velocity;
	private GameObject Camera;
	public bool holdCrouch;
	Quaternion eulerAngle;
	public bool isCrouching = false;
	private float timer;
	private bool firstCrouch = true;
	private float t = 0f;
	private float t2 = 0f;
	public float crouchLength;
	public float crouchSpeed; //Speed when crouching
	private float currentPosZ;
	private float currentPosZ2;
	private bool crouchDone;
	public float crouchingMovementSpeed;
	private bool stopCrouching;
	private float temp;
	private float standardPosZ = 0.0066f;
	public bool toggleSprint;
	public bool sprinting;
	public float sprintingSpeed;
	private float st = 0f;
	private float st2 = 0f;
	public float transitionSpeed;
	private float currentSpeed1;
	private float currentSpeed2;
	public float stamina;
	public float staminaTimer;
	private bool staminaBool;
	private bool staminaReset;
	private void Start()
	{
		charControl = GetComponent<CharacterController>();
		Camera = GameObject.Find("PlayerCamera"); 
	}
	private void Update () {
		if (holdCrouch == true)
		{
			if (Input.GetAxis("Crouch") == 1) {
				isCrouching = true;
			} else
			{
				isCrouching = false;
			}
		} else if (holdCrouch == false)
		{
			timer += Time.deltaTime;
			if (Input.GetAxis("Crouch") == 1 && isCrouching == false && timer >= 0.05f)
			{
				firstCrouch = false;
				timer = 0f;
				isCrouching = true;
			} else if (Input.GetAxis("Crouch") == 1 && isCrouching == true && timer >= 0.05f && firstCrouch == false)
			{
				timer = 0f;
				isCrouching = false;
			}
		}
		if (toggleSprint == false) {
			 if (Input.GetAxis("Sprint") == 1 && staminaBool == false) {
				sprinting = true;
			 } else {
				 sprinting = false;
			 }
		} else if (toggleSprint == true) {
			//Under development
		}
		if (isCrouching == true)
		{ 
			speed = crouchingMovementSpeed;
			t2 = 0f;
			t += Time.deltaTime;
			currentPosZ = Camera.transform.localPosition.z;
			Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y, Mathf.SmoothStep(currentPosZ2, 0.0016f, (t / crouchSpeed)));	
		} else if (isCrouching == false && Camera.transform.localPosition.z < 0.0066f)
		{
			Debug.Log("ss");
			speed = defaultSpeed;
			currentPosZ2 = Camera.transform.localPosition.z;
			t = 0f;
			t2 += Time.deltaTime;
			Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y, Mathf.SmoothStep(currentPosZ, 0.0066f, (t2 / crouchSpeed)));
		} else {
			currentPosZ = Camera.transform.localPosition.z;
			currentPosZ2 = Camera.transform.localPosition.z;
			t = 0f;
			t2 = 0f;
		}
		if (sprinting == true && staminaBool == false) { //sprinting
			st += Time.deltaTime;
			speed = Mathf.SmoothStep(currentSpeed1, sprintingSpeed, st / transitionSpeed);
			st2 = 0f;
			currentSpeed2 = speed;
			staminaTimer += Time.deltaTime;
			if (staminaTimer >= stamina) {
				staminaBool = true;
			}
		} else if(sprinting == false && speed != defaultSpeed && isCrouching == false && staminaReset == false) { //let go of sprint button but is not at normal speed
			st2 += Time.deltaTime;
			speed = Mathf.SmoothStep(currentSpeed2, defaultSpeed, st2 / transitionSpeed);
			st = 0f;
			currentSpeed1 = speed;
			if (staminaTimer >= 0f) {
				staminaTimer -= Time.deltaTime;
			}
		} else {
			st = 0f;
			st2 = 0f;
			currentSpeed1 = speed;
			currentSpeed2 = speed;
			if (staminaTimer >= 0f) {
				staminaTimer -= Time.deltaTime;
			}
		}
		    if (staminaTimer <= 0f && isCrouching == false) {
			staminaBool = false;
			staminaReset = false;
			Debug.Log("s2");
			speed = defaultSpeed;			
		} else if (staminaTimer >= stamina) {
			staminaBool = true;
			staminaReset = true;
			speed = 3f;
			Debug.Log("ww");
		}
		
	}

	private void FixedUpdate()
	{
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		move = Camera.transform.TransformDirection(move);
		charControl.Move(move * speed * Time.deltaTime);
		velocity.y += gravity * Time.deltaTime;
		charControl.Move(velocity * Time.deltaTime);
	}
}
