using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageProgress : MonoBehaviour
{
    public Ship ship;
    public Text SpeedoMeter;

    public GameObject VictorySplash;

    public float TargetDistance = 100f;
    
    float distance;

    public Slider ProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        ProgressBar.maxValue = TargetDistance;
    }

    // Update is called once per frame
    void Update()
    {
        distance += ship.Speed * Time.deltaTime;
        SpeedoMeter.text = $"{ship.Speed} KM/H";
        ProgressBar.SetValueWithoutNotify(distance);
        if(distance > TargetDistance)
            Win();
    }

    void Win()
    {
        ship.ShipParts.ForEach(h => h.enabled = false);
        ship.enabled = false;
        VictorySplash.SetActive(true);
        enabled = false;
        GetComponent<StageAdvancer>().NextStageWithDelay(3f);
    }
}
