﻿namespace Game.Physics
{
    public static class DefaultSettings
    {
        public const float TimeStep = 1.0f / 25.0f; // (25Hz)

        public const int VelocityIterations = 8;

        public const int PositionIterations = 3;

        public const float UpdatesPerSecond = 30.0f;

        public const float FramesPerSecond = 30.0f;

        public const float MaxTravelDistance = 1.0f;
    }
}