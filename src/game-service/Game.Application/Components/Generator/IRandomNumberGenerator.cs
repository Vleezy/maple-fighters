﻿namespace Game.Application.Components
{
    public interface IRandomNumberGenerator
    {
        int GenerateRandomNumber();

        int GenerateRandomNumber(int minimum, int maximum);
    }
}