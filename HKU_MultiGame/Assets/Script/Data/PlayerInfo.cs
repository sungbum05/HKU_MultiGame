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

    //플레이어 필드
    [Header("플레이어 필드")]
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

    //플레이어 하위 오브젝트
    [Header("플레이어 하위 필드_역할")]
    [SerializeField]
    private GameObject Chaser = null;
    [SerializeField]
    private GameObject Runner = null;

    [Header("플레이어 하위 필드_아이템")]
    public ItemInfo EquieItem;
    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    public bool IsAttack = false;
    public bool IsOpenChest = false;
    public bool IsHide = false;

    //아이템 체크
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

            //만약 플레이어 본인 캐릭터면 플레이어 스크립트 생성
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

    //오브젝트가 생성되면서 실행되기 때문에 동기화 함수 사용 X
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

        //사운드 변경
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
