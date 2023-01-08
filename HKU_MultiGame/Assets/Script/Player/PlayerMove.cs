using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPunCallbacks
{
    public bool IsTired = false;
    public float StaminaUsage = 5.0f;
    public float BasicSpeed = 0.0f;
    public float Speed = 0.0f;

    [SerializeField]
    public Animator PlayerAnimator;
    [SerializeField]
    private PlayerInfo PlayerInfo;

    private void Start()
    {
        BasicSpeed = Speed;
        PlayerInfo = this.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        if(PlayerInfo != null && PlayerInfo.IsAttack != true && PlayerInfo.IsOpenChest != true && PlayerInfo.IsHide != true)
            Move();
    }

    void Move()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        if (Horizontal == 0 && Vertical == 0)
            PlayerAnimator.SetTrigger("Idle");

        else if(Horizontal > 0)
            PlayerAnimator.SetTrigger("RightMove");

        else if (Horizontal < 0)
            PlayerAnimator.SetTrigger("LeftMove");

        else if (Horizontal == 0 && Vertical > 0)
            PlayerAnimator.SetTrigger("BackMove");

        else if (Horizontal == 0 && Vertical < 0)
            PlayerAnimator.SetTrigger("FrontMove");

        Run();

        this.gameObject.transform.position += new Vector3 (Horizontal, Vertical, 0) * Speed * Time.deltaTime;
    }

    void Run()
    {
        if (PlayerInfo.Type == PlayerType.Runner && PlayerInfo.IsShose == false)
        {
            //스태미나 사용
            if (Input.GetKey(KeyCode.LeftShift) == true && IsTired == false)
            {
                PlayerInfo.Stamina -= StaminaUsage * Time.deltaTime;
                Speed = BasicSpeed + 1.5f;

                if (PlayerInfo.Stamina <= 0)
                {
                    PlayerInfo.Stamina = 0.0f;
                    IsTired = true;
                }
            }

            else
            {
                Speed = BasicSpeed;
            }

            if (PlayerInfo.Stamina < PlayerInfo.MaxStamina)
            {
                PlayerInfo.Stamina += (StaminaUsage / 5.0f) * Time.deltaTime;

                if (PlayerInfo.Stamina > PlayerInfo.MaxStamina / 2)
                    IsTired = false;
            }

            else
            {
                PlayerInfo.Stamina = PlayerInfo.MaxStamina;
            }
        }
    }
}
