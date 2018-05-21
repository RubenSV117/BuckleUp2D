using UnityEngine;

/// <summary>
/// Detects grounded state
/// 
/// Ruben Sanchez
/// 5/21/18
/// </summary>

public class GroundDetector : MonoBehaviour
{
    public static bool isGrounded;

    [SerializeField] private LayerMask groundLayerMask;
	
	void Update ()
	{
	    Collider[] hit = Physics.OverlapSphere(transform.position, .2f, groundLayerMask);

	    isGrounded = hit.Length != 0;
	}
}
