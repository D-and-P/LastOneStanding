using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBluePrint : NetworkBehaviour {

    public GameObject PlayerModel;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Debug.Log("Sawning my PlayerModel prefab!");
        CmdSpawnPlayerModel();
    }

    [Command]
    private void CmdSpawnPlayerModel()
    {
        GameObject go = Instantiate(PlayerModel,transform.position,Quaternion.identity,transform) as GameObject;
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
