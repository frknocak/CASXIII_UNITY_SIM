using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwap : MonoBehaviour
{
    public Camera birdEyeCam;
    public Camera snapCam;
    public Camera underCam;
    public Camera tpsCam;

    private bool isBirdEye = true;
    private bool isTransitioning = false;

    void Start()
    {
        isTransitioning = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && !isTransitioning)
        {
            isTransitioning = true;
            if (isBirdEye)
                StartCoroutine(SwitchCamera(birdEyeCam, snapCam ));
            else
                StartCoroutine(SwitchCamera(snapCam, birdEyeCam ));
            isBirdEye = !isBirdEye;
        }
    }

    IEnumerator SwitchCamera(Camera camToEnable, Camera camToDisable1 )
    {
        camToEnable.depth = 1;
        camToDisable1.depth = 0;
        yield return new WaitForSeconds(0.1f); 
        isTransitioning = false;
    }
}
