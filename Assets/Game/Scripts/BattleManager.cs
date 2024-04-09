using System;
using System.Collections;
using UnityEngine;

namespace RPG.GAME
{
    sealed class BattleManager : IAction
    {
        public Action onTurnIsToEnemy;
        private CharacterBase currentSelectedForAction, currentTarget;
        private readonly Player player;
        private readonly CharacterCollisionEventArgs characters;
        private readonly CameraController cameraBattle;

        public BattleManager(CharacterCollisionEventArgs characters, CameraController cameraBattle, Player player)
        {
            this.characters = characters;
            this.cameraBattle = cameraBattle;
            this.player = player;
            this.player.BattleInit();
        }

        public void PlaceCharactersOnGrid(Transform[] grid)
        {
            
            for (int i = 0; i <= 4; i++)
            {
                if (i < characters.playerCharacter.GetTeam().Count)
                {
                    characters.playerCharacter.GetTeam()[i].transform.position = grid[i].position;
                }
                if (i < characters.enemyCharacter.GetTeam().Count)
                {
                    characters.enemyCharacter.GetTeam()[i].transform.position = grid[i + 4].position;
                }
            }
            currentSelectedForAction = characters.playerCharacter.GetHighSpeed().Speed >= characters.enemyCharacter.Speed ? characters.playerCharacter.GetHighSpeed() : characters.enemyCharacter.GetHighSpeed();

            cameraBattle.SetTarget(currentSelectedForAction.transform.position);

            currentTarget = characters.enemyCharacter.GetTeam().Contains(currentSelectedForAction) ? characters.playerCharacter.GetHighSpeed() : characters.enemyCharacter.GetHighSpeed();
            currentTarget.OutlineChangeVisibility(true);
        }

        public void PerformAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.PhysicalAttack:
                    Debug.Log($"O {currentSelectedForAction.name} está atacando {currentTarget.name}");
                    //Working
                    currentSelectedForAction.Damage = Mathf.Max(Mathf.RoundToInt(currentSelectedForAction.Attack - (currentTarget.PhysicDef / 2) + UnityEngine.Random.Range(-3, 3) + (currentSelectedForAction.Strenght/5) - (currentTarget.Resilience/7)), 1);
                    currentTarget.Hp -= currentSelectedForAction.Damage;
                    PassTurn();
                    break;
                case ActionType.MagicalAttack:

                    break;
                case ActionType.Defense:

                    break;
                default:
                    PassTurn();
                    break;

            }
        }

        private void PassTurn()
        {
            VerifyCurrentTargetActions(currentSelectedForAction);
            cameraBattle.SetTarget(currentSelectedForAction.transform.position);
        }
        private void VerifyCurrentTargetActions(CharacterBase currentAttacking)
        {
            currentAttacking.ActionReleased = true;
            bool allActionsReleased = true;

            foreach (CharacterBase character in currentAttacking.GetTeam())
            {
                if (!character.ActionReleased)
                {
                    allActionsReleased = false;
                    currentSelectedForAction = character.GetHighSpeed();
                    if (characters.enemyCharacter.GetTeam().Contains(currentSelectedForAction))
                    {
                        onTurnIsToEnemy.Invoke();
                    }
                    break;
                }
            }

            if (allActionsReleased)
            {
                currentAttacking.ResetActions();
                currentTarget.OutlineChangeVisibility(false);
                currentTarget = currentAttacking.GetHighSpeed();
                currentTarget.OutlineChangeVisibility(true);
                CharacterBase nextTeam;
                if (characters.playerCharacter.GetTeam().Contains(currentAttacking))
                {
                    nextTeam = characters.enemyCharacter.GetHighSpeed();
                    onTurnIsToEnemy.Invoke();
                }
                else
                {
                    nextTeam = characters.playerCharacter.GetHighSpeed();
                    player.PlayerTurnOrEnemy(true);
                }
                currentSelectedForAction = nextTeam;
            }
        }

        //Test
        public IEnumerator CurrentSelectedForActionIsEnemy()
        {
            yield return new WaitForSecondsRealtime(3f);
            PerformAction(ActionType.PhysicalAttack);
        }
    }
}