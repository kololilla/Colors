using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private SceneSetup scene;
    private Slider slider;
    [field: SerializeField] public Image Im_slider;
    [field: SerializeField] public Image Im_heart;

    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneSetup.Instance;
        maxHealth = scene.sceneSO.P_Health_MaxValue;
        Im_slider.color = scene.Color;
        Im_heart.color = scene.Color;

        slider = GetComponent<Slider>();
        slider.maxValue = maxHealth;
        SetHealth(maxHealth);
    }

    public void SetHealth(float value)
    {
        slider.value = value;
    }
}
