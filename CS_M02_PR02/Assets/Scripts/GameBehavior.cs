using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;

    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public bool followerText = false;
    public bool speedLable = false;

    private string speedText = "1.5 times Speed!";
    private string _state;

    private int _itemsCollected = 0;
    private int _followerCount = 0;
    private int _playerHP = 10;
    private int _speedBoostCount = 0;

    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public int Items
    {
        get
        {
            return _itemsCollected;
        }
        set
        {
            _itemsCollected = value;
            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }

    public int Follower
    {
        get
        {
            return _followerCount;
        }
        set
        {
            _followerCount = value;
            followerText = true;
        }
    }

    public int HP
    {
        get
        {
            return _playerHP;
        }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);
            if (_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... that's got to hurt.";
            }
        }
    }

    public int Speed
    {
        get
        {
            return _speedBoostCount;
        }
        set
        {
            _speedBoostCount = value;
            if (_speedBoostCount > 0)
            {
                speedLable = true;
            }
        }
    }

    void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        _state = "Manager initialized..";
        _state.FancyDebug();
        Debug.Log(_state);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel(0);
            }
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                Utilities.RestartLevel();
            }
        }

        if (followerText)
        {
            GUI.Box(new Rect(Screen.width - 170, 20, 150, 25), "You have " + _followerCount + " followers!");
        }

        if (speedLable)
        {
            GUI.Label(new Rect(Screen.width /2 - 100, 20, 150, 25), speedText);
        }
    }
}
