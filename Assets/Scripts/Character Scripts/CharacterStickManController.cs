using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStickManController : CharacterControllerBase
{
    //Variables -------------------------------------------------------------------
    private float move_prevY_vel = 0;
    private bool climbing = false;

    //Properties ------------------------------------------------------------------
    public bool CanClimb { get; set; } = false;

    //==============================================================================
    //Methods ---------------------------------------------------------------------
    public override void Movement(float horizontal, float vertical, bool jump, bool crouch)
    {
        if (crouch)
        {
            Crouch();
        }
        else //Ha nem guggol épp, csak akkor tud ugrani, vagy mozogni
        {
            if (jump && (IsOnGround() || climbing)) //Ugrik
            {
                Jump(jump_Force);
            }
            else if (CanClimb && vertical > 0)
            {
                Climb();
            }
            else
            {
                if (IsOnGround()) //Földön tartózkodik
                {
                     if (horizontal != 0) // Mozog
                    {
                        animat.Play("PlayerRunning");
                    }
                    else if (move_prevY_vel != 0)    //Nem mozog, elõzõleg nem tartózkodott a földön
                    {
                        animat.Play("PlayerLanding"); //Leérkezik
                    }
                    else if (horizontal == 0 && vertical == 0 && IsAnimFinished())
                    {
                        Idle();
                    }
                }

                Move(horizontal);
            }
        }
        move_prevY_vel = rbody.velocity.y;
    }
    
    private void Idle() 
    {
        animat.Play("PlayerIdle");
    }
    
    private void Move(float horizontal) 
    {
        Vector2 m_Velocity = Vector2.zero;
        rbody.velocity = Vector2.SmoothDamp(rbody.velocity, new Vector2(5f * horizontal * move_Speed, rbody.velocity.y), ref m_Velocity, 0.1f);
        FaceMovementDirection(horizontal);
    }
    
    private void Jump(float value)
    {
        jump_SoundSource.Play();
        rbody.AddForce(new Vector2(0, value), ForceMode2D.Impulse);
        animat.Play("PlayerJumping");
    }
    
    private new void Crouch()
    {
        if (0 <= rbody.velocity.y)
            rbody.velocity = Vector3.zero;
        animat.Play("PlayerCrouching");
    }
    
    private void Climb() 
    {
        climbing = true;
        rbody.velocity = Vector2.zero;
        this.transform.position = new Vector3(Mathf.Floor(this.transform.position.x) + 0.5f, this.transform.position.y, this.transform.position.z);
        Vector2 m_Velocity = Vector2.zero;
        rbody.velocity = Vector2.SmoothDamp(rbody.velocity, new Vector2(0, 20f), ref m_Velocity, 0.1f); //TODO mászási sebességet kiszervezni
        animat.Play("PlayerClimbing");
    }
    
    public void LeftClimbable()
    {
        if (!IsOnGround())
            Jump(jump_Force *2/3);
        climbing = false;
    }

    public override bool BeHappy()
    {
        animat.Play("PlayerHappy");
        return true;
    }
}
