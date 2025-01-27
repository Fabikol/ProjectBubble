using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public RawImage[] blueHearts;
    public RawImage[] pinkHearts;

    private int _blueLives;
    private int _pinkLives;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _blueLives = blueHearts.Length;
        _pinkLives = pinkHearts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLiveLost(int player)
    {
        if (player == 1)
        {
            _pinkLives -= 1;
            pinkHearts[_pinkLives].enabled = false;
        }
        else
        {
            _blueLives -= 1;
            blueHearts[_blueLives].enabled = false;
        }
    }

    public void OnRestart()
    {
        _blueLives = 3;
        _pinkLives = 3;
        
        for (int i = 0; i < 3; i++)
        {
            blueHearts[i].enabled = true;
            pinkHearts[i].enabled = true;
        }
                
    }
}
