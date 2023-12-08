using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    private SceneSetup Scene = SceneSetup.Instance;
    private HealthBar healthBar;

    private float h_value;
    public float Value {
        get { return h_value; }
        set 
        { 
            h_value = value;
            healthBar.SetHealth(Value);
        } 
    }
    
    private void Awake()
    {
        healthBar = Scene.HealthBar;
    }

    public void Reset() 
    {
        Value = Scene.sceneSO.P_Health_MaxValue;
    }

    public void Decrease(float value) 
    {
        Value -= value;
    }

    public void Increase(float value)
    {
        Value -= value;
    }
}
