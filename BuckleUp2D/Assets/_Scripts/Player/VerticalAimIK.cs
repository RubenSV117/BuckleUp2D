using UnityEngine;

/// <summary>
/// Controls IK for aiming movement
/// Gets info from InputManager and Vertical Aim
/// 
/// Ruben Sanchez
/// 5/29/18
/// </summary>

public class VerticalAimRotation : MonoBehaviour
{
    [SerializeField] private Transform rightHandTarget;
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private Transform shoulder;
    [SerializeField] private Transform aimPivot;
    [SerializeField] private InputManager input;
    [SerializeField] private VerticalAim verticalAim; 


    private Animator anim;
    private float mainHandWeight;
    private float offHandWeight;
    private float lookWeight;
    private float bodyWeight;
    private Vector3 lookDirection;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate () 
	{
		
	}

    public void UpdateShoulder()
    {
        lookDirection = verticalAim.targetTransform.position - aimPivot.position;
        MoveShoulder();
        RotateShoulder();
    }

    public void MoveShoulder()
    {
        aimPivot.position = shoulder.position;
    }

    public void RotateShoulder()
    {
        aimPivot.rotation = Quaternion.Slerp(aimPivot.rotation, 
            Quaternion.LookRotation(lookDirection == Vector3.zero ? aimPivot.transform.forward : lookDirection), 
            Time.deltaTime * input.AimSensitivity.y);
    }
}
