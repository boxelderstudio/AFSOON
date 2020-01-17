using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if( other.gameObject.tag == "Player" )
        {
            CheckPointManager.singeltone_.lastPos = gameObject.transform.position;
            Debug.Log( "Check Point pos: " + gameObject.transform.position);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
