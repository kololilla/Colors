using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button Exit_btn;
    [SerializeField] private Button NewGame_btn;
    [SerializeField] private Button Continue_btn;

    [SerializeField] private Gameplay gameplay;
    
    public void StartGame() 
    {
        Reset();
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        DataJSonHandler datahandler = new DataJSonHandler();
        if (File.Exists(Application.persistentDataPath + "/save.json"))
        {
            gameplay = datahandler.LoadData<Gameplay>("/save.json");
        }
        else 
        {
            Reset();
        }
        SceneManager.LoadScene(0);
    }


    private void Reset()
    {
        gameplay.UnlockedColorIndex = 0;
        gameplay.LastEvolutionState = GameSettings.EvolutionEnum.Ball;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
