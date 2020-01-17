using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Enter_Trigger : MonoBehaviour 
{
	public bool player_is_in_ = false;
	public GameObject player_gameobject;

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
        {
			player_is_in_ = true;
			player_gameobject = other.gameObject;
        }	
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
        {
			player_is_in_ = false;
			player_gameobject = null;
        }	
	}
}
