using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    // Start is called before the first frame update
  //  private MyNetworkPlayer player;
    [SerializeField] private GameObject unitSpawnerPrefab = null;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        GameObject unitSpawnerInstantiate = Instantiate
            (unitSpawnerPrefab,conn.identity.transform.position, conn.identity.transform.rotation);
        NetworkServer.Spawn(unitSpawnerInstantiate, conn);
       // player = conn.identity.GetComponent<MyNetworkPlayer>();
    }
}
