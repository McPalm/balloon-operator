using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthParticles : MonoBehaviour
{
    public Health Health;
    public ParticleSystem FireParticles;
    public ParticleSystem SmokeParticles;
    public SpriteRenderer Sprite;

    public AudioSource AudioSource;

    float RepairFlash = 0f;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        Health.OnHeal += Health_OnHeal;
    }

    private void Health_OnHeal()
    {
        if (Health.Lost > 0f)
            RepairFlash = 1f;
        else
            RepairFlash = .5f;
    }


    // Update is called once per frame
    void Update()
    {
        if(FireParticles.isPlaying)
        {
            if (Health.HealthPercent > .25f || !Health.IsAlive)
            {
                FireParticles.Stop();
                AudioSource.Stop();
            }
        }
        else
        {
            if (Health.HealthPercent < .25f && Health.IsAlive)
            {
                FireParticles.Play();
                AudioSource.Play();
            }
        }

        if(SmokeParticles.isPlaying)
        {
            if (Health.HealthPercent > .66f)
                SmokeParticles.Stop();
        }
        else
        {
            if (Health.HealthPercent < .66f)
                SmokeParticles.Play();
        }

        AudioSource.volume = Mathf.Clamp01(.25f - Health.HealthPercent);

        Sprite.color = GetColor();
    }

    public Color GetColor()
    {
        var baseColor = Health.IsAlive ? Color.white : Color.black;
        if (RepairFlash > 0f)
        {
            baseColor = Color.Lerp(baseColor, Color.green, RepairFlash);
            RepairFlash -= Time.deltaTime * 5f;
        }
        return baseColor;
    }
}
