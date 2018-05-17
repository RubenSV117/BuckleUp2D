using System.Collections;
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

        StartCoroutine(ReturnTimeCo());
    }

    public void ReturnTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
    }

    public IEnumerator ReturnTimeCo()
    {
        yield return new WaitForSecondsRealtime(2);
        ReturnTime();
    }


    private void Update()
    {
    }
}
