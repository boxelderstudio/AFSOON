using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    public Vector3 mirror_direction_;
    mirrorData mirrorData;
    [HideInInspector]
    public GameObject p, p_ins;

    private void OnTriggerEnter(Collider other) 
    {
        if( other.gameObject.tag == "Player" )
        {
            p = other.gameObject;
            Vector3 p_refl_pos;

            p_refl_pos = MirrorManager.Obj_Refl_Pos(mirror_direction_, p.transform.position);

            // instatiate reflected player
            p_ins = GameObject.Instantiate( p.gameObject, p_refl_pos, Quaternion.identity );  

            // change reflected player tag             
            p_ins.gameObject.tag = "Player_ins";   
            p_ins.gameObject.layer = LayerMask.NameToLayer("Player_ins");

            // disable the "Player_Movement" in reflected player
            p_ins.gameObject.GetComponent<PlayerMovement>().enabled = false;              

            // now we know that the main player is in the mirror area 
            p.gameObject.GetComponent<PlayerMovement>().IsInTheMirror = true;            
                                                                      
            var last_pos_ins = p_ins.transform.position;
            var last_pos_main = p.transform.position;

            var p_rb = p.gameObject.GetComponent<Rigidbody>();
            var p_ins_rb = p_ins.gameObject.GetComponent<Rigidbody>();

            enabled = true;
            
            mirrorData = new mirrorData (p_ins, p, p_ins_rb,p_rb, last_pos_ins, last_pos_main, mirror_direction_);
            MirrorManager.singletone.AddMirror(mirrorData);
        }
    }

    ////////////////////////////////////////BLOCKER////////////////////////////////////////
    // If player-main Ray hits the Blocker, disable collider and renderer of the player-ins 
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;
            int layer_mask = LayerMask.GetMask("Blocker");

            Ray ray = new Ray(other.transform.position, -mirror_direction_);

            if (Physics.Raycast(ray, out hit, 10, layer_mask))
            {        
                if (hit.collider != null)
                {
                    p_ins.gameObject.GetComponent<BoxCollider>().enabled = false;
                    p_ins.gameObject.GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    p_ins.gameObject.GetComponent<BoxCollider>().enabled = true;
                    p_ins.gameObject.GetComponent<Renderer>().enabled = true; 
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {   
        if (other.gameObject.tag == "Player")
        {
            enabled = false;

            MirrorManager.singletone.RemoveMirror(mirrorData);
            mirrorData.DestroyPlayer();

            // enable the "Player_Movement" out of mirror
            p.gameObject.GetComponent<PlayerMovement>().IsInTheMirror = false;

            // disable player transfer/deformation
            GetComponentInChildren<TransferZone>().resetPlayer_ = true;
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireCube(transform.position, transform.localScale);
    // }
}


