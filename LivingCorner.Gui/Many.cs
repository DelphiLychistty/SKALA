using System;
using System.Collections.Generic;
using System.Text;

namespace LivingCore.Gui
{
    public static class Many
    {
        public static int Coin { get; set; } = 0;
        public static void AddCoins(int amount)
        {
            Coin += amount;
        }
        public static void ResetSession()
        {
            Coin = 0;
        }
    }
}
