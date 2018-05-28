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
    }

    void Update () 
	{
	    // movement
	    anim.SetBool("moving", input.MoveDirection.magnitude != 0);

        // move direction
        anim.SetFloat("horizontal", input.MoveDirection.x);
	    anim.SetFloat("vertical", input.MoveDirection.z);

     

    }
}
