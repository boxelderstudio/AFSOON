using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorFlawSizeChanger : MonoBehaviour
{
    public Vector3 newSize_ = new Vector3(3,3,3);
    // public float speed_in_ = 0.03f;
    // public float speed_out_ = 0.03f;

    bool playerIsInTrigger_ = false;
    bool weAreDone = false;
    GameObject player_ins;
    
    
    void Update()
    {
        if (player_ins != null)
        {
            if (playerIsInTrigger_ == true && weAreDone == false)
            {
                // player_ins.transform.localScale = Vector3.Lerp(player_ins.transform.localScale, newSize_, speed_in_);
                player_ins.transform.localScale = newSize_;
                weAreDone = true;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player_ins" && weAreDone == false)
        {
            player_ins = other.gameObject;
            playerIsInTrigger_ = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
