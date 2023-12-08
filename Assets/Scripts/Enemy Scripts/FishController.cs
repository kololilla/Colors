using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public Patrolling patScript;
    public AttackTargetInZone attackScript;

    private void Start()
    {

        attackScript.Deactivate();
        patScript.Activate();
    }

    private void Update()
    {
        if (attackScript.IsActive && patScript.IsActive)
            patScript.Deactivate();
        if (!attackScript.IsActive && !patScript.IsActive)
            patScript.Activate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Triggerelt belépett");

            patScript.Deactivate();
            attackScript.Activate(patScript.RightPatrolEdge.transform.position);
        }
    }
}
