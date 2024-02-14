using RPG.GAME;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EventHandler onTurnHasChanged;
    [SerializeField] private Player player;
    [SerializeField] private bool playerTurn;
    public CharacterBase currentTarget, currentSelectedForAction;
    private BattleManager battleManager;

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
        player.onEnterInBattle += Player_OnEnterInBattle;
    }

    public void PassTurn()
    {
        battleManager.TakeAction(currentSelectedForAction, ActionType.None);
        onTurnHasChanged?.Invoke(currentSelectedForAction, EventArgs.Empty);//for camera and UI
    }

    void Player_OnEnterInBattle(object sender, CharacterCollisionEventArgs e)
    {
        battleManager = new BattleManager(e, e.playerCharacter.GetHighSpeed().m_Speed >= e.enemyCharacter.GetHighSpeed().m_Speed ? true : false);
        Debug.Log("Batalha iniciada entre " + e.playerCharacter._name + " e " + e.enemyCharacter._name);
    }
}
