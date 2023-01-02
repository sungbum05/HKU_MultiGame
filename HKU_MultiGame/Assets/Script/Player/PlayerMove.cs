using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float Speed = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            photonView.RPC("SetTransparency", RpcTarget.All);
    }

    void Move()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        this.gameObject.transform.position += new Vector3 (Horizontal, Vertical, 0) * Speed * Time.deltaTime;
    }

    [PunRPC]
    void SetTransparency()
    {
        if(this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        else
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
