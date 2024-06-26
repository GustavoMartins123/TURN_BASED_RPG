using RPG.GAME;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private BattleManager battleManager;
    [SerializeField] private Player player;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Material twirlMaterial;
    [SerializeField] private GameObject screenDistortion;
    //Enemy instance for tests
    [SerializeField] private CharacterBase enemy;
    [SerializeField] private float radius = 5f;

    [SerializeField] private Camera cameraDistortion;
    [SerializeField] private CameraController cameraBattle;
    [SerializeField] private float time = 0;
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

    private void Start()
    {
        characterMovement.onEnterInBattle += Player_OnEnterInBattle;
        SpawnObjectsInCircle();
        //twirlMaterial.SetFloat("_RotateSpeed", 0);
        //twirlMaterial.SetFloat("_TwirlStrenght", 0);
    }

    private void SpawnObjectsInCircle()
    {
        List<CharacterBase> enemyList = new();
        for (int i = 0; i < 4; i++)
        {
            float angle = i * Mathf.PI * 2f / 10;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 spawnPosition = transform.position + new Vector3(x, 0f, z);

            GameObject enemyObject = Instantiate(this.enemy.gameObject, spawnPosition, Quaternion.identity);
            CharacterBase enemy = enemyObject.GetComponent<CharacterBase>();
            enemyList.Add(enemy);
            enemy.name = $"Inimigo: {i}";

            if (i % 4 == 3)
            {
                for (int j = 0; j < 4; j++)
                {
                    int index = (i - j) % enemyList.Count;
                    enemyList[i].GetTeam().Add(enemyList[index]);
                    
                }
                enemyList[i].GetTeam().Reverse();
                for (int k = 0; k < 3; k++)
                {
                    int previousIndex = (i - k - 1) % enemyList.Count;
                    enemyList[previousIndex].GetTeam().Clear();
                    enemyList[previousIndex].GetTeam().AddRange(enemyList[i].GetTeam());
                }
            }

        }
    }

    public void PassTurn()
    {
        battleManager.PerformAction(ActionType.None);
    }

    public void Attack()
    {
        battleManager.PerformAction(ActionType.PhysicalAttack);
    }

    private void Player_OnEnterInBattle(CharacterCollisionEventArgs e)
    {
        battleManager = new BattleManager(e, cameraBattle, player);
        battleManager.PlaceCharactersOnGrid(grid);
        battleManager.onTurnIsToEnemy += StartEnemyTurn;
    }

    private void StartEnemyTurn()
    {
        StartCoroutine(battleManager.CurrentSelectedForActionIsEnemy());
        player.PlayerTurnOrEnemy(false);
    }

    #region not implemented
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
#endregion
}
