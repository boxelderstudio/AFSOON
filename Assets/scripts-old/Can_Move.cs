using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can_Move : MonoBehaviour 
{
	[SerializeField] GameObject trigger_;
	[SerializeField] GameObject CTRL_text_;
	[SerializeField] GameObject Space_text_;
	[SerializeField] GameObject climb_pos_;
 
	Rigidbody rb;

	[HideInInspector]
	public Player_Enter_Trigger Player_Is_In;

	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		rb.isKinematic = false;
		Player_Is_In = trigger_.GetComponent<Player_Enter_Trigger>();
	}
	
	void Update () 
	{
		float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

		Can_Move_Obstacle();
		Climb(climb_pos_.transform.position);
	}


	void Climb(Vector3 pos)
	{
		if (Player_Is_In.player_is_in_ && 
			Player_Is_In.player_gameobject)
		{
			Space_text_.SetActive(true);
			
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (Vector3.Distance(Player_Is_In.player_gameobject.transform.position, 
										climb_pos_.transform.position) < 3f)
				{
					Player_Is_In.player_gameobject.transform.position = climb_pos_.transform.position;
					Space_text_.SetActive(false);
				}
				else
					Debug.Log("It's too high to climb");
			}		
		}
		else
			Space_text_.SetActive(false);
	}



	void Can_Move_Obstacle()
	{
		if (Player_Is_In.player_is_in_ == true)
		{
			CTRL_text_.SetActive(true);

			//if (Input.GetKey(KeyCode.Mouse0))
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
				rb.isKinematic = false;
				CTRL_text_.SetActive(false);
			}	
			else
			{
				rb.isKinematic = true;
			}
		}
		else
			CTRL_text_.SetActive(false);
	}
}
