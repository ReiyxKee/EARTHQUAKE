using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Animation_Sync : MonoBehaviour
{
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
    }
}
