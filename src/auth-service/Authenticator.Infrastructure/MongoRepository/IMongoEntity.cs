﻿using Common.Repository.Interfaces;

namespace Common.MongoDB
{
    /// <summary>
    /// Represents an entity inside the mongo database.
    /// </summary>
    public interface IMongoEntity : IEntity<string>
    {
        // Left blank intentionally
    }
}