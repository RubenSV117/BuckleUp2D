using UnityEngine;

/// <summary>
/// Manages player animations
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Play(string stateName)
    {
        anim.Play(stateName);
    }
}
