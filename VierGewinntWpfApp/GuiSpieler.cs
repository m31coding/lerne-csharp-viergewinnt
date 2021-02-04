using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;

namespace VierGewinntWpfApp
{
    public class GuiSpieler : ISpieler
    {
        private int gewählteSpalte;
        private bool waiting;
        private readonly object locker;
        private readonly ManualResetEventSlim resetEvent;
        private bool spielabbruch = false;

        public GuiSpieler()
        {
            gewählteSpalte = 0;
            locker = new object();
            waiting = false;
            resetEvent = new ManualResetEventSlim(false);
        }

        public void Abbruch()
        {
            lock (locker)
            {
                spielabbruch = true;
                resetEvent.Set();
            }
        }

        public void WähleSpalte(int spalte)
        {
            lock (locker)
            {
                if (!waiting)
                {
                    return;
                }
                else
                {
                    gewählteSpalte = spalte;
                    resetEvent.Set();
                }
            }
        }

        public Spielzug BerechneNächstenSpielzug(Spielstellung stellung)
        {
            List<Spielzug> möglicheZüge = stellung.MöglicheZüge().ToList();

            lock (locker)
            {
                waiting = true;
            }

            resetEvent.Wait();

            lock (locker)
            {
                waiting = false;
                resetEvent.Reset();

                if (spielabbruch)
                {
                    throw new OperationCanceledException();
                }
            }

            Spielzug? zug = möglicheZüge.FirstOrDefault(z => z.Spalte == gewählteSpalte);

            if (zug == null)
            {
                return BerechneNächstenSpielzug(stellung);
            }
            else
            {
                return zug.Value;
            }
        }
    }
}
