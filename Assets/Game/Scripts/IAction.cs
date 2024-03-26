namespace RPG.GAME
{
    public enum ActionType
    {
        PhysicalAttack,
        MagicalAttack,
        Defense,
        None
    }
    public interface IAction
    {

        void PerformAction(ActionType action);
        /*public void PerformAction(CharacterBase currentSelectedForAction ,CharacterBase target, ActionType action)
        {
            switch (action)
            {
                case ActionType.PhysicalAttack:
                    currentSelectedForAction.m_Dmg = Mathf.RoundToInt(currentSelectedForAction.m_Attack - (target.m_Pdf / 2) + Random.Range(-3, 3) + currentSelectedForAction.m_Strenght / 5 - target.m_Resilience / 7);
                    int finalDmg = Mathf.Max(currentSelectedForAction.m_Dmg, 1);
                    target.m_Hp -= finalDmg;
                    break;
                case ActionType.MagicalAttack:

                    break;
                case ActionType.Defense:

                    break;
                case ActionType.None:

                    break;
            }
        }*/

    }
}
