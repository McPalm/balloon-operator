using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CloudPacer : MonoBehaviour
{
    Ship Ship;

    public PlayableDirector PlayableDirector;

    // Start is called before the first frame update
    void Start()
    {
        Ship = FindObjectOfType<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Ship.Speed * .25f - .2f;
        PlayableDirector.time += speed * Time.deltaTime;
        if (PlayableDirector.time > PlayableDirector.duration)
            PlayableDirector.time -= PlayableDirector.duration;
    }
}
