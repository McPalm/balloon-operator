using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public Health[] ShipParts { get; set; }
    public Transform ShipRoot;
    public Slider HealthBar;

    public float SinkSpeed;

    public float Speed { get; set; }

    float lostHeight;

    public AudioClip ExplodeSound;
    

    // Start is called before the first frame update
    void Start()
    {
        ShipParts = GetComponentsInChildren<Health>();

        foreach (var part in ShipParts)
        {
            SinkSpeed += part.HealthProps.FloatContribution;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lostHeight += SinkSpeed * Time.fixedDeltaTime;
        Speed = 0f;

        foreach (var part in ShipParts)
        {
            if (part.IsAlive)
            {
                lostHeight -= part.HealthProps.FloatContribution * Time.fixedDeltaTime;
                Speed += part.HealthProps.SpeedContribution * (part.Cooled ? 1.5f : 1f);
            }
        }
        lostHeight = Mathf.Clamp(lostHeight, 0, 10f);
        if (lostHeight >= 5f)
            lostHeight += .01f;
        if (lostHeight >= 7f)
        {
            Destroy(gameObject);
            AudioPool.PlaySound(transform.position, ExplodeSound);
            FindObjectOfType<StageAdvancer>().ReplayWithDelay(2f);
        }


        HealthBar.SetValueWithoutNotify(1f - lostHeight / 5f);


        ShipRoot.position = new Vector3(0f, Mathf.Sin(Time.timeSinceLevelLoad) * .1f - (lostHeight * lostHeight * .25f));
    }
}
