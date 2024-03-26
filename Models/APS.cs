using System;
using Autodesk.SDKManager;
using Autodesk.Authentication.Model;
using System.Collections.Generic;

public class Tokens
{
    public string InternalToken;
    public string PublicToken;
    public string RefreshToken;
    public DateTime ExpiresAt;
}

public partial class APS
{
    private readonly SDKManager _sdkManager;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _callbackUri;
    private readonly List<Scopes> InternalTokenScopes = new List<Scopes> { Scopes.DataRead, Scopes.ViewablesRead };
    private readonly List<Scopes> PublicTokenScopes = new List<Scopes> { Scopes.ViewablesRead };

    public APS(string clientId, string clientSecret, string callbackUri)
    {
        _sdkManager = SdkManagerBuilder.Create().Build();
        _clientId = clientId;
        _clientSecret = clientSecret;
        _callbackUri = callbackUri;
    }
}
