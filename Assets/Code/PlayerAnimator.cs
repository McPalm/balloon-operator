using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        var re = GetComponent<Repair>();
        re.OnSwing += Re_OnSwing;
    }

    private void Re_OnSwing()
    {
        Animator.SetTrigger("Swing");
    }
}
