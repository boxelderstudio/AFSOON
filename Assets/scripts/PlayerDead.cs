using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDead : MonoBehaviour
{
    GameObject player_;
    [HideInInspector]
    public bool is_dead_ = false;


    // after birth, move character to last checkpoint and then start the game
    void Start()
    {
        if (this.gameObject.tag == "Player")
            gameObject.transform.position = CheckPointManager.singeltone_.lastPos;
    }


    // is character is dead, call Die method
    void Update()
    {
        if (is_dead_ == true)
        {
            Die();
        }
    }


    // dead method + resert level
    public void Die()
    {
        // dead screen
        var HUD = GameObject.Find("HUD");
        HUD.transform.Find("dead-reset").gameObject.SetActive(true);

        // After press enter, reset the scene
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameObject.transform.position = CheckPointManager.singeltone_.lastPos;
        }
    }
}
