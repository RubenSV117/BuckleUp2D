using UnityEngine;

/// <summary>
/// Controls player animations
/// Gets input info from InputManager
/// 
/// Ruben Sanchez
/// 5/28/19
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private InputManager input;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        input.OnSprintChange += SetSprint;
        input.OnRoll += Roll;
    }

    public void SetMoving()
    {
        // movement
        anim.SetBool("moving", input.MoveDirection.magnitude != 0);
    }

    public void SetMovementDirection()
    {
        // move direction
        anim.SetFloat("horizontal", input.MoveDirection.x);
        anim.SetFloat("vertical", input.MoveDirection.z);
    }

    private void SetSprint(bool isSprinting)
    {
        anim.SetBool("sprint", isSprinting);
    }

    public void Roll()
    {
        anim.Play("Roll");
    }


}
