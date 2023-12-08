using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    protected SceneSetup scene;
    private GameObject player;
    private float camSize;
    private Camera cam;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        scene = SceneSetup.Instance;
        cam = Camera.main;
        this.camSize = cam.orthographicSize;

    }
    void Update()
    {
        if (player == null) 
        {
            player = GameObject.FindGameObjectWithTag("Player"); 
        }
        float minXBounds = cam.orthographicSize * cam.aspect;
        float newXPos = Mathf.Clamp(player.transform.position.x, scene.E_Left + minXBounds, scene.E_Right - minXBounds);
        float newYPos = Mathf.Clamp(player.transform.position.y, scene.E_Bottom + this.camSize, scene.E_Top - this.camSize);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(newXPos, newYPos, this.transform.position.z), ref velocity, scene.gSO.CameraSmoothTime);
    }
}
