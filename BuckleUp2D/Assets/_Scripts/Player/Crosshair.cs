using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls crosshair vertical movement
/// 
/// Ruben Sanchez
/// 5/24/18
/// </summary>

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Texture2D image;
    [SerializeField] private int size;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;

    private float deltaHeight;
    private InputManager input;
    private float aimInput;

    private void Start()
    {
        input = GameManager.Instance.InputManager;
    }

    private void Update()
    {
        aimInput = Mathf.Lerp(aimInput, input.AimDirection.z, 1f / input.AimDamping.y);
        SetHeight(aimInput * input.AimSensitivity.y);
    }

    public void SetHeight(float amount)
    {
        deltaHeight += amount;

        if (deltaHeight > maxHeight || deltaHeight < minHeight)
            deltaHeight -= amount;
    }

    private void OnGUI()
    {
        Vector3 ScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        ScreenPosition.y = Screen.height - ScreenPosition.y - deltaHeight;
        GUI.DrawTexture(new Rect(ScreenPosition.x, ScreenPosition.y, size, size), image);
    }
}
