using System;
using UnityEngine;

namespace Core.Movement.Data
{
    [Serializable]
    public class DirectionalMovementData
    {
        [field: SerializeField] public  SpriteRenderer PlayerSprite { get; private set; }
    }
}