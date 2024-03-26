using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace RPG.GAME
{
    [RequireComponent(typeof(Outline))]
    public class CharacterBase : MonoBehaviour
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

        public TypeOfCharacter m_Type { 
            get { return m_Type; }
            set { m_Type = value; }
        }
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
        public Outline outline;

        public virtual void Start()
        {
            outline = GetComponent<Outline>();
        }

        public CharacterBase GetHighSpeed()
        {
            CharacterBase highestSpeedCharacter = null;
            int maxSpeed = 0;

            foreach (CharacterBase character in Team)
            {
                if (!character.action_Released && character.m_Speed > maxSpeed)
                {
                    maxSpeed = character.m_Speed;
                    highestSpeedCharacter = character;
                }
            }

            return highestSpeedCharacter;
        }

        public List<CharacterBase> GetTeam()
        {
            return Team;
        }

        public void ResetActions()
        {
            foreach (CharacterBase character in Team)
            {
                character.action_Released = false;
            }
        }

        public void ActivateOutline()
        {
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineWidth = 8;
        }
        public void DeactivateOutline()
        {
            outline.OutlineMode = Outline.Mode.OutlineHidden;
            outline.OutlineWidth = 0;
        }
    }
    
}
