using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTransferZone : MonoBehaviour
{
    bool playerIsInTrigger_ = false;
    public bool resetPlayer_ = false;
    GameObject player_;
    Vector3 playerOriginalSize;


    void Update()
    {
        if(playerIsInTrigger_ == true && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Transfer");
            Transfer();
        }

        // // If player exit from mirror area, reset back its properties
        // if (resetPlayer_ == true)
        // {
        //     player_.gameObject.transform.localScale = playerOriginalSize;
        //     resetPlayer_ = false;
        // }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            playerIsInTrigger_ = true;
            player_ = other.gameObject;
            playerOriginalSize = player_.transform.localScale;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player")
        {
            playerIsInTrigger_ = false;  
        }
    }


    public void Transfer()
    {
        GameObject player_main, player_ins;
        Vector3 p_main_scale, p_ins_scale;

        player_main = GetComponentInParent<Mirror>().p;
        player_ins = GetComponentInParent<Mirror>().p_ins;

        if (player_ins != null)
        {
            p_main_scale = player_main.transform.localScale;
            p_ins_scale = player_ins.transform.localScale;

            player_main.transform.localScale = p_ins_scale;
            player_ins.transform.localScale = p_main_scale;
        }

        // player_main.transform.localScale = Vector3.Lerp(player_main.transform.localScale, p_ins_scale, 0.1f);
        // player_ins.transform.localScale = Vector3.Lerp(player_ins.transform.localScale, p_main_scale, 0.1f);
    }
}
