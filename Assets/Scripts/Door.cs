using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int sceneNameIdx;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(gameManager.Game.UnlockedColorIndex + 1 == sceneNameIdx)
            {
                print("Switching scene to: " + sceneNameIdx);
                gameManager.LoadLevel(sceneNameIdx);
            }
            print("Locked Door");
        }
    }
}
