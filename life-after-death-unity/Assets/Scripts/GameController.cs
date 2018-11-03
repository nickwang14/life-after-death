using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController instance = null;

    ProgresionFlags progressionFlags;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public static ProgresionFlags ProgresionFlags
    {
        get { return instance != null ? instance.progressionFlags : null; }
    }
}
