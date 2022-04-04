﻿using MongoDB.Driver;

namespace Common.MongoDB
{
    /// <summary>
    /// Represents the provider of the mongo database.
    /// </summary>
    public interface IDatabaseProvider
    {
        IMongoDatabase Provide();
    }
}