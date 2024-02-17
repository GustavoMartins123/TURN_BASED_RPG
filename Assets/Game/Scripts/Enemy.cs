using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GAME
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] List<CharacterBase> partners = new();
        public CharacterBase character;
        void Start()
        {
            character.Team = partners;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
