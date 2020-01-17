using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float Speed = 170;
    [HideInInspector]
    public bool IsInTheMirror;
    Rigidbody playerRigidbody; 
    Vector2 moveVector;


    void Start()
    {
        playerRigidbody = this.gameObject.GetComponent<Rigidbody>();
        IsInTheMirror = false;
    }


    void Update() 
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if( IsInTheMirror == false )
        {
            moveVector.x = h;
            moveVector.y = v;
            Move(moveVector.x, moveVector.y);
        }
    }


    // movement method
    public void Move(float h , float v)
    {
        Vector3 movement = new Vector3(h,0,v);
        movement = movement.normalized * Speed * Time.deltaTime;

        playerRigidbody.velocity = movement ;

        // change player orientation
        if (movement != Vector3.zero)
		{
			this.gameObject.transform.rotation = Quaternion.LookRotation(movement);
		}
    }
}
