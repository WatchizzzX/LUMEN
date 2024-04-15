using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Animations
{
    public abstract class BaseAnimation
    {
        public AnimationParameterType AnimationParameterType { get; protected set; }

        public float Duration { get; protected set; }
    }
}