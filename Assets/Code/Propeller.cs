using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    Ship Ship;

    public Sprite[] sprites;

    SpriteRenderer SpriteRenderer;

    float offset;

    // Start is called before the first frame update
    void Start()
    {
        Ship = FindObjectOfType<Ship>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Ship.Speed * Time.deltaTime;
        var sprite = Mathf.FloorToInt(offset) % sprites.Length;
        SpriteRenderer.sprite = sprites[sprite];
    }
}
