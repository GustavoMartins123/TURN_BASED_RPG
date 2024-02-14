using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GAME
{
    public class Player : MonoBehaviour
    {
        public EventHandler<CharacterCollisionEventArgs> onEnterInBattle;
        [SerializeField] List<CharacterBase> partners = new();
        [SerializeField] private CharacterBase character;
        void Start()
        {
            character.m_Hp = 30;
            character.Team = partners;
        }

        void Update()
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal") * 5* Time.deltaTime,0,0) , Space.Self);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 8)
            {
                CharacterBase enemyCharacter = collision.gameObject.GetComponent<Enemy>().character;
                if (enemyCharacter != null)
                {
                    onEnterInBattle?.Invoke(this, new CharacterCollisionEventArgs(character, enemyCharacter));
                }
            }
        }
    }

    public class CharacterCollisionEventArgs : EventArgs
    {
        public CharacterBase playerCharacter;
        public CharacterBase enemyCharacter;


        public CharacterCollisionEventArgs(CharacterBase player, CharacterBase enemy)
        {
            playerCharacter = player;
            enemyCharacter = enemy;
        }
    }
}
