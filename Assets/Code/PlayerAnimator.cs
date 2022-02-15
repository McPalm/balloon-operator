using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator Animator;

    public int player = 1;

    // Start is called before the first frame update
    void Start()
    {
        var re = GetComponent<Repair>();
        re.OnSwing += Re_OnSwing;
        Animator.SetLayerWeight(1, player == 1 ? 1f : 0f);
        Animator.SetLayerWeight(2, player == 2 ? 1f : 0f);
    }

    private void Re_OnSwing()
    {
        Animator.SetTrigger("Swing");
    }
}
