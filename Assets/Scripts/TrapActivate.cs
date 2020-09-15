using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivate : Trap
{
    public GameObject[] objectsToActivate;

    private void Update()
    {
        if (GetActivated())
        {
            foreach (GameObject objectToActivate in objectsToActivate)
                objectToActivate.SetActive(true);
        }
    }
}
