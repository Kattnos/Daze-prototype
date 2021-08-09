using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
	//Fix toggle crouch bug with timer
    private CharacterController charControl;
	public int speed;
	public float gravity;
	private Vector3 velocity;
	private GameObject Camera;
	public bool holdCrouch;
	Quaternion eulerAngle;
	public bool isCrouching = false;
	private float timer;
	private bool firstCrouch = true;
	private void Start()
	{
		charControl = GetComponent<CharacterController>();
		Camera = GameObject.Find("PlayerCamera");
	}
	private void FixedUpdate()
	{
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		move = Camera.transform.TransformDirection(move);
		charControl.Move(move * speed * Time.deltaTime);
		velocity.y += gravity * Time.deltaTime;
		charControl.Move(velocity * Time.deltaTime);

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
		if (isCrouching == true)
		{
			
		} else
		{

		}
	}
}
