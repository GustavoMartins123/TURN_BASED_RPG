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
    }
}
