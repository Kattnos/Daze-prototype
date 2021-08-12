using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
	public float horizontal;
	public float vertical;
	private float yaw = 0f;
	private float pitch = 0f;
	public bool fullScreen;
	public int refreshRate;

	private void Start () {
		Screen.SetResolution(640, 360, fullScreen, refreshRate);
	}
	private void Update()
	{
		yaw += horizontal * Input.GetAxis("Mouse X");
		pitch -= vertical * Input.GetAxis("Mouse Y");
		pitch = Mathf.Clamp(pitch, -80f, 80f);

		transform.eulerAngles = new Vector3(pitch, yaw, 0f);
	}
}