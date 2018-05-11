using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slows down rigidbodies in the scene
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class SlowDownManager : MonoBehaviour
{
    public static SlowDownManager Instance;

    [SerializeField]
    private float slowScale = .2f;

    private void Awake()
    {
        Instance = this;
    }

    public void SlowDown()
    {
        Time.timeScale = slowScale;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    public void Return()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
    }
}
