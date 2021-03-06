﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{

	[SerializeField] GameObject player_;
	[SerializeField] public float dist_mult_;
	[SerializeField] float lerp_speed_;
	// [SerializeField] Camera mirror_camera_;
	// [SerializeField] Vector3 mirror_cam_offset_;
	
	Vector3 offset;

	void Start () 
	{
		offset = -transform.forward.normalized;
		transform.position = player_.transform.position;
		transform.position = transform.position + (offset * dist_mult_);
	}
	
	void FixedUpdate () 
	{
		Vector3 Target_Cam_Pos = player_.transform.position + (offset * dist_mult_);
		transform.position = Vector3.Lerp(transform.position , Target_Cam_Pos , lerp_speed_* Time.deltaTime); 

		// mirror_camera_.transform.position = transform.position +  mirror_cam_offset_;
	}
}
