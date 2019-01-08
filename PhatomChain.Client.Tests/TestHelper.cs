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
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RichardSzalay.MockHttp;

using PhantomChain.Client.API;

namespace PhantomChain.Client.Tests
{
    public static class TestHelper
    {
        const string MOCK_HOST = "https://127.0.0.1:4003/api/";
        const string FIXTURES_PATH = "../../../Fixtures/";

        static MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

        public static MockedRequest MockHttpRequest(string endpoint)
        {
            mockHttp = new MockHttpMessageHandler();

            var fixtureName = endpoint.Replace("/", "-") + ".json";
            var path = Path.Combine(FIXTURES_PATH, fixtureName);
            var fixture = File.ReadAllText(path);

            return mockHttp
                .When(string.Format("{0}{1}", MOCK_HOST, endpoint))
                .Respond("application/json", fixture);
        }

        public static IConnection MockConnection()
        {
            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri(MOCK_HOST);

            return new Connection(client);
        }

        public static void VerifyNoOutstandingExpectation()
        {
            mockHttp.VerifyNoOutstandingExpectation();
        }

        public static void AssertSuccessResponse(JObject response)
        {
            Assert.IsTrue((bool)response["success"]);
            VerifyNoOutstandingExpectation();
        }
    }
}
