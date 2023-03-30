﻿namespace Player
{
    public interface IEntityInputSource
    {
        public float HorizontalDirection { get;}
        public bool Jump { get; }
        public bool Attack { get; }

        public void ResetOneTimeActions();
    }
}