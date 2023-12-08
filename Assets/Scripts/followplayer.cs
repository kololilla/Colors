using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{
    public GameObject follow;
    void Start()
    {
        if(follow == null)
            follow = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (follow == null) 
            follow = GameObject.FindGameObjectWithTag("Player"); 
        this.transform.position = Vector3.MoveTowards(this.transform.position, follow.transform.position, 0.5f * Time.fixedDeltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Megtalaltam!");
        }
    }
}
