﻿using VContainer;
using VContainer.Unity;

namespace ReversStudio.SUDO.Runtime.Loading
{
    public sealed class LoadingScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LoadingFlow>();
        }
    }
}