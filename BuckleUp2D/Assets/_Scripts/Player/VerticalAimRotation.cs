using UnityEngine;

/// <summary>
/// Controls IK for aiming movement
/// Gets info from InputManager and Vertical Aim
/// 
/// Ruben Sanchez
/// 5/29/18
/// </summary>

public class VerticalAimIK : MonoBehaviour
{
    [SerializeField] private Transform rightHandTarget;
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private Transform shoulder;
    [SerializeField] private Transform aimPivot;
    [SerializeField] private InputManager input;
    [SerializeField] private VerticalAim verticalAim;
    [SerializeField] private float defaultBodyWeight = .4f;

    private Animator anim;
    private float mainHandWeight;
    private float offHandWeight;
    private float lookWeight;
    private float bodyWeight;
    private Vector3 lookDirection;
    [SerializeField] private double minAngle;
    [SerializeField] private double maxAngle;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        shoulder = anim.GetBoneTransform(HumanBodyBones.RightShoulder).transform;
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

    public void SetWeights()
    {
        bodyWeight = defaultBodyWeight;
        offHandWeight = 1;

        float targetLookAtWeight = 0;
        float targetMainHandWeight = 1;

        float angle = Vector3.Angle(transform.forward, lookDirection);

        targetLookAtWeight = angle < minAngle ? 1 : 0;

        if (angle > maxAngle)
            targetMainHandWeight = 0;

        lookWeight = Mathf.Lerp(lookWeight, targetLookAtWeight, Time.deltaTime);
        mainHandWeight = Mathf.Lerp(mainHandWeight, targetMainHandWeight, Time.deltaTime);
    }

    private void OnAnimatorIK()
    {
        SetWeights();

        anim.SetLookAtWeight(lookWeight, bodyWeight, 1, 1, 1);
        anim.SetLookAtPosition(verticalAim.targetTransform.position);

        UpdateIK(AvatarIKGoal.LeftHand, leftHandTarget, offHandWeight);
        UpdateIK(AvatarIKGoal.RightHand, rightHandTarget, mainHandWeight);
    }

    private void UpdateIK(AvatarIKGoal goal, Transform trans, float weight)
    {
        anim.SetIKPositionWeight(goal, weight);
        anim.SetIKRotationWeight(goal, weight);
        anim.SetIKPosition(goal, trans.position);
        anim.SetIKRotation(goal, trans.rotation);
    }
}
