using System;
using System.Threading.Tasks;
using Autodesk.Authentication;
using Autodesk.Authentication.Model;

public partial class APS
{
    public string GetAuthorizationURL()
    {
        AuthenticationClient authenticationClient = new AuthenticationClient(_SDKManager);
        ResponseType responseType = ResponseType.Code;
        return authenticationClient.Authorize(_clientId, responseType, _callbackUri, InternalTokenScopes);

    }

    public async Task<Tokens> GenerateTokens(string code)
    {
        AuthenticationClient authenticationClient = new AuthenticationClient(_SDKManager);
        dynamic internalAuth = await authenticationClient.GetThreeLeggedTokenAsync(_clientId, _clientSecret, code, _callbackUri);
        dynamic publicAuth = await authenticationClient.GetRefreshTokenAsync(_clientId, _clientSecret, internalAuth.RefreshToken, PublicTokenScopes);
        
        return new Tokens
        {
            PublicToken = publicAuth.AccessToken,
            InternalToken = internalAuth.AccessToken,
            RefreshToken = publicAuth._RefreshToken,
            ExpiresAt = DateTime.Now.ToUniversalTime().AddSeconds(internalAuth.ExpiresIn)
        };
    }

    public async Task<Tokens> RefreshTokens(Tokens tokens)
    {
        AuthenticationClient authenticationClient = new AuthenticationClient(_SDKManager);
        dynamic internalAuth = await authenticationClient.GetRefreshTokenAsync(_clientId, _clientSecret, tokens.RefreshToken, InternalTokenScopes);
        dynamic publicAuth = await authenticationClient.GetRefreshTokenAsync(_clientId, _clientSecret, internalAuth._RefreshToken, PublicTokenScopes);
        return new Tokens
        {
            PublicToken = publicAuth.AccessToken,
            InternalToken = internalAuth.AccessToken,
            RefreshToken = publicAuth._RefreshToken,
            ExpiresAt = DateTime.Now.ToUniversalTime().AddSeconds(internalAuth.ExpiresIn)
        };
    }

    public async Task<dynamic> GetUserProfile(Tokens tokens)
    {
        AuthenticationClient authenticationClient = new AuthenticationClient(_SDKManager);
        dynamic profile = await authenticationClient.GetUserInfoAsync(tokens.InternalToken);
        return profile;
    }
}
