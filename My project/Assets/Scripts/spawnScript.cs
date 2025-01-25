using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class spawnScript : MonoBehaviour
{

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void OnPlayerJoined()
    {
        Debug.Log("Player Joined");
        GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            int index = player.GetComponentInChildren<PlayerInput>().playerIndex;
            player.GetComponentInChildren<CinemachineCamera>().OutputChannel = (OutputChannels) (1 << index + 1);
            player.GetComponentInChildren<CinemachineBrain>().ChannelMask = (OutputChannels) (1 << index + 1);
            //player.GetComponentInChildren<CinemachineInputAxisController>().PlayerIndex = index;
        }
    }
}
