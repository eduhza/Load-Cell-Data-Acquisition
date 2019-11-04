﻿using System;
using System.Threading;

namespace BioMensurae
{
    class BackgroundBeep
    {
        static Thread _beepThread;
        static AutoResetEvent _signalBeep;

        static BackgroundBeep()
        {
            _signalBeep = new AutoResetEvent(false);
            _beepThread = new Thread(() =>
            {
                for (; ; )
                {
                    _signalBeep.WaitOne();
                    Console.Beep(1000,500);
                }
            }, 1);
            _beepThread.IsBackground = true;
            _beepThread.Start();
        }

        public static void Beep()
        {
            _signalBeep.Set();
        }
    }
}
