using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCollectable : MonoBehaviour
{
    [SerializeField] private GameObject colorPrefab;
    [SerializeField] private Animator animat;

    private void Awake()
    {
        Instantiate(colorPrefab, this.transform);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            SceneSetup.Instance.FinishedLevel();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.position, 0.4f);
    }
}
