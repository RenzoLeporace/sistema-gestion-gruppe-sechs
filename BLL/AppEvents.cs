using System;

namespace BLL
{
    public static class AppEvents
    {
        public static event EventHandler? EconomiaActualizada;
        public static void NotifyEconomiaActualizada() => EconomiaActualizada?.Invoke(null, EventArgs.Empty);
    }
}