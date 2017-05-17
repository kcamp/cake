// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Cake.Core.Diagnostics;

namespace Cake.Common.Build.TeamCity
{
    /// <summary>
    /// Provides formatting service for TeamCity service messages
    /// </summary>
    internal sealed class TeamCityMessageServiceFormatter
    {
        private const string MessagePrefix = "##teamcity[";
        private const string MessagePostfix = "]";
        private static readonly Dictionary<string, string> _sanitizationTokens;

        private readonly ICakeLog _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamCityMessageServiceFormatter"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public TeamCityMessageServiceFormatter(ICakeLog log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }
            _log = log;
        }

        static TeamCityMessageServiceFormatter()
        {
            _sanitizationTokens = new Dictionary<string, string>
            {
                { "|", "||" },
                { "\'", "|\'" },
                { "\n", "|n" },
                { "\r", "|r" },
                { "[", "|[" },
                { "]", "|]" }
            };
        }

        public void WriteServiceMessage(string messageName, string attributeValue)
        {
            WriteServiceMessage(messageName, new Dictionary<string, string> { { " ", attributeValue } });
        }

        public void WriteServiceMessage(string messageName, string attributeName, string attributeValue)
        {
            WriteServiceMessage(messageName, new Dictionary<string, string> { { attributeName, attributeValue } });
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "Reviewed.")]
        public void WriteServiceMessage(string messageName, Dictionary<string, string> values)
        {
            var valueString =
                string.Join(" ",
                    values
                        .Select(keypair =>
                                {
                                    if (string.IsNullOrWhiteSpace(keypair.Key))
                                    {
                                        return string.Format(CultureInfo.InvariantCulture, "'{0}'", Sanitize(keypair.Value));
                                    }
                                    return string.Format(CultureInfo.InvariantCulture, "{0}='{1}'", keypair.Key, Sanitize(keypair.Value));
                                })
                        .ToArray());
            _log.Write(Verbosity.Quiet, LogLevel.Information, "{0}{1} {2}{3}", MessagePrefix, messageName, valueString, MessagePostfix);
        }

        private static string Sanitize(string source)
        {
            foreach (var charPair in _sanitizationTokens)
            {
                source = source.Replace(charPair.Key, charPair.Value);
            }
            return source;
        }
    }
}