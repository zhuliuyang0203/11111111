// <copyright file="NetworkModule.HighLevel.cs" company="Selenium Committers">
// Licensed to the Software Freedom Conservancy (SFC) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The SFC licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
// </copyright>

using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public partial class NetworkModule
{
    public async Task<Intercept> InterceptRequestAsync(Func<BeforeRequestSentEventArgs, Task> handler, InterceptRequestOptions? options = null)
    {
        var intercept = await AddInterceptAsync([InterceptPhase.BeforeRequestSent], options).ConfigureAwait(false);

        await intercept.OnBeforeRequestSentAsync(handler).ConfigureAwait(false);

        return intercept;
    }

    public async Task<Intercept> InterceptResponseAsync(Func<ResponseStartedEventArgs, Task> handler, InterceptResponseOptions? options = null)
    {
        var intercept = await AddInterceptAsync([InterceptPhase.ResponseStarted], options).ConfigureAwait(false);

        await intercept.OnResponseStartedAsync(handler).ConfigureAwait(false);

        return intercept;
    }

    public async Task<Intercept> InterceptAuthAsync(Func<AuthRequiredEventArgs, Task> handler, InterceptAuthOptions? options = null)
    {
        var intercept = await AddInterceptAsync([InterceptPhase.AuthRequired], options).ConfigureAwait(false);

        await intercept.OnAuthRequiredAsync(handler).ConfigureAwait(false);

        return intercept;
    }
}

public record InterceptRequestOptions : AddInterceptOptions;

public record InterceptResponseOptions : AddInterceptOptions;

public record InterceptAuthOptions : AddInterceptOptions;
