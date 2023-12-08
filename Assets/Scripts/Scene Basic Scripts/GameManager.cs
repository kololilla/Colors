using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Animator transitionAnim;
    [field: SerializeField] public GameSettings gSO;
    [field: SerializeField] public Gameplay Game;
    [field: SerializeField] public SceneSettings DoorScene;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        if (Game.UnlockedColorIndex == 0)
        {
            StartNewGame();
        }
    }

    private void Start()
    {
        if (Game.UnlockedColorIndex == 0)
        {
            StartNewGame();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SaveProgress();
            Application.Quit();
            Debug.Log("Escetnyomott");
            EditorApplication.isPlaying = false;
        }
    }

    private void SaveProgress() 
    {
        DataJSonHandler datahandler = new DataJSonHandler();
        datahandler.SaveData<Gameplay>("/save.json", Game);
        Debug.Log("Mentés");
    }

    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(Load(levelIndex));
    }

    private IEnumerator Load(int levelIndex) //Coroutine
    {
        transitionAnim.SetTrigger("StartLoading");
        yield return new WaitForSeconds(gSO.LoadTransitionTime);
        SceneManager.LoadScene(levelIndex);
    }
    
    private void StartNewGame()
    {
        DoorScene = Instantiate(gSO.CleanDoorSettings);
    }

    public void FinishedLevel(GameSettings.EvolutionEnum item, float P_speedGain, float P_jumpGain)
    {
        if(Game.UnlockedColorIndex < gSO.LevelsCount)
            Game.UnlockedColorIndex = Game.UnlockedColorIndex + 1;
        Game.LastEvolutionState = item;
        DoorScene.P_evolution = item;
        DoorScene.P_RunSpeed = P_speedGain;
        DoorScene.P_JumpForce = P_jumpGain;
    }
}
