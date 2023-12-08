using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    [SerializeField] private float damageValue = 3;
    [SerializeField] private float coolDownTime = 1;
    
    private CharacterControllerBase player;
    private float nextDamageTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            nextDamageTime = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player == null)
                    player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerBase>();

            if (nextDamageTime < Time.time)
            {
                nextDamageTime = Time.time + coolDownTime;
                player.UnderAttack(damageValue);
            }
        }
    }
}
