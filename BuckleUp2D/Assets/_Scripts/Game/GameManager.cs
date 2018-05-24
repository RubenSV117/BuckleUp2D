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

    private Player _localPlayer;
    public Player LocalPlayer
    {
        get { return _localPlayer; }

        set
        {
            _localPlayer = value;

            if (OnLocalPlayerJoined != null)
                OnLocalPlayerJoined(_localPlayer);
        }
    }

    public SlowDownManager SlowManager;
    public RespawnManager RespawnManager;
    public InputManager InputManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            SlowManager = GetComponent<SlowDownManager>();
            RespawnManager = GetComponent<RespawnManager>();
            InputManager = GetComponent<InputManager>();

            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }
}
