using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    public Vector3 lastPos;
    // public GameObject player_;
    public static CheckPointManager singeltone_;

    void Awake()
    {
        lastPos = GameObject.FindWithTag("Player").transform.position;
        
        if (singeltone_ == null)
        {
            singeltone_ = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}
