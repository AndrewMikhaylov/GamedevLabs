using StatsSystem;
using StatsSystem.Enum;
using UnityEngine;

namespace Movement.Controller
{
    public class Jumper
    {
        private Rigidbody2D _rigidbody;
        private IStatValueGiver _statValueGiver;

        public Jumper(Rigidbody2D rigidbody, IStatValueGiver statValueGiver)
        {
            _statValueGiver = statValueGiver;
            _rigidbody = rigidbody;
        }
        public void Jump(bool onGround)
        {
            if (!onGround)
            {
                return;
            }
            
            _rigidbody.AddForce(Vector2.up*_statValueGiver.GetStatValue(StatsType.JumpForce));

        }
    }
}