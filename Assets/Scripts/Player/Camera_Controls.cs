using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Controls : MonoBehaviour
{
    public CinemachineFreeLook cinemachine;

    public float ZoomSpeed = 2;
    public float ZoomCapMax = 95;
    public float ZoomCapMin = 15;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        if ((Input.GetAxis("Mouse ScrollWheel") != 0))
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if(cinemachine.m_Lens.FieldOfView < ZoomCapMax)
                cinemachine.m_Lens.FieldOfView += ZoomSpeed;
            }
            else
            {
                if (cinemachine.m_Lens.FieldOfView > ZoomCapMin)
                cinemachine.m_Lens.FieldOfView -= ZoomSpeed;
            }
        }


    }
}