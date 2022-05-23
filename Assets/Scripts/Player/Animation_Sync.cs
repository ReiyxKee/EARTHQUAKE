using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Animation_Sync : MonoBehaviour
{
    public LRHand lrhand;
    public Player_Movement playerMovement;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalk", playerMovement.isMove);
        animator.SetBool("isRun", playerMovement.isRun);
        animator.SetBool("isJump", playerMovement.isJump);
        animator.SetBool("isGrounded", playerMovement.isGrounded);
        animator.SetBool("LeftHand", lrhand.Left);
        animator.SetBool("isClimbMode", playerMovement.isClimbing);
        animator.SetBool("isClimb", playerMovement.isClimbing_Anim);
        animator.SetBool("Climbing_Up", playerMovement.isClimbing_Up );
        animator.SetBool("SideWall_Jump", playerMovement.sideJump);
    }
}
