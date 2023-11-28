using System;
using System.Collections.Generic;
using Autodesk.SDKManager;
using Autodesk.Authentication.Model;

public class Tokens
{
    public string InternalToken;
    public string PublicToken;
    public string RefreshToken;
    public DateTime ExpiresAt;
}

public partial class APS
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _callbackUri;
    private SDKManager _SDKManager;
    private readonly List<Scopes> InternalTokenScopes = new List<Scopes> { Scopes.DataRead, Scopes.ViewablesRead };
    private readonly List<Scopes> PublicTokenScopes = new List<Scopes> { Scopes.ViewablesRead };

    public APS(string clientId, string clientSecret, string callbackUri)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
        _callbackUri = callbackUri;
        _SDKManager = SdkManagerBuilder.Create().Build();
    }
}
