using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GAME
{
    sealed class BattleManager : IAction
    {
        public EventHandler onTurnIsToEnemy;
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
            int playerCount = characters.playerCharacter.Team.Count;
            int enemyStartIndex = grid.Length / 2;


            for (int i = 0; i < playerCount; i++)
            {
                characters.playerCharacter.Team[i].transform.position = grid[i].position;
            }
            for (int j = 0; j < characters.enemyCharacter.Team.Count; j++)
            {
                int enemyGridIndex = enemyStartIndex + j;
                characters.enemyCharacter.Team[j].transform.position = grid[enemyGridIndex].position;
            }

            currentSelectedForAction = characters.playerCharacter.GetHighSpeed().m_Speed >= characters.enemyCharacter.m_Speed ? characters.playerCharacter.GetHighSpeed() : characters.enemyCharacter.GetHighSpeed();

            cameraBattle.target.position = currentSelectedForAction.transform.position;

            if (characters.enemyCharacter.GetTeam().Contains(currentSelectedForAction))
            {
                currentTarget = characters.playerCharacter.GetHighSpeed();
            }
            else
            {
                currentTarget = characters.enemyCharacter.GetHighSpeed();
            }
            currentTarget.ActivateOutline();
        }

        public void PerformAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.PhysicalAttack:
                    Debug.Log($"O {currentSelectedForAction.name} está atacando {currentTarget}");
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
                        onTurnIsToEnemy.Invoke(this, EventArgs.Empty);
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
                    onTurnIsToEnemy.Invoke(this, EventArgs.Empty);
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