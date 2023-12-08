using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClimbable : MonoBehaviour
{
    [SerializeField] CharacterStickManController player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Climbable":
                player.CanClimb = true;
                break;

            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Climbable":
                player.CanClimb = false;
                player.LeftClimbable();
                break;

            default:
                break;
        }
    }
}
