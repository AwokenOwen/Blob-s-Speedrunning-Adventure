using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
