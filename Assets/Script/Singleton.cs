using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance_ = null;
    public static T instance
    {
        get
        {
            return instance_;
        }
    }

    [SerializeField]
    private bool is_dont_destroy = false;

    private void Awake()
    {
        if (instance_ == null)
        {
            instance_ = FindObjectOfType<T>();
            if (is_dont_destroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
