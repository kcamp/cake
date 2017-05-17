// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Cake.Common.Build.TeamCity;
using Xunit;

namespace Cake.Common.Tests.Unit.Build.TeamCity
{
    public sealed class TeamCityServiceMessageFormatterTests
    {
        public sealed class TheConstructor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given, When
                var result = Record.Exception(() => new TeamCityMessageServiceFormatter(null));

                // Then
                Assert.IsArgumentNullException(result, "log");
            }
        }
    }
}
