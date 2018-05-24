using UnityEngine;

/// <summary>
/// Player class for Multiplayer
/// Ruben Sanchez
/// 
/// </summary>

public class Player : MonoBehaviour
 {
     private void Awake()
     {
         GameManager.Instance.LocalPlayer = this;
     }
 }
