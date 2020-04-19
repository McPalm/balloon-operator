using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour, ICoolable, IStrikeAble
{
    public int count = 0;

    public GameObject[] targets;
    public SpriteRenderer[] pipes1;
    public SpriteRenderer[] pipes2;

    public Color WaterColour;

    public bool Cooled { get; set; }

    // Update is called once per frame
    void Update()
    {
        targets[count].GetComponent<ICoolable>().Cooled = Cooled;
        if (count == 0)
            pipes1.ForEach(s => s.color = Cooled ? WaterColour : Color.black);
        if (count == 1)
            pipes2.ForEach(s => s.color = Cooled ? WaterColour : Color.black);
    }

    public void Strike()
    {
        count++;
        count %= 2;
        foreach (var obj in targets)
            obj.GetComponent<ICoolable>().Cooled = false;
        foreach (var sprite in pipes1)
            sprite.color = Color.black;
        foreach (var sprite in pipes2)
            sprite.color = Color.black;
    }
}
