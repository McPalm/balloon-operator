using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
    public InputToken InputToken { get; set; }
    public float cooldown = 1f;
    public Collider2D SwingCollider;
    public AudioClip HitAudio;
    public AudioClip FullHealthAudio;

    float nextAvailable;
    bool IsAvailable => nextAvailable < Time.timeSinceLevelLoad;

    public event System.Action OnSwing;

    private void Update()
    {
        if(IsAvailable && InputToken.UseHeld)
        {
            DoThings();
            InputToken.ConsumeUse();
        }
    }

    void DoThings()
    {
        nextAvailable = Time.timeSinceLevelLoad + cooldown;
        OnSwing?.Invoke();
    }

    static RaycastHit2D[] hits = new RaycastHit2D[1];

    public void TriggerRepair()
    {
        var hitN = SwingCollider.Cast(Vector2.zero, hits);
        if (hitN > 0)
        {
            var strike = hits[0].collider.GetComponent<IStrikeAble>();
            strike?.Strike();
            var health = hits[0].collider.GetComponent<Health>();
            if (health)
            {
                AudioPool.PlaySound(transform.position, HitAudio, .7f, .5f + health.HealthPercent);
                if(health.Lost <= 0f)
                    AudioPool.PlaySound(transform.position, FullHealthAudio, .5f);
                
            }
            else
                AudioPool.PlaySound(transform.position, HitAudio, .7f);
        }
    }
}
