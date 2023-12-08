using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLerpFollow : MonoBehaviour
{
    
    protected SceneSetup scene;
    [SerializeField] private Camera camera;

    private float cam_widthO;
    private float cam_heightO;

    private Sprite image;
    private float im_width;
    private float im_height;


    private void Start()
    {
        scene = SceneSetup.Instance;
        image = this.GetComponent<SpriteRenderer>().sprite;
        im_width = image.rect.width / image.pixelsPerUnit; // a kép pixelarányos szélessége
        im_height = image.rect.height / image.pixelsPerUnit; // a kép pixelarányos magassága

        cam_widthO = camera.orthographicSize * camera.aspect; // a kamera ortografikus szélessége a felbontásnak megfelelõen  
        cam_heightO= camera.orthographicSize; // a kamera ortografikus magassága
    }

    private void Update()
    {
        float Lerp_horizontal = Mathf.InverseLerp((scene.E_Left + cam_widthO), (scene.E_Right - cam_widthO), camera.transform.position.x);
        float Lerp_vertical = Mathf.InverseLerp((scene.E_Bottom + cam_heightO), (scene.E_Top - cam_heightO), camera.transform.position.y);

        this.transform.position = Vector3.Lerp(new Vector3(scene.E_Left + im_width / 2, transform.position.y, transform.position.z), new Vector3(scene.E_Right - im_width / 2, transform.position.y, transform.position.z), Lerp_horizontal);
        this.transform.position = Vector3.Lerp(new Vector3(transform.position.x, scene.E_Bottom + im_height / 2, transform.position.z), new Vector3(transform.position.x, scene.E_Top - im_height / 2, transform.position.z), Lerp_vertical);
    }
}
