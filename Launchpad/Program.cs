using System;
using System.Threading;

namespace Launchpad
{
    class Program
    {
        static void Main(string[] args)
        {
            Launchpad launchpad = new Launchpad();

            launchpad.Reset();

            for (int i = 0; i < 30; i++)
            {
                launchpad.Reset();
                launchpad.ChangeGrid(i % 16, i / 16, LColor.GREEN_HIGH);
                Thread.Sleep(500);
            }

            Console.ReadKey();
            launchpad.Reset();

            Console.ReadKey();
        }
    }
}
