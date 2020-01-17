using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public GameObject object_;
    GameObject player_;
    bool playerIsNear_ = false;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (playerIsNear_ == true )
        {
            if (Input.GetKey(KeyCode.RightControl))
            {
                MoveObstacle(h, v);
                player_.GetComponent<Rigidbody>().mass = object_.GetComponent<Rigidbody>().mass;
                player_.GetComponent<Rigidbody>().drag = object_.GetComponent<Rigidbody>().drag;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Player_ins")
        {
            player_ = other.gameObject;
            playerIsNear_ = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Player_ins")
        {
            playerIsNear_ = false;
        }
    }

    public void MoveObstacle(float h , float v)
    {
        Vector3 movement = new Vector3(h, 0, v);
        movement = movement.normalized * Time.deltaTime * player_.GetComponent<PlayerMovement>().Speed;

        object_.GetComponent<Rigidbody>().velocity = movement ;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.gameObject.transform.position, this.gameObject.transform.localScale * 2.74f);
    }
}
