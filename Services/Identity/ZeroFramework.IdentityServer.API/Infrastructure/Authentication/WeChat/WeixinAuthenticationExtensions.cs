﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Authentication;
using System.Diagnostics.CodeAnalysis;
using ZeroFramework.IdentityServer.API.Infrastructure.Authentication.Weixin;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods to add Weixin authentication capabilities to an HTTP application pipeline.
/// </summary>
public static class WeixinAuthenticationExtensions
{
    /// <summary>
    /// Adds <see cref="WeixinAuthenticationHandler"/> to the specified
    /// <see cref="AuthenticationBuilder"/>, which enables Weixin authentication capabilities.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static AuthenticationBuilder AddWeixin([NotNull] this AuthenticationBuilder builder)
    {
        return builder.AddWeixin(WeixinAuthenticationDefaults.AuthenticationScheme, options => { });
    }

    /// <summary>
    /// Adds <see cref="WeixinAuthenticationHandler"/> to the specified
    /// <see cref="AuthenticationBuilder"/>, which enables Weixin authentication capabilities.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="configuration">The delegate used to configure the OpenID 2.0 options.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static AuthenticationBuilder AddWeixin(
        [NotNull] this AuthenticationBuilder builder,
        [NotNull] Action<WeixinAuthenticationOptions> configuration)
    {
        return builder.AddWeixin(WeixinAuthenticationDefaults.AuthenticationScheme, configuration);
    }

    /// <summary>
    /// Adds <see cref="WeixinAuthenticationHandler"/> to the specified
    /// <see cref="AuthenticationBuilder"/>, which enables Weixin authentication capabilities.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="scheme">The authentication scheme associated with this instance.</param>
    /// <param name="configuration">The delegate used to configure the Weixin options.</param>
    /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
    public static AuthenticationBuilder AddWeixin(
        [NotNull] this AuthenticationBuilder builder,
        [NotNull] string scheme,
        [NotNull] Action<WeixinAuthenticationOptions> configuration)
    {
        return builder.AddWeixin(scheme, WeixinAuthenticationDefaults.DisplayName, configuration);
    }

    /// <summary>
    /// Adds <see cref="WeixinAuthenticationHandler"/> to the specified
    /// <see cref="AuthenticationBuilder"/>, which enables Weixin authentication capabilities.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="scheme">The authentication scheme associated with this instance.</param>
    /// <param name="caption">The optional display name associated with this instance.</param>
    /// <param name="configuration">The delegate used to configure the Weixin options.</param>
    /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
    public static AuthenticationBuilder AddWeixin(
        [NotNull] this AuthenticationBuilder builder,
        [NotNull] string scheme,
        [MaybeNull] string caption,
        [NotNull] Action<WeixinAuthenticationOptions> configuration)
    {
        return builder.AddOAuth<WeixinAuthenticationOptions, WeixinAuthenticationHandler>(scheme, caption, configuration);
    }
}
