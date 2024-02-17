using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GAME
{
    public enum ActionType
    {
        PhysicalAttack,
        MagicalAttack,
        Defense,
        None
    }
    [CreateAssetMenu(fileName ="Character", menuName = "New Character")]
    public class CharacterBase: ScriptableObject
    {
        public string _name;
        public enum TypeOfCharacter
        {
            HUMAN,
            MONSTER,
            ANGEL,
            DEMON,
            GOD
        }

        public TypeOfCharacter m_Type;
        public int m_Lvl;
        public int m_Hp;
        public int m_Mp;
        public int m_Dmg;
        public int m_CriticalDmg;
        public int m_Attack;
        public int m_SkillMultiplyer;
        public int m_Pdf;
        public int m_Mdf;
        public byte m_Strenght;
        public byte m_Intelligence;
        public byte m_Resilience;
        public byte m_Speed;
        public bool action_Released = false;
        public List<CharacterBase> Team = new();
        public GameObject VisualObject;

        public void PerformAction(CharacterBase target, ActionType action)
        {
            switch (action)
            {
                case ActionType.PhysicalAttack:
                    m_Dmg = Mathf.RoundToInt(m_Attack - (target.m_Pdf / 2) + Random.Range(-3, 3) + m_Strenght/5 - target.m_Resilience/7);
                    int finalDmg = Mathf.Max(m_Dmg, 1);
                    target.m_Hp -= finalDmg;
                    break;
                case ActionType.MagicalAttack:
                    
                    break;
                case ActionType.Defense:
                    
                    break;
                case ActionType.None: 
                    
                    break;
            }
        }

        public CharacterBase GetHighSpeed()
        {
            CharacterBase highestSpeedCharacter = null;
            int maxSpeed = 0;

            foreach (CharacterBase character in Team)
            {
                if (!character.action_Released)
                {
                    if (character.m_Speed > maxSpeed)
                    {
                        maxSpeed = character.m_Speed;
                        highestSpeedCharacter = character;
                    }
                }
            }

            return highestSpeedCharacter;
        }
    }
}
