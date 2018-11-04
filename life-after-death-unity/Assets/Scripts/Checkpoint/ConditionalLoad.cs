using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalLoad : MonoBehaviour
{
    [SerializeField]
    string flag;

    void Start()
    {
        if (GameController.ProgresionFlags.HasFlag(flag))
            gameObject.SetActive(false);
    }
}
