using System;

namespace domain.commands
{
    [Flags]
    public enum Connection
    {
        None,
        Top,
        Bottom
    }
}