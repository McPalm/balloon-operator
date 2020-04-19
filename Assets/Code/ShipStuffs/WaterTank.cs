using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour
{
    public GameObject target;
    Health Health;
    public SpriteRenderer[] WaterSprites;
    public Color WaterColor;

    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        target.GetComponent<ICoolable>().Cooled = Health.IsAlive;
        WaterSprites.ForEach(s => s.color = Health.IsAlive ? WaterColor : Color.black);
    }
}
