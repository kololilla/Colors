using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    [field: SerializeField] public SceneSettings CleanDoorSettings { get; set; }
    [field: SerializeField] public float CameraSmoothTime { get; set; } = 0.3f;
    [field: SerializeField] public float LoadTransitionTime { get; set; } = 0.3f;


    [field: SerializeField] public int LevelsCount { get; set; } = 0;

    [field: SerializeField] public DoorColorSettings[] ColorUnlockedProfiles;

    [field: SerializeField] public PlayerEvolutions[] EvolutionStates;

    [Serializable] public struct PlayerEvolutions
    {
        public EvolutionEnum Evolution;
        public GameObject Prefab;
    }

    [Serializable] public struct DoorColorSettings
    {
        public int DoorIndex;
        public VolumeProfile PpProfile;
    }

    public enum EvolutionEnum
    {
        Ball,
        StickMan
    }
}
