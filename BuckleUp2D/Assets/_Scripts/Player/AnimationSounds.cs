using UnityEngine;

/// <summary>
/// Sounds for animation events
/// Ruben Sanchez
/// 
/// </summary>

public class AnimationSounds : MonoBehaviour
{
    [SerializeField] private AudioClip footStep;
    [SerializeField] private AudioClip rollSound;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FootStep()
    {
        audioSource.PlayOneShot(footStep);
    }

    public void RollSound()
    {
        audioSource.PlayOneShot(rollSound);
    }
}
