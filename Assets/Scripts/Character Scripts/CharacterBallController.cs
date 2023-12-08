using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBallController : CharacterControllerBase
{
    public override void Movement(float horizontal, float vertical, bool jump, bool crouch)
    {
        if (crouch)
        {
            Crouch();
        }
        else //Ha nem guggol épp, csak akkor tud ugrani, vagy mozogni
        {
            if (IsOnGround() && jump) //Földön tartózkodik
            {
                Jump();
            }
            Move(horizontal);
        }
    }

    private void Jump()
    {
        jump_SoundSource.Play();
        rbody.AddForce(new Vector2(0, jump_Force), ForceMode2D.Impulse);
    }

    private void Move(float horizontal) 
    {
        Vector2 m_Velocity = Vector2.zero;
        rbody.velocity = Vector2.SmoothDamp(rbody.velocity, new Vector2(5f * horizontal * move_Speed, rbody.velocity.y), ref m_Velocity, 0.1f);
        FaceMovementDirection(horizontal);
    }

    public override bool BeHappy()
    {
        Debug.Log("Happy!!!");
        rbody.velocity = Vector2.zero;
        animat.Play("PlayerBallHappy");
        return true;
    }
}
