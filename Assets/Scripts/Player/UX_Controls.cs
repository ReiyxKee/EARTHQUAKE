using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UX_Controls : MonoBehaviour
{
    public KeyCode CursorLocker = KeyCode.LeftAlt;
    bool CursorLocked = true;

    // Start is called before the first frame update

    void Start()
    {
        CursorLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(CursorLocker))
        {
            Debug.Log("");
            CursorLocked = !CursorLocked;
        }

        CursorLock();
    }

    private void CursorLock()
    {
        Cursor.lockState = CursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }


}
