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
                if (i < characters.playerCharacter.Team.Count)
                {
                    characters.playerCharacter.Team[i].transform.position = grid[i].position;
                }
                if (i < characters.enemyCharacter.Team.Count)
                {
                    characters.enemyCharacter.Team[i].transform.position = grid[i + 4].position;
                }
            }
            currentSelectedForAction = characters.playerCharacter.GetHighSpeed().m_Speed >= characters.enemyCharacter.m_Speed ? characters.playerCharacter.GetHighSpeed() : characters.enemyCharacter.GetHighSpeed();

            cameraBattle.target.position = currentSelectedForAction.transform.position;

            currentTarget = characters.enemyCharacter.GetTeam().Contains(currentSelectedForAction) ? characters.playerCharacter.GetHighSpeed() : characters.enemyCharacter.GetHighSpeed();
            currentTarget.ActivateOutline();
        }

        public void PerformAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.PhysicalAttack:
                    Debug.Log($"O {currentSelectedForAction.name} está atacando {currentTarget.name}");
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
            cameraBattle.target.transform.position = currentSelectedForAction.transform.position;
        }
        private void VerifyCurrentTargetActions(CharacterBase currentAttacking)
        {
            currentAttacking.action_Released = true;
            bool allActionsReleased = true;

            foreach (CharacterBase character in currentAttacking.GetTeam())
            {
                if (!character.action_Released)
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
                currentTarget.DeactivateOutline();
                currentTarget = currentAttacking.GetHighSpeed();
                currentTarget.ActivateOutline();
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