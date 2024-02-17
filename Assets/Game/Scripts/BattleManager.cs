using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.GAME
{
    public class BattleManager
    {
        private CharacterCollisionEventArgs characters;
        private List<CharacterBase> characters_list = new();
        private bool playerTurn;
        
        private Transform[] grid;

        public BattleManager(CharacterCollisionEventArgs characters, bool playerTurn, Transform[] grid)
        {
            this.characters = characters;
            this.grid = grid;
            for (int i = 0; i < characters.playerCharacter.Team.Count; i++)
            {
                characters_list.Add(characters.playerCharacter.Team[i]);
            }
            for (int i = 0; i < characters.enemyCharacter.Team.Count; i++)
            {
                characters_list.Add(characters.enemyCharacter.Team[i]);
            }
            GameManager.Instance.currentSelectedForAction = playerTurn ? this.characters.playerCharacter.GetHighSpeed() : this.characters.enemyCharacter.GetHighSpeed();

            CharacterBase selectedEnemy = GameManager.Instance.currentSelectedForAction;
            if (this.characters.enemyCharacter.Team.Contains(selectedEnemy))
            {
                GameManager.Instance.currentTarget = this.characters.playerCharacter.Team[0];
            }
            else
            {
                GameManager.Instance.currentTarget = this.characters.enemyCharacter.Team[0];
            }

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

        public void PlaceCharactersOnGrid()
        {
            int playerCount = characters.playerCharacter.Team.Count;
            int enemyStartIndex = playerCount;

            for (int i = 0; i < characters_list.Count; i++)
            {
                //Working on this, because the main characters will not instantiate, just reposition
                if (i < playerCount)
                {
                    InstantiateObject.InstantiateObjects(characters_list[i].VisualObject, grid[i].position);
                }
                else
                {
                    InstantiateObject.InstantiateObjects(characters_list[i].VisualObject, grid[i + enemyStartIndex].position);
                }
            }
        }
    }

    public class InstantiateObject: MonoBehaviour
    {
        public static void InstantiateObjects(GameObject gameObject, Vector3 pos)
        {
            Instantiate(gameObject, pos, Quaternion.identity);
        }
    }
}
