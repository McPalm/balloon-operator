using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ICoolable, IStrikeAble
{
    public HealthProps HealthProps;
    public float Lost { get; set; }

    public float HealthLeft => HealthProps.MaxHealth - Lost;
    public float HealthPercent => HealthLeft / HealthProps.MaxHealth;

    public bool IsAlive { get; private set; } = true;
    public bool Cooled { get; set; } = false;

    public event System.Action OnHeal;
    public event System.Action OnRevive;

    

    // Update is called once per frame
    void Update()
    {
        if(IsAlive)
        {
            if (Cooled)
                Lost += HealthProps.Decay * Time.deltaTime * .15f;
            else
                Lost += HealthProps.Decay * Time.deltaTime;
        }
        if (Lost > HealthProps.MaxHealth && IsAlive)
            Kill();
    }

    public void Kill()
    {
        IsAlive = false;
        AudioPool.PlaySound(transform.position, HealthProps.DestroyedAudio);
    }

    public void Heal(float health)
    {
        Lost -= health;
        OnHeal?.Invoke();
        if(Lost <= 0f)
        {
            Lost = 0f;
            if (!IsAlive)
            {
                IsAlive = true;
                OnRevive?.Invoke();
            }
        }
    }

    public void Strike()
    {
        Heal(1f);
    }
}
