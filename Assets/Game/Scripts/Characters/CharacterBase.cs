using System.Collections.Generic;
using UnityEngine;

namespace RPG.GAME
{
    public enum TypeOfCharacter
    {
        HUMAN,
        MONSTER,
        ANGEL,
        DEMON,
        GOD
    }
    [RequireComponent(typeof(Outline))]
    public class CharacterBase : MonoBehaviour
    {
        [SerializeField] private TypeOfCharacter _type;
        public TypeOfCharacter Type { 
            get { return _type; }
        }
        [SerializeField] private string _name;
        public string Name
        {
            get { return _name; }
        }
        [SerializeField] private int m_Level;
        public int Level
        {
            get { return m_Level; }
        }
        [SerializeField] private int m_Hp;
        public int Hp
        {
            get { return m_Hp; }
            set { m_Hp = value; }
        }
        [SerializeField] private int m_Mp;
        public int Mp
        {
            get { return m_Mp; }
        }
        [SerializeField] private int m_Attack;
        public int Attack
        {
            get { return m_Attack; }
        }
        [SerializeField] private int m_SkillMultiplyer;
        public int SkillMultiplyer
        {
            get { return m_SkillMultiplyer; }
        }
        [SerializeField] private int m_PhysicDef;
        public int PhysicDef
        {
            get { return m_PhysicDef; }
        }
        [SerializeField] private int m_MagicDef;
        public int MagicDef
        {
            get { return m_MagicDef; }
        }
        [SerializeField] private int m_Damage;
        public int Damage
        {
            get { return m_Damage; }
            set { m_Damage = value; }
        }
        [SerializeField] private byte m_Strenght;
        public byte Strenght
        {
            get { return m_Strenght; }
        }
        [SerializeField] private byte m_Intelligence;
        public byte Intelligence
        {
            get { return m_Intelligence; }
        }
        [SerializeField] private byte m_Resilience;
        public byte Resilience
        {
            get { return m_Resilience; }
        }
        [SerializeField] private byte m_Speed;
        public byte Speed
        {
            get { return m_Speed; }
        }
        [SerializeField] private float m_CriticalDmg;
        public float CriticalDmg
        {
            get { return m_CriticalDmg; }
        }
        private bool _actionReleased = false;
        public bool ActionReleased
        {
            get { return _actionReleased;}
            set { _actionReleased = value;}
        }

        [SerializeField] private List<CharacterBase> Team = new();
        [SerializeField] private Outline outline;

        public CharacterBase GetHighSpeed()
        {
            CharacterBase highestSpeedCharacter = null;
            int maxSpeed = 0;

            foreach (CharacterBase character in Team)
            {
                if (!character.ActionReleased && character.Speed > maxSpeed)
                {
                    maxSpeed = character.Speed;
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
                character.ActionReleased = false;
            }
        }

        public void OutlineChangeVisibility(bool visibility)
        {
            outline.OutlineMode = visibility? Outline.Mode.OutlineVisible : Outline.Mode.OutlineHidden;
            outline.OutlineWidth = visibility? 8 : 0;
        }
    }
    
}
