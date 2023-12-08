using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Scene Settings")]
public class SceneSettings : ScriptableObject
{
    [field: SerializeField] public int LevelIdx { get; private set; }
    [field: Header("Player's settings")] //---------------------------------------------------------------
    [field: SerializeField] public int P_Health_MaxValue { get; private set; }
    [field: SerializeField, Min(0.5f)] public float P_JumpForce { get; set; } = 1;
    [field: SerializeField, Min(0.5f)] public float P_RunSpeed { get; set; } = 1;
    [field: SerializeField, Min(0.5f)] public float P_ClimbSpeed { get; set; } = 1;

    [field: SerializeField] public GameSettings.EvolutionEnum P_evolution { get; set; }
    [field: SerializeField] public float P_speedGain { get; set; }
    [field: SerializeField] public float P_jumpGain { get; set; }

    [field: Header("Environment settings")] //------------------------------------------------------------
    [field: SerializeField] public bool FallDamage { get; private set; }
    [field: SerializeField] public float FallDamageLimit { get; private set; } = 0;
    [field: SerializeField] public float Gravity { get; private set; } = 1;


    [field: Header("Visual settings")] //-----------------------------------------------------------------
    [field: SerializeField] public Sprite Bcg_sprite { get; private set; }

    [field: Header("Audio settings")] //------------------------------------------------------------------
    [field: SerializeField] public AudioClip BgMusic { get; private set; }
    [field: SerializeField] public AudioClip P_JumpSound { get; private set; }

}
