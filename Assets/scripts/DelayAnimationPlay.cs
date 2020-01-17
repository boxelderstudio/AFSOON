using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAnimationPlay : MonoBehaviour
{
    public float delayInSecs = 0;
    public string animClip = "hazard_press_anim";
    Animator animator;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        StartCoroutine(playAnimation());
    }

    public IEnumerator playAnimation() 
    {
        yield return new WaitForSeconds(delayInSecs); 
        animator.Play(animClip);
    }
}
