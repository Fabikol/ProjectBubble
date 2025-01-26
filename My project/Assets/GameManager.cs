using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player1, player2;
    public Transform respawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        player1.GetComponent<PlayerControl>().OnDeath += (sender, e) => PlayerDeath(e);
        player2.GetComponent<PlayerControl>().OnDeath += (sender, e) => PlayerDeath(e);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PlayerDeath(PlayerControl player)
    {
        Debug.Log($"{player.gameObject.name} starb");
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.transform.position = respawnPoint.position;
    }
}
