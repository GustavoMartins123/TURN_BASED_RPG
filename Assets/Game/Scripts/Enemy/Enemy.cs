using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GAME
{
    public class Enemy : CharacterBase
    {
        private void Awake()
        {
            m_Speed = (byte)Random.Range(2, 11);
        }
    }
}