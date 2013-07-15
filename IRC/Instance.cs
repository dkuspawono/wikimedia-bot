﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace wmib
{
    /// <summary>
    /// Represent one instance of this bot
    /// </summary>
    public class Instance
    {
        /// <summary>
        /// Nickname of this instance
        /// </summary>
        public string Nick;
        /// <summary>
        /// Port for bouncer
        /// </summary>
        public int Port = 0;
        /// <summary>
        /// Host for bouncer
        /// </summary>
        public string Hostname = "127.0.0.1";
        /// <summary>
        /// If this instance is connected
        /// </summary>
        public bool IsConnected = false;
        public Thread thread = null;
        public bool IsWorking
        {
            get
            {
                return irc.IsWorking;
            }
        }
        /// <summary>
        /// Number of channels that are being used by this instance
        /// </summary>
        public int ChannelCount
        {
            get
            {
                int Channels = 0;
                lock (config.channels)
                {
                    foreach (config.channel channel in config.channels)
                    {
                        if (channel.instance == this)
                        {
                            Channels++;
                        }
                    }
                }
                return Channels;
            }
        }
        public IRC irc = null;

        /// <summary>
        /// Creates a new bot instance but not connect it to IRC
        /// </summary>
        /// <param name="name"></param>
        public Instance(string name, int port = 0)
        {
            Nick = name;
            Port = port;
            irc = new IRC(config.network, Nick, config.name, config.name, this);
            irc.Bouncer = Hostname;
            irc.BouncerPort = Port;
        }

        public void JoinAll()
        {
            
        }

        public void Init()
        {
            thread = new Thread(Connect);
            thread.Name = Nick;
            thread.Start();
        }

        public void Shut()
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }

        /// <summary>
        /// Connect the instance
        /// </summary>
        private void Connect()
        {
            irc.Connect();
        }
    }
}
