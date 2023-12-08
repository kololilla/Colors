using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class CharacterControllerBase : MonoBehaviour
{
    protected SceneSetup Scene = SceneSetup.Instance;

    protected Rigidbody2D rbody;
    protected Animator animat;
    protected HealthScript health;
    protected AudioSource jump_SoundSource;

    protected float jump_Force = 5;
    protected float move_Speed = 5;
    protected bool onGround = false;
    protected float highestOfFalling = 0;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animat = GetComponent<Animator>();
        health = GetComponent<HealthScript>();
        jump_SoundSource = GetComponent<AudioSource>();
        jump_SoundSource.clip = Scene.sceneSO.P_JumpSound;

        jump_Force = Scene.sceneSO.P_JumpForce;
        move_Speed = Scene.sceneSO.P_RunSpeed;
        health.Reset();
    }

    void FixedUpdate()
    {
        if (!IsOnGround())
        {
            if (highestOfFalling < transform.position.y)
            {
                highestOfFalling = transform.position.y;
            }
        }
    }
    protected bool IsOnGround()
    {
        return rbody.velocity.y == 0;
    }
    protected bool IsAnimFinished() // Visszaadja, hogy az aktuális animáció befejeződött e
    {
        return animat.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }

    public abstract void Movement(float horizontal, float vertical, bool jump, bool crouch);

    public void UnderAttack(float damageForce)
    {
        Debug.Log("Meghívódtam: Under Attack");
        TakeDamage(damageForce);
    }

    protected void TakeDamage(float damageForce) 
    {
        Debug.Log($"damage taken {damageForce}, health now: {health.Value}");
        health.Decrease(damageForce);
        if (health.Value <= 0)
        {
            Respawn();
        }
    }

    protected void Respawn() 
    {
        this.rbody.velocity = Vector2.zero;
        this.transform.position = Scene.SpawnPoint;
        highestOfFalling = 0;
        health.Reset();
        Debug.Log($"Respawn, health now: {health.Value}");
    }

    protected void FaceMovementDirection(float horizontal) // -------------- Amikor a mozgás iránya ellenkező a játékos irányával, akkor megfordítja azt az X tengelyen, így az a másik irányba néz. --------------
    {
        if ((0 < horizontal && this.transform.localScale.x < 0) || (horizontal < 0 && 0 < this.transform.localScale.x))
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
        }
    }

    protected void Crouch()
    {
        if (0 <= rbody.velocity.y)
            rbody.velocity = Vector3.zero;
    }

    protected void HandleFallImpact()
    {
        float fell = highestOfFalling - transform.position.y;
        if ( Scene.sceneSO.FallDamageLimit < fell)
        {
            TakeDamage(fell - Scene.sceneSO.FallDamageLimit);
        }
    }
    public abstract bool BeHappy();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("player triggerelte: " + collision.tag);
        switch (collision.tag)
        {
            case "FallDetector":
                Respawn();
                break;

            case "Color":
                BeHappy();
                break;

            case "Collectable":
                // TODO összegyűjteni a tárgyat
                collision.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Ground":
                onGround = true;
                if (Scene.sceneSO.FallDamage == true)
                {
                    HandleFallImpact();
                }
                highestOfFalling = 0;
                break;
            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Ground":
                onGround = false;
                highestOfFalling = transform.position.y;
                break;
            default:
                break;
        }
    }
}
