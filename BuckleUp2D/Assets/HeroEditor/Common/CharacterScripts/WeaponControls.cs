using System;
using HeroEditor.Common.Enums;
using UnityEngine;

namespace Assets.HeroEditor.Common.CharacterScripts
{
    /// <summary>
    /// Rotates arms and passes input events to child components like FirearmFire and BowShooting.
    /// </summary>
    public class WeaponControls : MonoBehaviour
    {
        public Character Character;
        public Transform ArmL;
        public Transform ArmR;
        public KeyCode ReloadButton;
	    public bool FixHorizontal;

        private bool _locked;
        private InputManager input;

        private void Awake()
        {
            input = GameManager.Instance.Input;
        }

        public void Update()
        {
            _locked = !Character.Animator.GetBool("Ready") || Character.Animator.GetInteger("Dead") > 0;

            if (_locked) return;

            bool isAttacking = input.AimDirection.magnitude != 0;

            switch (Character.WeaponType)
            {
                case WeaponType.Melee1H:
                case WeaponType.Melee2H:
                case WeaponType.MeleePaired:
                    if (isAttacking)
                    {
                        Character.Animator.SetTrigger(Time.frameCount % 2 == 0 ? "Slash" : "Jab"); // Play animation randomly
                    }
                    break;
                case WeaponType.Bow:
                    Character.BowShooting.ChargeButtonDown = isAttacking;
                    Character.BowShooting.ChargeButtonUp = isAttacking;
                    break;
                case WeaponType.Firearms1H:
                case WeaponType.Firearms2H:
                    Character.Firearm.Fire.FireButtonDown = isAttacking;
                    Character.Firearm.Fire.FireButtonPressed = isAttacking;
                    Character.Firearm.Fire.FireButtonUp = isAttacking;
                    Character.Firearm.Reload.ReloadButtonDown = Input.GetKeyDown(ReloadButton);
                    break;
	            case WeaponType.Supplies:
		            if (isAttacking)
		            {
			            Character.Animator.Play(Time.frameCount % 2 == 0 ? "UseSupply" : "ThrowSupply", 0); // Play animation randomly
		            }
		            break;
			}
        }

        /// <summary>
        /// Called each frame update, weapon to mouse rotation example.
        /// </summary>
        public void LateUpdate()
        {
            if (_locked) return;

            Transform arm;
            Transform weapon;

            switch (Character.WeaponType)
            {
                case WeaponType.Bow:
                    arm = ArmL;
                    weapon = Character.BowRenderers[3].transform;
                    break;
                case WeaponType.Firearms1H:
                case WeaponType.Firearms2H:
                    arm = ArmR;
                    weapon = Character.Firearm.FireTransform;
                    break;
                default:
                    return;
            }

            RotateArm(arm, weapon, -180, 180);
        }

        /// <summary>
        /// Selected arm to position (world space) rotation, with limits.
        /// </summary>
        public void RotateArm(Transform arm, Transform weapon, float angleMin, float angleMax) // TODO: Very hard to understand logic
        {
            Vector2 target = transform.right * transform.localScale.x;

            if(GameManager.Instance.Input.AimDirection.magnitude != 0)
                target =  GameManager.Instance.Input.AimDirection;

            else if (GameManager.Instance.Input.MoveDirection.magnitude != 0)
                target = GameManager.Instance.Input.MoveDirection;

            target *= transform.localScale;

            var angleToTarget = Vector2.SignedAngle(Vector2.right, target);
            var angleToFirearm = Vector2.SignedAngle(weapon.right, arm.transform.right) * Math.Sign(weapon.lossyScale.x);
            var angleFix = Mathf.Asin(weapon.InverseTransformPoint(arm.transform.position).y / target.magnitude) * Mathf.Rad2Deg;
            var angle = angleToTarget + angleToFirearm + angleFix;

            angleMin += angleToFirearm;
            angleMax += angleToFirearm;

            var z = arm.transform.localEulerAngles.z;

            if (z > 180) z -= 360;

            if (z + angle > angleMax)
            {
                angle = angleMax;
            }
            else if (z + angle < angleMin)
            {
                angle = angleMin;
            }
            else
            {
                angle += z;
            }

            arm.transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}