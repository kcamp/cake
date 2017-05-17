// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Cake.Common.Build.TeamCity
{
    /// <summary>
    /// A set of extensions providing additional communication with the TeamCity service message system
    /// </summary>
    public static class TeamCityProviderExtensions
    {
        /// <summary>
        /// Write a testSuiteStarted message to the TeamCity build log.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="name">The test suite name.</param>
        public static void TestSuiteStarted(this ITeamCityProvider provider, string name)
        {
            WriteServiceMessage("testSuiteStarted", new Dictionary<string, string>
            {
                { "name", name }
            });
        }

        private static void WriteServiceMessage(string message, Dictionary<string, string> values)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Write a testSuiteFinished message to the TeamCity build log.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="name">The test suite name.</param>
        public static void TestSuiteFinished(this ITeamCityProvider provider, string name)
        {
            WriteServiceMessage("testSuiteFinished", new Dictionary<string, string>
            {
                { "name", name }
            });
        }

        /// <summary>
        /// Write a testStarted message to the TeamCity build log.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="name">The name of the test.</param>
        /// <param name="captureStandardOutput">if set to <c>true</c> TeamCity should capture standard output as part of the test.</param>
        public static void TestStarted(this ITeamCityProvider provider, string name, bool captureStandardOutput = false)
        {
            WriteServiceMessage("testStarted", new Dictionary<string, string>
            {
                { "name", name },
                { "captureStandardOutput", captureStandardOutput.ToString().ToLower() }
            });
        }

        /// <summary>
        /// Write a testFinished message to the TeamCity build log.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="name">The name of the test.</param>
        /// <param name="durationMilliseconds">The duration of the test in milliseconds (optional).</param>
        public static void TestFinished(this ITeamCityProvider provider, string name, int? durationMilliseconds = null)
        {
            var values = new Dictionary<string, string> { { "name", name } };

            if (durationMilliseconds.HasValue)
            {
                values.Add("duration", durationMilliseconds.Value.ToString());
            }

            WriteServiceMessage("testFinished", values);
        }

        /// <summary>
        /// Write a testStdOut message to the TeamCity build log.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="name">The name.</param>
        /// <param name="output">The output.</param>
        public static void TestOutput(this ITeamCityProvider provider, string name, string output)
        {
            WriteServiceMessage("testStdOut", new Dictionary<string, string>
            {
                { "name", name },
                { "out", output }
            });
        }

        /// <summary>
        /// Write a testStdErr message to the TeamCity build log.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="name">The name.</param>
        /// <param name="output">The output.</param>
        public static void TestError(this ITeamCityProvider provider, string name, string output)
        {
            WriteServiceMessage("testStdErr", new Dictionary<string, string>
            {
                { "name", name },
                { "out", output }
            });
        }

        /// <summary>
        /// Write a testIgnored message to the TeamCity build log.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="name">The name of the test.</param>
        /// <param name="reason">The reason.</param>
        public static void TestIgnored(this ITeamCityProvider provider, string name, string reason)
        {
            WriteServiceMessage("testIgnored", new Dictionary<string, string>
            {
                { "name", name },
                { "message", reason }
            });
        }

        /// <summary>
        /// Write a testFailed message to the TeamCity build log.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="name">The name of the test.</param>
        /// <param name="message">The message.</param>
        /// <param name="details">The details of the failure.</param>
        public static void TestFailed(this ITeamCityProvider provider, string name, string message, string details = null)
        {
            WriteServiceMessage("testFailed", new Dictionary<string, string>
            {
                { "name", name },
                { "message", message },
                { "details", details ?? string.Empty }
            });
        }

        /// <summary>
        /// Write a testFailed message to the TeamCity build log.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="name">The name of the test.</param>
        /// <param name="message">The message.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <param name="details">The details of the failure.</param>
        public static void TestFailed(this ITeamCityProvider provider, string name, string message, object expected, object actual, string details = null)
        {
            WriteServiceMessage("testFailed", new Dictionary<string, string>
            {
                { "type", "comparisonFailure" },
                { "name", name },
                { "message", message },
                { "expected", expected?.ToString() ?? "<null>" },
                { "actual", actual?.ToString() ?? "<null>" },
                { "details", details ?? string.Empty }
            });
        }
    }
}
