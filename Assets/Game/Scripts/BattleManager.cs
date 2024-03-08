using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace RPG.GAME
{
    sealed class BattleManager: IAction
    {
        private CharacterBase currentSelectedForAction, currentTarget;
        private readonly CharacterCollisionEventArgs characters;
        private readonly List<CharacterBase> characters_list = new();
        private readonly CameraController cameraBattle;

        public BattleManager(CharacterCollisionEventArgs characters, CameraController cameraBattle, Player player)
        {
            this.characters = characters;
            this.cameraBattle = cameraBattle;
            characters_list.Add(this.characters.playerCharacter);
            characters_list.Add(this.characters.enemyCharacter);
            player.onSelectTargetInBattle += Player_OnSelectTargetInBattle;
        }

        public void PlaceCharactersOnGrid(Transform[] grid)
        {
            int playerCount = characters.playerCharacter.Team.Count;
            int enemyStartIndex = grid.Length/2;

            
            for (int i = 0; i < playerCount; i++)
            {
                characters.playerCharacter.Team[i].transform.position = grid[i].position;
            }
            for (int j = 0; j < characters.enemyCharacter.Team.Count; j++)
            {
                int enemyGridIndex = enemyStartIndex + j;
                characters.enemyCharacter.Team[j].transform.position = grid[enemyGridIndex].position;
            }

            currentSelectedForAction =  characters.playerCharacter.GetHighSpeed().m_Speed >= characters.enemyCharacter.m_Speed? characters.playerCharacter.GetHighSpeed() : characters.enemyCharacter.GetHighSpeed();

            cameraBattle.target.position = currentSelectedForAction.transform.position;

            if (this.characters.enemyCharacter.GetTeam().Contains(currentSelectedForAction))
            {
                currentTarget = characters.playerCharacter.GetHighSpeed();
            }
            else
            {
                currentTarget = characters.enemyCharacter.GetHighSpeed();
            }

            
        }

        public void PerformAction(ActionType action)
        {
            switch(action)
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
                    currentSelectedForAction = character;
                    break;
                }
            }

            if (allActionsReleased)
            {
                currentAttacking.ResetActions();
                currentTarget = currentAttacking.GetHighSpeed();

                CharacterBase nextTeam = (currentAttacking == characters.enemyCharacter) ?
            characters.playerCharacter : characters.enemyCharacter;
                currentSelectedForAction = nextTeam.GetTeam()[0];
            }
        }


        void Player_OnSelectTargetInBattle(object sender, RaycastHit e)
        {
            currentTarget = e.collider.GetComponent<CharacterBase>();
            cameraBattle.target.transform.position = currentTarget.transform.position;

        }
    }
}
