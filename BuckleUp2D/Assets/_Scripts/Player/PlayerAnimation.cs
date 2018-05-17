﻿using UnityEngine;

/// <summary>
/// Manages player animations
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rigidB;

    private float verticalFlipThreshold = .8f;

    private string rollRightAnimation = "Roll_Right";
    private string rollLeftAnimation = "Roll_Left";
    private string rollUpAnimation = "Roll_Up";
    private string rollDownAnimation = "Roll_Down";
    private string moveAnimation = "Move"; // temp bobbing anim for movement

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigidB = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetFloat("Speed", rigidB.velocity.magnitude);
    }

    public void PlayRoll(Vector2 direction)
    {
        if (direction.x > 0 && Mathf.Abs(direction.y) < verticalFlipThreshold)
            anim.Play(rollRightAnimation);

        else if (direction.x < 0 && Mathf.Abs(direction.y) < verticalFlipThreshold)
            anim.Play(rollLeftAnimation);

        else if (direction.y > verticalFlipThreshold)
            anim.Play(rollUpAnimation);

        else if (direction.y < -verticalFlipThreshold)
            anim.Play(rollDownAnimation);
    }

}
