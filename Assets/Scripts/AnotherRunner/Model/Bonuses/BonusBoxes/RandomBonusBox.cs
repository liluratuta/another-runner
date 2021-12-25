using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AnotherRunner.Model.Bonuses.BonusBoxes
{
    public class RandomBonusBox : IBonusBox
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; }

        public RandomBonusBox(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public IBonus Open()
        {
            var randomNumber = Random.Range(0, 3);

            return randomNumber switch
            {
                0 => new CoinsBonus(10),
                1 => new MultiplierJumpHeightBonus(2f),
                2 => new MultiplierRunningSpeedBonus(2f),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}