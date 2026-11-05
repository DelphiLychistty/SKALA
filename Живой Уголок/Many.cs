using System;
using System.Drawing;
using System.Windows.Forms;

namespace Живой_Уголок
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

        public static Image? LoadImage(string filename)
        {
            try
            {
                var full = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", filename);
                if (System.IO.File.Exists(full))
                {
                    return Image.FromFile(full);
                }
                var alt = System.IO.Path.Combine(Application.StartupPath, "..", "..", "..", "Resources", filename);
                if (System.IO.File.Exists(alt))
                {
                    return Image.FromFile(alt);
                }
            }
            catch { }
            return null;
        }
    }
}
