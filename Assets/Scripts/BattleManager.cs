using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GAME
{
    public class BattleManager
    {
        private CharacterCollisionEventArgs characters;
        private List<CharacterBase> characters_list = new();
        public CharacterBase currentTarget;
        public bool selectedEnemy = false;
        //private CharacterBase currentSelectedForAction;
        private bool playerTurn;

        public BattleManager(CharacterCollisionEventArgs characters, bool playerTurn)
        {
            this.characters = characters;
            //this.currentTarget = currentTarget;
            this.playerTurn = playerTurn;
            for (int i = 0; i < characters.playerCharacter.Team.Count; i++)
            {
                characters_list.Add(characters.playerCharacter.Team[i]);
            }
            for (int i = 0; i < characters.enemyCharacter.Team.Count; i++)
            {
                characters_list.Add(characters.enemyCharacter.Team[i]);
            }
            GameManager.Instance.currentSelectedForAction = playerTurn ? characters.playerCharacter.GetHighSpeed() : characters.enemyCharacter.GetHighSpeed();
            selectedEnemy = GameManager.Instance.currentSelectedForAction == characters.playerCharacter? true : false;
            GameManager.Instance.currentTarget = !selectedEnemy? currentTarget = characters.playerCharacter.Team[0] : characters.enemyCharacter.Team[0];
        }

        public void TakeAction(CharacterBase target, ActionType action)
        {
            //In the combat system under development, currently, even with multiple characters on the team, only one of them needs to take an action for the turn to be changed. However, this behavior will be modified in the future.
            CharacterBase currentAttacking = GameManager.Instance.currentSelectedForAction;
            currentAttacking.action_Released = true;
            if(action == ActionType.None)
            {
                VerifyCurrentTargetActions(currentAttacking);
                return;
            }
            //currentTarget = target;
            for (int i = 0; i < characters_list.Count; i++)
            {
                if (characters_list[i] == currentAttacking)
                {
                    characters_list[i].PerformAction(target, action);
                    break;
                }
            }
        }

        private void VerifyCurrentTargetActions(CharacterBase currentAttacking)
        {
            bool allTrue = true;
            List<CharacterBase> team = (currentAttacking == characters.enemyCharacter) ?
                characters.enemyCharacter.Team : characters.playerCharacter.Team;
            for (int i = 0; i < team.Count; i++)
            {
                if (team[i].action_Released == false)
                {
                    GameManager.Instance.currentSelectedForAction = team[i];
                    allTrue = false;
                    break;
                }
            }
            if (allTrue)
            {
                playerTurn = !playerTurn;
                GameManager.Instance.PassTurn();
            }
            //Notifies that the turn has passed, or the character's turn has passed
            return;
        }
    }
}
