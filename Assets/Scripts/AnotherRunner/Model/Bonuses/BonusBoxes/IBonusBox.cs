using AnotherRunner.Model.Bodies;

namespace AnotherRunner.Model.Bonuses.BonusBoxes
{
    public interface IBonusBox : IBody
    {
        IBonus Open();
    }
}