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

    private float verticalFlipThreshold = .8f;

    private string rollRightAnimation = "Roll_Right";
    private string rollLeftAnimation = "Roll_Left";
    private string rollUpAnimation = "Roll_Up";
    private string rollDownAnimation = "Roll_Down";
    private string moveAnimation = "Move"; // temp bobbing anim for movement

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
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

    public void PlayWalk(Vector2 direction)
    {
        anim.SetBool("Moving", true);
        anim.Play(moveAnimation);
        // to be used for directional anims
        //if (Mathf.Abs(1 - Vector2.Dot(direction, new Vector2(1, 0))) < flipThreshold)

        //else if (Mathf.Abs(1 - Vector2.Dot(direction, new Vector2(.7f, -.7f))) < flipThreshold)

        //else if (Mathf.Abs(1 - Vector2.Dot(direction, new Vector2(0, -1))) < flipThreshold)

        //else if (Mathf.Abs(1 - Vector2.Dot(direction, new Vector2(-.7f, -.7f))) < flipThreshold)

        //else if (Mathf.Abs(1 - Vector2.Dot(direction, new Vector2(-1, 0))) < flipThreshold)

        //else if (Mathf.Abs(1 - Vector2.Dot(direction, new Vector2(-.7f, .7f))) < flipThreshold)


        //else if (Mathf.Abs(1 - Vector2.Dot(direction, new Vector2(0, 1))) < flipThreshold)


        //else if (Mathf.Abs(1 - Vector2.Dot(direction, new Vector2(.7f, .7f))) < flipThreshold)
    }
}
