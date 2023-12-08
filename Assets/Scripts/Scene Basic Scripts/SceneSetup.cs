using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSetup : MonoBehaviour
{
    public static SceneSetup Instance { get; private set; }


    [field: SerializeField] public SceneSettings sceneSO { get; set; }
    [field: SerializeField] public GameSettings gSO { get; set; }
    [field: SerializeField] protected GameManager gameManager;
    [field: SerializeField] public GameObject PlayerObject { get; set; }
    [field: SerializeField] public SpriteRenderer BcgObject { get; set; }
    [field: SerializeField] public HealthBar HealthBar { get; set; }

    [field: SerializeField] public Color Color { get; set; }


    private EdgeCollider2D edgeCollider;
    [field: SerializeField] public int E_Left { get; private set; } = -20;          //A p�lya bal sz�l�nek hat�r�rt�ke
    [field: SerializeField] public int E_Right { get; private set; } = 20;          //A p�lya jobb sz�l�nek hat�r�rt�ke
    [field: SerializeField] public int E_Top { get; private set; } = 10;            //A p�lya fels� sz�l�nek hat�r�rt�ke
    [field: SerializeField] public int E_Bottom { get; private set; } = -10;        //A p�lya als� sz�l�nek hat�r�rt�ke
    [field: SerializeField] public Vector3 SpawnPoint { get; set; } = Vector3.zero; // A j�t�kos �jra�led�si pontja

    private AudioSource bcg_musicSource;
    

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        InitialSetup();
        SetupEdges();
        SetupPlayer();
        

        BcgObject.sprite = sceneSO.Bcg_sprite; //H�tt�rk�p be�ll�t�sa
        bcg_musicSource = GetComponent<AudioSource>(); //H�tt�rzene be�ll�t�sa
        bcg_musicSource.clip = sceneSO.BgMusic;
        bcg_musicSource.Play();
    }

    protected virtual void InitialSetup() 
    {
        Debug.Log("Started Setiing up scene");
    }

    private void Update()
    {
        if (PlayerObject == null)
        {
            FindPlayerObject();
        }
    }

    private void SetupEdges()
    {
        List<Vector2> verticies = new List<Vector2>(); //L�trehozom a p�lya sz�l�nek megfelel� ponthat�rokat
        verticies.Add(new Vector2(E_Left, E_Bottom));
        verticies.Add(new Vector2(E_Left, E_Top));
        verticies.Add(new Vector2(E_Right, E_Top));
        verticies.Add(new Vector2(E_Right, E_Bottom));
        verticies.Add(new Vector2(E_Left, E_Bottom));
        edgeCollider = gameObject.AddComponent<EdgeCollider2D>(); //L�trehozom a p�lya hat�r�hoz a collidert
        edgeCollider.points = verticies.ToArray(); //Be�ll�tom a p�lya sz�l�n l�v� collidert a ponthat�rok alapj�n, hogy a j�t�kos ne tudjon t�lll�pni a j�t�kt�ren
    }

    public virtual void SetupPlayer()
    {
        for (int i = 0; i < gSO.EvolutionStates.Length; i++)
        {
            //Debug.Log($"i:{i} �s prefab: {gSO.EvolutionStates[i].Evolution}");
            if (gSO.EvolutionStates[i].Evolution == sceneSO.P_evolution)
            {
                GeneratePlayer(gSO.EvolutionStates[i].Prefab);
            }
        }

        if (PlayerObject == null)
        {
            FindPlayerObject();
        }
    }

    protected void GeneratePlayer(GameObject prefab) 
    {
        Destroy(PlayerObject);
        PlayerObject = Instantiate(prefab, SpawnPoint, Quaternion.identity);
        PlayerObject.name = "Player";
        PlayerObject.GetComponent<Rigidbody2D>().gravityScale = sceneSO.Gravity;
    }
    public GameObject FindPlayerObject()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        return PlayerObject;
    }

    internal void FinishedLevel()
    {
        gameManager.FinishedLevel(sceneSO.P_evolution, sceneSO.P_speedGain, sceneSO.P_jumpGain);
        gameManager.LoadLevel(0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(E_Left, E_Top, -5),  new Vector3(E_Right, E_Top, -5));
        Gizmos.DrawLine(new Vector3(E_Left, E_Bottom, -5), new Vector3(E_Right, E_Bottom, -5));
        Gizmos.DrawLine(new Vector3(E_Left, E_Bottom, 10), new Vector3(E_Left, E_Top, 10));
        Gizmos.DrawLine(new Vector3(E_Right, E_Bottom, 0), new Vector3(E_Right, E_Top, 0));
        Gizmos.DrawSphere(SpawnPoint, 0.2f);
    }
}
