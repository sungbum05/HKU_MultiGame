using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public enum PlayerType
{
    Lobby, Runner, Chaser
}

public class PlayerInfo : MonoBehaviourPun
{
    [SerializeField]
    public TextMeshProUGUI PlayerName;
    [SerializeField]
    private Player[] players;

    //�÷��̾� �ʵ�
    [Header("�÷��̾� �ʵ�")]
    [SerializeField]
    private PlayerType Type;

    [SerializeField]
    public float MaxHp;
    [SerializeField]
    private float hp;
    public float Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }

    [SerializeField]
    public float MaxStamina;
    [SerializeField]
    private float stamina;
    public float Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            stamina = value;
        }
    }

    //�÷��̾� ���� ������Ʈ
    [Header("�÷��̾� ���� �ʵ�")]
    [SerializeField]
    private GameObject Chaser = null;
    [SerializeField]
    private GameObject Runner = null;

    private void Awake()
    {
        this.gameObject.name = $"Player{photonView.OwnerActorNr}";

        GameManager.Instance.LocalPlayerList.Add(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            PlayerName.text = "Me";

            //���� �÷��̾� ���� ĳ���͸� �÷��̾� ��ũ��Ʈ ����
            this.gameObject.AddComponent<Player>();
        }

        else
        {
            PlayerName.text = $"Player{photonView.OwnerActorNr}";
        }

        BasicSetting();
    }

    //������Ʈ�� �����Ǹ鼭 ����Ǳ� ������ ����ȭ �Լ� ��� X
    public void BasicSetting()
    {
        Hp = 100;
        MaxHp = Hp;

        Stamina = 50;
        MaxStamina = Stamina;
    }

    [PunRPC]
    public void SetType(PlayerType playerType)
    {
        Type = playerType;
    }

    [PunRPC]
    public void SetTypeColor()
    {
        switch (Type)
        {
            case PlayerType.Runner:
                PlayerName.color = Color.green;
                this.gameObject.tag = "Runner";
                break;

            case PlayerType.Chaser:
                PlayerName.color = Color.red;
                this.gameObject.tag = "Chaser";
                break;

            default:
                PlayerName.color = Color.white;
                break;
        }
    }

    [PunRPC]
    public void ChangeTypeState()
    {
        this.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        if (photonView.IsMine)
        {
            switch (Type)
            {
                case PlayerType.Runner:
                    Runner.SetActive(true);
                    break;

                case PlayerType.Chaser:
                    Chaser.SetActive(true);
                    break;

                default:
                    break;
            }
        }
    }
}
