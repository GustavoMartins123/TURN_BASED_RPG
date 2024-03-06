using Cinemachine;
using RPG.GAME;
using System;
using System.Collections;
using UnityEngine;

sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EventHandler onTurnHasChanged;
    [SerializeField] private Player player;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private bool playerTurn;
    public CharacterBase currentTarget, currentSelectedForAction;
    private BattleManager battleManager;
    [SerializeField] private Material twirlMaterial;
    [SerializeField] private GameObject screenDistortion;
    [SerializeField] private Camera cameraDistortion;
    [SerializeField] private CinemachineVirtualCamera cameraBattle; 
    float time = 0;
    [SerializeField] private Transform[] grid;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        characterMovement.onEnterInBattle += Player_OnEnterInBattle;
        //twirlMaterial.SetFloat("_RotateSpeed", 0);
        //twirlMaterial.SetFloat("_TwirlStrenght", 0);
    }


    private void Update()
    {
        /*if(battleManager != null)
        {
            DistortionBattleScreen();
        }*///******Study shaders****** 
        // Thinking about how to improve this, because it's very heavy and ugly
    }
    public void PassTurn()
    {
        battleManager.TakeAction(currentSelectedForAction, ActionType.None);
        onTurnHasChanged?.Invoke(currentSelectedForAction, EventArgs.Empty);//for camera and UI
    }

    void Player_OnEnterInBattle(object sender, CharacterCollisionEventArgs e)
    {
        playerTurn = e.playerCharacter.GetHighSpeed().m_Speed >= e.enemyCharacter.GetHighSpeed().m_Speed ? true : false;
        Debug.Log(playerTurn);
        battleManager = new BattleManager(e, playerTurn, grid, cameraBattle);
        player.BattleInit();
        Debug.Log($"O target atual é {currentTarget}");
        Debug.Log($"A vez é do {currentSelectedForAction}");
        battleManager.PlaceCharactersOnGrid();
        cameraBattle.Follow = currentSelectedForAction.transform;
    }

    public bool GetCurrentTurn()
    {
        return playerTurn;
    }

    void DistortionBattleScreen()
    {
        time += Time.deltaTime;

        cameraDistortion.gameObject.SetActive(true);
        screenDistortion.SetActive(true);
        if (time < 1f)
        {
            float rotateSpeed = Mathf.Lerp(0f, 1f, time / 1f);
            float twirlStrength = Mathf.Lerp(0f, 1f, time / 1f);
            twirlMaterial.SetFloat("_RotateSpeed", rotateSpeed);
            twirlMaterial.SetFloat("_TwirlStrenght", twirlStrength);
        }
        else if (time >= 1f && time < 4f)
        {
            twirlMaterial.SetFloat("_RotateSpeed", 1f);
            twirlMaterial.SetFloat("_TwirlStrenght", 1f);
        }
        else if(time > 4f && time < 6f)
        {
            float fieldOfView = Mathf.Lerp(cameraDistortion.fieldOfView, 60 + 10f, (time - 4f) / 2f);
            cameraDistortion.fieldOfView = fieldOfView;
            float rotateSpeed = Mathf.Lerp(1f, 10f, (time - 4f) / 2f);
            float twirlStrength = Mathf.Lerp(1f, 100f, (time - 4f) / 2f);
            twirlMaterial.SetFloat("_RotateSpeed", rotateSpeed);
            twirlMaterial.SetFloat("_TwirlStrenght", twirlStrength);
        }
        screenDistortion.SetActive(false);
        cameraDistortion.gameObject.SetActive(false);
    }
}
