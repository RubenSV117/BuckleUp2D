using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

/// <summary>
/// Singleton for managing game events
/// 
/// Ruben Sanchez
/// 5/23/18
/// </summary>

public class GameManager : MonoBehaviour
{
    public event System.Action<Player> OnLocalPlayerJoined; 
    public static GameManager Instance;

    public Player LocalPlayer
    {
        get { return LocalPlayer; }

        set
        {
            LocalPlayer = value;

            if (OnLocalPlayerJoined != null)
                OnLocalPlayerJoined(LocalPlayer);
        }
    }

    public SlowDownManager SlowManager;
    public RespawnManager RespawnManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            SlowManager = GetComponent<SlowDownManager>();
            RespawnManager = GetComponent<RespawnManager>();

            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }
}
