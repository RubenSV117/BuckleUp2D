using System.Collections.Generic;
using UnityEngine;

namespace Monument
{
    /// <summary>
    ///
    /// Ruben Sanchez
    /// 5/24/18
    /// 
    /// </summary>

    public class AnimationStateManager : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private GameObject activeModel;
        [SerializeField] private Rigidbody rigidB;
        [SerializeField] private Collider controllerCollider;
        [SerializeField] private LayerMask layersToIgnore;
        [SerializeField] private LayerMask groundLayer;

        private List<Collider> ragdollColliders = new List<Collider>();
        private List<Rigidbody> ragdollRigids = new List<Rigidbody>();

        public void Init()
        {
            rigidB = GetComponent<Rigidbody>();
            controllerCollider = GetComponent<Collider>();
            SetUpRagdoll();
        }

        void SetUpRagdoll()
        {
            Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();

            foreach (var r in rigids)
            {
                if (r == rigidB)
                    continue;

                r.isKinematic = true;
                r.gameObject.layer = LayerMask.NameToLayer("Ragdoll");

                Collider c = r.gameObject.GetComponent<Collider>();
                c.isTrigger = true;

                ragdollRigids.Add(r);
                ragdollColliders.Add(c);
            }
        }

        public void FixedTick()
        {

        }

        public void Tick()
        {

        }
       
    }

}


