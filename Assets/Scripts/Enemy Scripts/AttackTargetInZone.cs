using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTargetInZone : MonoBehaviour
{
    [SerializeField] public bool IsActive { get; set; }

    private Transform followed;
    [SerializeField] private GameObject playerObject;

    [SerializeField] private float attackForce;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject inner; //Az objektum, amit mozgatunk és forgatunk
    [SerializeField] private Transform mouth; //Az objektum, amit mozgatunk és forgatunk
    [SerializeField] private Collider2D triggerColider;

    private Transform tr;
    private Vector3 startPoint;
    private bool isTriggered = false;


    private void Awake()
    {
        tr = inner.GetComponent<Transform>();
    }

    private void Start()
    {   
        playerObject = GameObject.FindGameObjectWithTag("Player");
        followed = playerObject.GetComponent<Transform>();
    }


    void Update()
    {
        if (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            followed = playerObject.GetComponent<Transform>();
        }
        if (IsActive)
        {
            if (isTriggered && !HasReached())
            {
                Rotate(followed.position);
                GetToDestination(followed.position);
            }
            else if (tr.position != startPoint)
            {
                TurnBack();
            }
        }
    }

    public void Activate(Vector3 goal)
    {
        startPoint = goal;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private bool HasReached()
    {
        return ((Mathf.Abs(mouth.position.x - followed.position.x) < 0.1) && (Mathf.Abs(mouth.position.y - followed.position.y) < 0.1));
    }

    private void TurnBack()
    {
        isTriggered = false;
        GetToDestination(startPoint);
        if (tr.position == startPoint)
        {
            Reset();
            Deactivate();
        }
    }

    private void GetToDestination(Vector3 dest)
    {
        tr.position = Vector3.MoveTowards(tr.position, dest, speed * Time.deltaTime);
        if (HasReached())
        {
            Attack();
        }
    }

    private void Reset()
    {
        Rotate(startPoint);
        tr.rotation = Quaternion.identity;
    }

    private void Rotate(Vector3 dest)
    {
        Vector2 dir = dest - tr.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        tr.rotation = Quaternion.Slerp(tr.rotation, rotation, rotationSpeed * Time.deltaTime);
    }



    private void Attack()
    {
        playerObject.GetComponent<CharacterControllerBase>().UnderAttack(attackForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Triggerelt belépett");
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Triggerelt elhagyott");
            isTriggered = false;
        }
    }
}
