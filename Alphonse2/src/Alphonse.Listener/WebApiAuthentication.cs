using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Alphonse.Listener.Dto;
using DotNet.RestApi.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace Alphonse.Listener;

public class WebApiAuthentication
{
    private readonly ILogger<WebApiAuthentication> _logger;
    private readonly string _webApiBaseUri;
    private readonly string? _webApiUserName;
    private readonly string? _webApiUserPass;

    private AsyncRetryPolicy _webQueryPolicy = Policy
        .Handle<HttpRequestException>()
        .Or<TaskCanceledException>()
        .WaitAndRetryAsync(new double[] { 1, 1, 3, 5, 10, 20, 30 }.Select(TimeSpan.FromSeconds));

    private AsyncAccessBox<AuthenticationInfo> _currentAuthenticationInfo = new(new());

    public WebApiAuthentication(
        ILogger<WebApiAuthentication> logger,
        IOptions<AlphonseSettings> alphonseSettings
        )
    {
        this._logger = logger;
        this._webApiBaseUri = alphonseSettings.Value.WebApiBaseUri!;
        this._webApiUserName = alphonseSettings.Value.WebApiUserName;
        this._webApiUserPass = alphonseSettings.Value.WebApiUserPass;
    }

    public void ApplyAuthorization(HttpClient client)
    {
        var task = Task.Run(()
            => _webQueryPolicy.ExecuteAsync(()
                => this._currentAuthenticationInfo.UseAsync(info
                    => ApplyAuthorizationAsync(info, client)))
        );
        task.Wait();
    }

    private async Task ApplyAuthorizationAsync(AuthenticationInfo info, HttpClient client)
    {
        // login/pass info
        if (string.IsNullOrWhiteSpace(_webApiUserPass) || string.IsNullOrWhiteSpace(_webApiUserPass))
            return; // no authentication info

        try
        {
            // check if still ok
            client.DefaultRequestHeaders.Authorization = info.Value;
            var securityCheckUri = new Uri($"{_webApiBaseUri}/Security/currentUser");
            using var securityCheckResponse = await client.GetAsync(securityCheckUri);
            if (securityCheckResponse.IsSuccessStatusCode)
            {
                this._logger.LogDebug("auth_check: no security or still good");
                return;
            }

            if (securityCheckResponse.StatusCode != HttpStatusCode.Unauthorized)
            {
                // no error from this context, wait for a consummer to highlight this one
                this._logger.LogWarning("auth_check: fail to contact webapi");
                return;
            }

            // reset auth info
            info.Value = null;

            // login phase
            var loginData = new { userName = _webApiUserName, userPass = _webApiUserPass, };
            var securityLoginUri = new Uri($"{_webApiBaseUri}/Security/login");
            using var securityLoginResponse = await client.PostAsync(securityLoginUri, JsonContent.Create(loginData));
            if (!securityLoginResponse.IsSuccessStatusCode)
            {
                this._logger.LogWarning("auth_login: user/pass failed or refused => [{StatusCode}] {Message}",
                    securityLoginResponse.StatusCode,
                    await securityLoginResponse.Content.ReadAsStringAsync());
                return;
            }

            var user = await securityLoginResponse.DeseriaseJsonResponseAsync<UserTokenDto>();
            client.DefaultRequestHeaders.Authorization = info.Value = new AuthenticationHeaderValue("Bearer", user.Token);
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex, "auth_login: unexpected error occured during authorization handshake");
        }
    }
}

public record AuthenticationInfo
{
    public AuthenticationHeaderValue? Value { get; set; }
}