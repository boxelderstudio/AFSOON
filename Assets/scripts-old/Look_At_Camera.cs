using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_At_Camera : MonoBehaviour {


	GameObject mainCam;
	
	// Use this for initialization
	void Start () 
	{
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt(mainCam.transform);
	}
}
