using System;
using System.Runtime.CompilerServices;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

/// <summary>
/// Singleton GameManager
/// 
/// Ruben Sanchez
/// 6/7/18
/// </summary>

public class GameManager
{
    public delegate void LocalPlayerInit(Player p);
    public event LocalPlayerInit OnLocalPlayerJoined;

    private static GameManager mInstance;
    public static GameManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new GameManager();
                mInstance.gameObj = new GameObject("GameManager");
                mInstance.gameObj.AddComponent<TouchInputManager>();
            }

            return mInstance;
        }
    }

    private Player mLocalPlayer;
    public Player LocalPlayer
    {
        get
        {
            return mLocalPlayer;
        }

        set
        {
            mLocalPlayer = value;

            if(OnLocalPlayerJoined != null)
                OnLocalPlayerJoined.Invoke(mLocalPlayer);
        }
    }

    private GameObject gameObj;
    private TouchInputManager mInput;
    public TouchInputManager Input
    {
        get
        {
            if (mInput == null)
                mInput = gameObj.GetComponent<TouchInputManager>();

            return mInput;
        }
    }
}
