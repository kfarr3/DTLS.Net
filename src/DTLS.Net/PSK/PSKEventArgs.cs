/*
 * Copyright (c) 2016, Ken Farr <kfarr@infinetix.com>,
 * Infinetix Corp.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY.
 * 
 * This file is part of the CoAP.NET, a CoAP framework in C#.
 * Please see README for more information.
 */

using System;

namespace DTLS
{
    /// <summary>
    /// Represents an event of a message.
    /// </summary>
    /// <typeparam name="T">the type of the message</typeparam>
    public class PSKEventArgs : EventArgs
    {
        public readonly byte[] Identity;
        public byte[] Key;

        /// <summary>
        /// 
        /// </summary>
        public PSKEventArgs(byte[] Identity)
        {
            this.Identity = Identity;
        }
    }


}
