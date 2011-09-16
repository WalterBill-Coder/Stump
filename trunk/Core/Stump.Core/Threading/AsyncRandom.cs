﻿using System;
using System.Threading;
using Stump.Core.Mathematics;

namespace Stump.Core.Threading
{
    /// <summary>
    ///   Represent a Random class that generate a thread unique seed
    /// </summary>
    public sealed class AsyncRandom : FastRandom
    {
        private static int m_incrementer = 0;

        public AsyncRandom()
            : base (Environment.TickCount + Thread.CurrentThread.ManagedThreadId + m_incrementer)
        {
            unchecked
            {
                Interlocked.Increment(ref m_incrementer);
            }
        }

        public AsyncRandom(int seed)
            : base(seed)
        {
        }
    }
}