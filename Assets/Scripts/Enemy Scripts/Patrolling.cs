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
            if (Vector2.Distance(Inner.transform.position, point_goal) < 1f) // Ha az inner objektum elérte a célpontot, akkor:
            {
                
                if (point_goal == RightPatrolEdge.transform.position)
                {
                    point_goal = LeftPatrolEdge.transform.position;   //átállítjuk a másik pontot célnak
                }
                else
                {
                    point_goal = RightPatrolEdge.transform.position;   //átállítjuk a másik pontot célnak
                }
                Flip(); // megfordítjuk az inner objektumot a mozgás irányába
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
        // Inicializáljuk a mozgáshoz az "Inner" objektumot
        IsActive = true;
        point_goal = RightPatrolEdge.transform.position; // Kezdõ célnak megadjuk a bal szélét a mozgásnak
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

