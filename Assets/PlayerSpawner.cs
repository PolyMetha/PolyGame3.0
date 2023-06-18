using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSpawner : NetworkBehaviour
{
    public GameObject ballPlayer;
    public GameObject birdPlayer;
    public GameObject applePlayer;

    public void OnSceneChange()
    {
        if (hasAuthority)
        {
            CmdSpawnPlayer();
        }
    }

    [Command]
    void CmdSpawnPlayer()
    {
        GameObject playerGO = Instantiate(ballPlayer, new Vector3(0, 1, 0), Quaternion.identity);
        NetworkServer.Spawn(playerGO);
    }
}
