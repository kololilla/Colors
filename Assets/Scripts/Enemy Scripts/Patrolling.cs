using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [field: SerializeField] public GameObject LeftPatrolEdge { get; set; }
    [field: SerializeField] public GameObject RightPatrolEdge { get; set; }

    [SerializeField] private GameObject Inner;
    private Transform tr;
    public bool IsActive { get; set; }
    private Vector3 point_goal;

    private void Awake()
    {
        tr = Inner.GetComponent<Transform>();
    }

    private void Start()
    {
        Activate();
    }

    private void Update()
    {
        if (IsActive)
        {
            tr.position = Vector3.MoveTowards(tr.position, point_goal, speed * Time.deltaTime);
            if (Vector2.Distance(Inner.transform.position, point_goal) < 1f) // Ha az inner objektum el�rte a c�lpontot, akkor:
            {
                
                if (point_goal == RightPatrolEdge.transform.position)
                {
                    point_goal = LeftPatrolEdge.transform.position;   //�t�ll�tjuk a m�sik pontot c�lnak
                }
                else
                {
                    point_goal = RightPatrolEdge.transform.position;   //�t�ll�tjuk a m�sik pontot c�lnak
                }
                Flip(); // megford�tjuk az inner objektumot a mozg�s ir�ny�ba
            }
        }
    }

    private void Flip()
    {
        Vector3 theScale = Inner.transform.localScale;
        theScale.x *= -1;
        Inner.transform.localScale = theScale;
    }

    public void Activate() 
    {
        // Inicializ�ljuk a mozg�shoz az "Inner" objektumot
        IsActive = true;
        point_goal = RightPatrolEdge.transform.position; // Kezd� c�lnak megadjuk a bal sz�l�t a mozg�snak
        tr.position = Vector3.MoveTowards(tr.position, point_goal, speed * Time.deltaTime);
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(190, 0, 170);
        Gizmos.DrawSphere(LeftPatrolEdge.transform.position, 0.2f);
        Gizmos.DrawSphere(RightPatrolEdge.transform.position, 0.2f);
        Gizmos.DrawLine(LeftPatrolEdge.transform.position, RightPatrolEdge.transform.position);

        if (LeftPatrolEdge.transform.position.x > RightPatrolEdge.transform.position.x)
            Debug.LogError("Left edge must be smaller than right edge");
    }
}

