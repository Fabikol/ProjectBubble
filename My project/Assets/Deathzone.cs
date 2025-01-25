using UnityEngine;

public class Deathzone : MonoBehaviour
{
    
    public int Player1lives = 3;
    public int Player2lives2 = 3;

    public Transform Respawnpoint;

    

    public void OnTriggerEnter(Collider other)
    {
       
        
            Debug.Log("Player collided with Deathzone");
            Player1lives--;
            PlayerControl controls = other.GetComponent<PlayerControl>();
            controls.Respawn(Respawnpoint);
            
        
    }
}
