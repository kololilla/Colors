using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    private SceneSetup scene = SceneSetup.Instance;
    [SerializeField] private Animator anim;
    [SerializeField] private float calmTime;
    [SerializeField] private float attackForce;


    private float calmCounter = 0; 
    private float attackCounter = 0; 
    private bool isPlayerOnShell = false;
    private CharacterControllerBase player;

    private void Update()
    {

        calmCounter += Time.deltaTime;
        attackCounter += Time.deltaTime;


        anim.SetFloat("calmTime", calmCounter);
        if (calmCounter >= calmTime)
        {
            calmCounter = 0.0f;
        }
        if (attackCounter > calmTime + 2f) 
        {
            if (isPlayerOnShell)
            {
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerBase>();
                }
                player.UnderAttack(attackForce);
            }
            calmCounter = 0.0f;
            attackCounter = 0.0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerOnShell = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerOnShell = false;
        }
    }
}
