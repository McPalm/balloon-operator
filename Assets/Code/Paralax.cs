using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    Ship Ship;

    float offset;

    public float width;


    public Transform root;

    Vector3 startValue;

    public float speedFactor = .01f;

    void Start()
    {
        startValue = root.localPosition;
        Ship = FindObjectOfType<Ship>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offset += Ship.Speed * Time.deltaTime * speedFactor;
        root.transform.localPosition = startValue + Vector3.right * (offset % width);
    }
}
