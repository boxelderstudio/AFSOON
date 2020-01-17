using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistanceController : MonoBehaviour
{
    public GameObject mainCam;
    public float distance_ = 40f;
    public float speed_in_ = 0.03f;
    public float speed_out_ = 0.03f;

    float camDist, oldDist;
    bool playerIsIn_ = false;

    void Awake()
    {
        mainCam = GameObject.FindWithTag("MainCamera");
        oldDist = mainCam.GetComponent<CameraFollow>().dist_mult_;
    }
    void Update()
    {
        if (playerIsIn_ == true)
        {
            mainCam.GetComponent<CameraFollow>().dist_mult_ = Mathf.Lerp(mainCam.GetComponent<CameraFollow>().dist_mult_, distance_, speed_in_);
        }
        else if (playerIsIn_ == false)
        {
            mainCam.GetComponent<CameraFollow>().dist_mult_ = Mathf.Lerp(mainCam.GetComponent<CameraFollow>().dist_mult_, oldDist, speed_out_);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            playerIsIn_ = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
            playerIsIn_ = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
