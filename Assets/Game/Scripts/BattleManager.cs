using System.Collections.Generic;
using UnityEngine;

namespace RPG.GAME
{
    sealed class BattleManager: IAction
    {
        private CharacterCollisionEventArgs characters;
        private List<CharacterBase> characters_list = new();
        private bool playerTurn;
        private CameraController cameraBattle;

        public BattleManager(CharacterCollisionEventArgs characters, CameraController cameraBattle)
        {
            this.characters = characters;
            this.cameraBattle = cameraBattle;
            characters_list.Add(this.characters.playerCharacter);
            characters_list.Add(this.characters.enemyCharacter);

        }

        public void PlaceCharactersOnGrid(bool playerTurn,Transform[] grid)
        {
            int playerCount = characters.playerCharacter.Team.Count;
            int enemyStartIndex = grid.Length/2;
            this.playerTurn = playerTurn;

            
            for (int i = 0; i < playerCount; i++)
            {
                characters.playerCharacter.Team[i].transform.position = grid[i].position;
            }
            for (int j = 0; j < characters.enemyCharacter.Team.Count; j++)
            {
                int enemyGridIndex = enemyStartIndex + j;
                characters.enemyCharacter.Team[j].transform.position = grid[enemyGridIndex].position;
            }

            GameManager.Instance.currentSelectedForAction = this.playerTurn ? characters.playerCharacter.GetHighSpeed() : characters.enemyCharacter.GetHighSpeed();

            CharacterBase currentSelectedForAction = GameManager.Instance.currentSelectedForAction;

            cameraBattle.target.position = currentSelectedForAction.transform.position;

            if (this.characters.enemyCharacter.GetTeam().Contains(currentSelectedForAction))
            {
                GameManager.Instance.currentTarget = characters.playerCharacter.GetHighSpeed();
            }
            else
            {
                GameManager.Instance.currentTarget = characters.enemyCharacter.GetHighSpeed();
            }

            
        }

        public void PerformAction(CharacterBase currentSelectedForAction, CharacterBase target, ActionType action)
        {
            currentSelectedForAction.action_Released = true;
            switch(action)
            {
                case ActionType.PhysicalAttack:
                    Debug.Log($"O {currentSelectedForAction.name} está atacando {target}");
                    cameraBattle.target.transform.position = GameManager.Instance.currentSelectedForAction.transform.position;
                    break;
                case ActionType.MagicalAttack:

                    break;
                case ActionType.Defense:

                    break;
                default:
                    PassTurn(currentSelectedForAction);
                    break;

            }
            VerifyCurrentTargetActions(currentSelectedForAction);
        }

        private void PassTurn(CharacterBase selectedForAction)
        {
            cameraBattle.target.transform.position = selectedForAction.transform.position;
        }
        //Doesnt work now, working to fix
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
                PassTurn(GameManager.Instance.currentSelectedForAction);
            }
            //Notifies that the turn has passed, or the character's turn has passed
            return;
        }
    }
}
