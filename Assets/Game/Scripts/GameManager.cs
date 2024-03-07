using Cinemachine;
using RPG.GAME;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Player player;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private bool playerTurn;
    public CharacterBase currentTarget, currentSelectedForAction;
    private BattleManager battleManager;
    [SerializeField] private Material twirlMaterial;
    [SerializeField] private GameObject screenDistortion;
    /// <summary>
    /// ///////////////////////////////////////////////////////
    /// </summary>
    [SerializeField] private CharacterBase enemy;/// <summary>
    /// //////////////////////////////////////////////////////
    /// </summary>
    [SerializeField] private Camera cameraDistortion;
    [SerializeField] private CameraController cameraBattle; //Work to set the right target and stay in a good position, it will probably be a targetcamera like the one on the regular player
    float time = 0;
    [SerializeField] private Transform[] grid;

    //Test
    public float radius = 5f;

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
        player.onSelectTargetInBattle += Player_OnSelectTargetInBattle;
        SpawnObjectsInCircle();
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

    void SpawnObjectsInCircle()
    {
        List<CharacterBase> enemyList = new List<CharacterBase>();
        for (int i = 0; i < 8; i++)
        {
            float angle = i * Mathf.PI * 2f / 10;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 spawnPosition = transform.position + new Vector3(x, 0f, z);

            GameObject enemyObject = Instantiate(this.enemy.gameObject, spawnPosition, Quaternion.identity);
            CharacterBase enemy = enemyObject.GetComponent<CharacterBase>();
            enemyList.Add(enemy);

            if (i % 4 == 3)
            {
                for (int j = 0; j < 4; j++)
                {
                    int index = (i - j) % enemyList.Count;
                    enemyList[i].Team.Add(enemyList[index]);
                    
                }
                enemyList[i].Team.Reverse();
                for (int k = 0; k < 3; k++)
                {
                    int previousIndex = (i - k - 1) % enemyList.Count;
                    enemyList[previousIndex].Team.Clear();
                    enemyList[previousIndex].Team.AddRange(enemyList[i].Team);
                }
            }

        }
    }

    public void PassTurn()
    {
        battleManager.PerformAction(currentSelectedForAction, currentTarget, ActionType.None);
    }

    public void Attack()
    {
        battleManager.PerformAction(currentSelectedForAction, currentTarget, ActionType.PhysicalAttack);
    }

    void Player_OnEnterInBattle(object sender, CharacterCollisionEventArgs e)
    {
        playerTurn = e.playerCharacter.GetHighSpeed().m_Speed >= e.enemyCharacter.GetHighSpeed().m_Speed ? true : false;
        battleManager = new BattleManager(e, cameraBattle);
        player.BattleInit();
        battleManager.PlaceCharactersOnGrid(playerTurn, grid);
    }

    //Replace this with a UI action, because now it will be useless, as the camera will move
    void Player_OnSelectTargetInBattle(object sender, RaycastHit e)
    {
        currentTarget = e.collider.GetComponent<CharacterBase>();
        cameraBattle.target.transform.position = currentTarget.transform.position;

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
