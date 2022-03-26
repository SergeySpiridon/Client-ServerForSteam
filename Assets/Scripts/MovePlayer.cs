using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : NetworkBehaviour
{
    private Rigidbody rb;
    [Command]
    public void CmdMove(Vector3 vect)
    {
        //   transform.position = vect;
        Debug.Log("2");
        rb.velocity = vect * 2f;
    }

    public override void OnStartClient()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!hasAuthority)
        //    return;
        //Vector3 vect = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
       // Move();
        
        
    }
    public void Move()
    {
        if (!hasAuthority)
            return;
        Debug.Log("1");
        Vector3 vect = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        CmdMove(vect);
    }
}
