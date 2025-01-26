using System.Collections.Generic;
using Mono.Cecil;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player1, player2, hud;
    
    public Canvas victoryScreen;
    public Image vSBackground;
    public TMP_Text vSMessage;
    public Color blueColor;
    public Color pinkColor;
    
    
    public Transform respawnPoint;

    private HUD _hudScript;
    private int[] _playerLives = { 3, 3 };
    
    private 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        player1.GetComponent<PlayerControl>().OnDeath += (sender, e) => PlayerDeath(e);
        player2.GetComponent<PlayerControl>().OnDeath += (sender, e) => PlayerDeath(e);

        _hudScript = hud.GetComponent<HUD>();
        victoryScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PlayerDeath(PlayerControl player)
    {
        FindObjectOfType<AudioManager>().Play("Death");
        Debug.Log($"{player.gameObject.name} starb");
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.transform.position = respawnPoint.position;

        int deadPlayer = player.gameObject == player1 ? 0 : 1;
        
        _playerLives[deadPlayer] -= 1;
        print(_playerLives[deadPlayer] + " lives Remaining");
        _hudScript.OnLiveLost(deadPlayer);

        if (_playerLives[deadPlayer] <= 0)
        {
            print("Game Over!");
            if (deadPlayer == 1)
            {
                vSBackground.GetComponent<Image>().color = blueColor;
                vSMessage.text = "Player 1 Won!";
            }
            else
            {
                vSBackground.GetComponent<Image>().color = pinkColor;
                vSMessage.text = "Player 2 Won!";
            }
            victoryScreen.enabled = true;

            /*
            yield return new WaitForSeconds(2);

            _playerLives[0] = _playerLives[1] = 3;

            _hudScript.OnRestart();
            victoryScreen.enabled = false;
            */
        }
        
    }
}
