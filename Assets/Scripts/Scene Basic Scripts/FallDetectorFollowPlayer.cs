using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetectorFollowPlayer : MonoBehaviour
{
    private SceneSetup scene;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource Falling_audio;
    private void Start()
    {
        scene = SceneSetup.Instance;
        this.transform.position = new Vector3(this.transform.position.x, scene.E_Bottom + 1f, this.transform.position.z);
    }

    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Falling_audio.Play();
        }
    }
}