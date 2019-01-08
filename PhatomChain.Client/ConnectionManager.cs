// Author:
//       Brian Faust <brian@ark.io>
//
// Copyright (c) 2018 PhantomChain <info@phantom.org>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using PhantomChain.Client.API;

namespace PhantomChain.Client
{
    public class ConnectionManager
    {
        string defaultConnection = "main";

        readonly Dictionary<string, IConnection> connections = new Dictionary<string, IConnection>();

        public IConnection Connect(IConnection connection, string name = "main")
        {
            if (connections.ContainsKey(name)) {
                throw new Exception(string.Format("Connection '{0}' already exists.", name));
            }

            connections[name] = connection as IConnection;
            return connection;
        }

        public void Disconnect(string name = null)
        {
            connections.Remove(name ?? GetDefaultConnection());
        }

        public IConnection Connection(string name = null)
        {
            return connections[name ?? GetDefaultConnection()] as IConnection;
        }

        public string GetDefaultConnection()
        {
            return defaultConnection;
        }

        public void SetDefaultConnection(string name)
        {
            defaultConnection = name;
        }

        public Dictionary<string, IConnection> GetConnections()
        {
            return connections;
        }
    }
}
