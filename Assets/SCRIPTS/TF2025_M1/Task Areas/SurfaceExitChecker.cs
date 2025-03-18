using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceExitChecker : MonoBehaviour
{
    public bool isInAllowedZone = false;
    public bool large_area_control = false;
    public bool medium_area_control = false;
    public bool small_area_control = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("smallArea"))
        {
            isInAllowedZone = true;
            small_area_control = true;
        }
        if (other.CompareTag("mediumArea"))
        {
            isInAllowedZone = true;
            medium_area_control = true;
        }
        if (other.CompareTag("largeArea"))
        {
            isInAllowedZone = true;
            large_area_control= true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("smallArea"))
        {
            isInAllowedZone = false;
            small_area_control = false;
        }
        if (other.CompareTag("mediumArea"))
        {
            isInAllowedZone = false;
            medium_area_control = false;
        }
        if (other.CompareTag("largeArea"))
        {
            isInAllowedZone = false;
            large_area_control = false;
        }
    }
    
}
