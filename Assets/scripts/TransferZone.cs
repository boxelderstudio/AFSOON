using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransferZone : MonoBehaviour
{
    // resets player changes
    public bool resetPlayer_ = false;
    bool transfered_ = false;
    bool firstEnterHappened = false;
    Vector3 playerOriginalSize = new Vector3(.3f,.3f,.3f);



    private void Update() 
    {
        // if palyer exit from mirror area, reset changes
        if (resetPlayer_ == true)
        {
            if (transfered_ == true)
            {
                GetComponentInParent<Mirror>().p.transform.localScale = playerOriginalSize;
                transfered_ = false;
            }
            resetPlayer_ = false;
        }
    }


    // Get player original size
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player" && firstEnterHappened == false)
        {
            playerOriginalSize = other.transform.localScale;
            firstEnterHappened = true;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Player")
        {
            Transfer();
        }
    }


    // Transfer method
    public void Transfer()
    {
        Vector3 p_main_scale, p_ins_scale;
        GameObject player_main = GetComponentInParent<Mirror>().p;
        GameObject player_ins = GetComponentInParent<Mirror>().p_ins;

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (player_ins != null)
            {
                p_main_scale = player_main.transform.localScale;
                p_ins_scale = player_ins.transform.localScale;

                player_main.transform.localScale = p_ins_scale;
                player_ins.transform.localScale = p_main_scale;

                transfered_ = true;
                Debug.Log("Transfer");

                // player_main.transform.localScale = Vector3.Lerp(player_main.transform.localScale, p_ins_scale, 0.1f);
                // player_ins.transform.localScale = Vector3.Lerp(player_ins.transform.localScale, p_main_scale, 0.1f);
            }
        }
    }
}
