﻿using System.Collections;
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
	private float t;
	public float crouchLength;
	public float currentPosZ;
	private bool crouchDone;
	private bool stopCrouching;
	private float temp;
	private float standardPosZ = 0.0066f;
	private void Start()
	{
		charControl = GetComponent<CharacterController>();
		Camera = GameObject.Find("PlayerCamera"); 
	}
	private void Update () {
		currentPosZ = Camera.transform.localPosition.z;
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
		if (isCrouching == true && crouchDone == false)
		{
			t += Time.deltaTime;
			Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y, Mathf.Lerp((currentPosZ -= crouchLength), currentPosZ, t));
			if (Camera.transform.localPosition.z == standardPosZ - crouchLength) {
				crouchDone = true;
				stopCrouching = true;
			}
		} else if (isCrouching == false && stopCrouching == true)
		{
			Debug.Log(standardPosZ);
			Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y, Mathf.Lerp(currentPosZ, (currentPosZ += crouchLength), t));
			if (Camera.transform.localPosition.z >= standardPosZ + crouchLength) {
				stopCrouching = false;
				Debug.Log("This triggers too");
			}
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