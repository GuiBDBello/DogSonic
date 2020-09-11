using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private bool activated;

    public void SetActivated(bool activated)
    {
        this.activated = activated;
    }

    public bool GetActivated()
    {
        return this.activated;
    }
}
