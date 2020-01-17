using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorFlawSizeChanger : MonoBehaviour
{
    public Vector3 newSize_ = new Vector3(3,3,3);
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player_ins")
        {
            other.transform.localScale = newSize_;
        }    
    }
}
