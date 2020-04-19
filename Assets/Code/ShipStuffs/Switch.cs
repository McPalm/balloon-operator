using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour, IStrikeAble
{

    public UnityEvent OnStruck;

    public void Strike()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.flipX = !sr.flipX;
        OnStruck.Invoke();
    }
}
