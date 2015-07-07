﻿using Loki.UI;

namespace Luna.UI
{
    public interface IScreenFactory
    {
        T Create<T>() where T : Screen;

        void Release(Screen screen);
    }
}