using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UX_Controls : MonoBehaviour
{
    public KeyCode CursorLocker = KeyCode.LeftAlt;
    public bool CursorLocked = true;

    public Player_Movement movement;

    public GameObject ClimbUp;
    public GameObject ClimbCatch;
    public GameObject Climbing;

    // Start is called before the first frame update

    void Start()
    {
        CursorLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        ClimbUp.SetActive(movement.isClimbWall && !movement.isClimbing && movement.isGrounded ? true :false);

        ClimbCatch.SetActive((movement.isClimbWall && !movement.isGrounded && !movement.isClimbing) ? true :false);

        Climbing.SetActive((movement.isClimbWall && movement.isClimbing) ? true :false);

        if (Input.GetKeyDown(CursorLocker))
        {
            CursorLocked = !CursorLocked;
        }

        CursorLock();
    }

    private void CursorLock()
    {
        Cursor.lockState = CursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }


}
