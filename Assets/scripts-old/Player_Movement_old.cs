using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_old : MonoBehaviour 
{

	[SerializeField] public float speed_ = 10f;

	[HideInInspector]
	public Vector3 movement;
	Rigidbody PlayerRigidbody;
	// Object_Reflection_Movement Object_Reflection_Movement;


	void Awake () 
	{
		PlayerRigidbody = GetComponent<Rigidbody>();
	}

	
	void Update ()
	{
		float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
		
		PlayerMove(PlayerRigidbody,speed_,h,v);
	}

	void PlayerMove(Rigidbody Rbody, float Move_Speed , float h , float v)
	{
		movement.Set(-v,0,h);
		movement = movement.normalized * speed_ * Time.deltaTime;
		Rbody.MovePosition(transform.position + movement);

		// If the player was moving and has direction, rotate it.
		// To avoid reset rotation while stop moving
		if (movement != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(movement);
		}
	}





}
