using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOutline : MonoBehaviour
{
    [SerializeField] private Outline outline;

    public void SetGreen()
    {
        outline.OutlineColor = Color.green;
    }

    public void SetRed()
    {
        outline.OutlineColor = Color.red;
    }
}
