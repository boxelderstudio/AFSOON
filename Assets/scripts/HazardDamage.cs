using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if( other.gameObject.tag == "Player" || other.gameObject.tag == "Player_ins")
        {
            other.GetComponent<PlayerDead>().is_dead_ = true;
        }
    }
}