﻿using Scripts.Network.APIs;
using Scripts.Network.Core;

namespace Scripts.Network.Services
{
    public interface IGameServerProviderService : IServiceBase
    {
        IAuthorizerApi GetAuthorizerApi();

        IGameServerProviderApi GetGameServerProviderApi();
    }
}