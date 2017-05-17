// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Cake.Common.Build.TeamCity
{
    /// <summary>
    /// Represents a more complete TeamCity provider.
    /// </summary>
    public interface ITeamCityProvider2 : ITeamCityProvider
    {
        /// <summary>
        /// Writes the service message to TeamCity.
        /// </summary>
        /// <param name="messageName">Name of the message.</param>
        /// <param name="values">The name/value parameters.</param>
        void WriteServiceMessage(string messageName, Dictionary<string, string> values);
    }
}