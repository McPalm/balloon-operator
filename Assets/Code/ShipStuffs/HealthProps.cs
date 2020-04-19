using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName ="New Healthprops", menuName ="Health Props", order =0)]
public class HealthProps : ScriptableObject
{
    public float MaxHealth;
    public float Decay;
    public float SpeedContribution;
    public float FloatContribution;
    public AudioClip DestroyedAudio;
}
