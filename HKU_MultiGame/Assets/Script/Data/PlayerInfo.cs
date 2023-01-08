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
    [SerializeField]
    private PlayerMove PlayerMove;

    //�÷��̾� �ʵ�
    [Header("�÷��̾� �ʵ�")]
    public PlayerType Type;
    [SerializeField]
    private List<PlayerData> RunnerDataList;
    [SerializeField]
    private List<PlayerData> ChaserDataList;
    [SerializeField]
    private PlayerData CurPlayerData;
    [SerializeField]
    private Animator PlayerAnimator;

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

            if(hp <= 0)
            {
                if (Player.Instance.PlayerInfo.Type == PlayerType.Runner)
                    UiManager.Instance.OnSadEnding();

                else if (Player.Instance.PlayerInfo.Type == PlayerType.Chaser)
                    UiManager.Instance.OnHappyEnding();
            }

            else if (hp > 30)
            {
                PostProcessManager.Instance.HpMax();
            }

            else
            {
                PostProcessManager.Instance.NoHp();
            }
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
    [Header("�÷��̾� ���� �ʵ�_����")]
    [SerializeField]
    private GameObject Chaser = null;
    [SerializeField]
    private GameObject Runner = null;

    [Header("�÷��̾� ���� �ʵ�_������")]
    public ItemInfo EquieItem;
    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    public bool IsAttack = false;
    public bool IsOpenChest = false;
    public bool IsHide = false;

    //������ üũ
    public bool IsShose = false;
    public bool IsBible = false;
    public bool IsEnergeDrink = false;
    public bool IsFlashLight = false;
    public bool IsMilk = false;


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

        SpriteRenderer = GameObject.Find("ItemImage").GetComponent<SpriteRenderer>();
        PlayerMove = this.GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if(photonView.IsMine == true && EquieItem != null && Input.GetKeyDown(KeyCode.Q) && Type == PlayerType.Runner)
        {
            GameObject ItemPrefab = PhotonNetwork.Instantiate(EquieItem.ItemPrefab.name, new Vector3(200, 200, 200), Quaternion.identity);
            ItemPrefab.GetComponent<Item>().Use();
            EquieItem = null;

            SpriteRenderer.sprite = null;
        }
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
    public void SetType(PlayerType playerType, int Idx)
    {
        Debug.Log($"IDX:{Idx}");

        switch (playerType)
        {
            case PlayerType.Runner:
                CurPlayerData = RunnerDataList[Idx];

                break;

            case PlayerType.Chaser:
                CurPlayerData = ChaserDataList[Idx];
                break;

            default:
                break;
        }

        Type = playerType;
        PlayerName.gameObject.SetActive(false);

        Player.Instance.PlayerMove.Speed = CurPlayerData.Speed;
        PlayerAnimator.runtimeAnimatorController = CurPlayerData.Animator;

        //���� ����
        if(this.photonView.IsMine == true)
        {
            switch (playerType)
            {
                case PlayerType.Runner:
                    SoundMgr.In.ChangeBGM("BGM_Fugitive_Normal_1");

                    break;

                case PlayerType.Chaser:
                    SoundMgr.In.ChangeBGM("BGM_Chaser");
                    break;

                default:
                    break;
            }
        }
    }

    [PunRPC]
    public void SetTypeColor()
    {
        switch (Type)
        {
            case PlayerType.Runner:
                PlayerName.color = Color.green;
                this.gameObject.tag = "Runner";

                int Ran = Random.Range(0, 3);
                this.gameObject.transform.position = GameManager.Instance.RunnerSpawnPoint[Ran].transform.position;
                break;

            case PlayerType.Chaser:
                PlayerName.color = Color.red;
                this.gameObject.tag = "Chaser";

                this.gameObject.transform.position = GameManager.Instance.ChaserSpawnPoint[CurPlayerData.Index].transform.position;
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

        else if (Player.Instance != null && photonView.IsMine != true)
        {
            if (Player.Instance.PlayerInfo.Type == PlayerType.Chaser && this.Type == PlayerType.Chaser)
            {
                this.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            }
        }
    }

    #region Hide
    public void HideCheck(bool Check)
    {

        if (Check == true)
        {
            photonView.RPC("OnHide", RpcTarget.All);
        }

        else
        {
            photonView.RPC("OffHide", RpcTarget.All);
        }

    }

    [PunRPC]
    public void OnHide()
    {
        IsHide = true;

        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;

    }

    [PunRPC]
    public void OffHide()
    {
        IsHide = false;

        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
    }
    #endregion
}
