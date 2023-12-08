using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Gameplay Instance")]
public class Gameplay : ScriptableObject
{
    [field: SerializeField] public int UnlockedColorIndex { get; set; }
    [field: SerializeField] public GameSettings.EvolutionEnum LastEvolutionState { get; set; }
}
