using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.GAME
{
    sealed class BattleManager: IAction
    {
        private CharacterCollisionEventArgs characters;
        private List<CharacterBase> characters_list = new();
        private bool playerTurn;
        [SerializeField] CinemachineVirtualCamera cameraBattle;
        
        private Transform[] grid;

        public BattleManager(CharacterCollisionEventArgs characters, bool playerTurn, Transform[] grid, CinemachineVirtualCamera virtualCamera)
        {
            this.characters = characters;
            this.grid = grid;
            characters_list.Add(characters.playerCharacter);
            characters_list.Add(characters.enemyCharacter);
            foreach(CharacterBase character in characters_list)
            {
                Debug.Log(character.name);
            }

            GameManager.Instance.currentSelectedForAction = playerTurn ? this.characters.playerCharacter.GetHighSpeed() : this.characters.enemyCharacter.GetHighSpeed();
            CharacterBase selectedForAction = GameManager.Instance.currentSelectedForAction;

            CharacterBase selectedEnemy = selectedForAction;
            if (this.characters.enemyCharacter.GetTeam().Contains(selectedEnemy))
            {
                GameManager.Instance.currentTarget = this.characters.playerCharacter.GetTeam()[0];
            }
            else
            {
                GameManager.Instance.currentTarget = this.characters.enemyCharacter.GetTeam()[0];
            }


        }

        //Switch this, and use IAction please
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
            /*for (int i = 0; i < characters_list.Count; i++)
            {
                if (characters_list[i] == currentAttacking)
                {
                    //characters_list[i].PerformAction(target, action);
                    break;
                }
            }*/
        }

        private void VerifyCurrentTargetActions(CharacterBase currentAttacking)
        {
            bool allTrue = true;
            List<CharacterBase> team = (currentAttacking == characters.enemyCharacter) ?
                characters.enemyCharacter.GetTeam() : characters.playerCharacter.GetTeam();
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
            int enemyStartIndex = grid.Length / 2;
            for (int i = 0; i < playerCount; i++)
            {
                InstantiateObject.InstantiateObjects(characters.playerCharacter.VisualObject[i], grid[i].position);
            }
            for (int j = 0; j < characters.enemyCharacter.Team.Count; j++)
            {
                int enemyGridIndex = enemyStartIndex + j;
                InstantiateObject.InstantiateObjects(characters.enemyCharacter.VisualObject[j], grid[enemyGridIndex].position);
            }
        }

        public void PerformAction(CharacterBase currentSelectedForAction, CharacterBase target, ActionType action)
        {
            throw new System.NotImplementedException();
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
