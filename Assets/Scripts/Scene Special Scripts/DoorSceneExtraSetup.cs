using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;



public class DoorSceneExtraSetup :  SceneSetup
{
    [field: SerializeField] private Gameplay gameplay;
    [field: SerializeField] private SceneSettings CleanStartScene;

    [field: SerializeField] private Light2D globalLight;
    [field: SerializeField] private Volume ppv;

    protected override void InitialSetup()
    {
        base.InitialSetup();
        sceneSO = gameManager.DoorScene;
        SetUpLighting();
        SetUpColorPP();
    }

    public void SetUpLighting()
    {
        float unlockedPercent = (float)gameplay.UnlockedColorIndex / (float)gSO.LevelsCount;
        switch (gameplay.UnlockedColorIndex)
        {
            case 0:
                globalLight.intensity = 0.3f;
                break;
            case > 0:
                Debug.Log(unlockedPercent);
                globalLight.intensity = Mathf.Lerp(0.3f, 1, unlockedPercent);
                break;
            default:
                break;
        }
    }
    public void SetUpColorPP()
    {
        ppv.profile = gSO.ColorUnlockedProfiles[gameplay.UnlockedColorIndex].PpProfile;
    }
}
