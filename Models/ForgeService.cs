using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autodesk.Forge;

namespace forge_hubs_browser_dotnet
{
    public class Tokens
    {
        public string InternalToken;
        public string PublicToken;
        public string RefreshToken;
        public DateTime ExpiresAt;
    }

    public interface IForgeService
    {
        string GetAuthorizationURL();
        Task<Tokens> GenerateTokens(string code);
        Task<Tokens> RefreshTokens(Tokens tokens);
        Task<dynamic> GetUserProfile(Tokens tokens);
        Task<IEnumerable<dynamic>> GetHubs(Tokens tokens);
        Task<IEnumerable<dynamic>> GetProjects(string hubId, Tokens tokens);
        Task<IEnumerable<dynamic>> GetContents(string hubId, string projectId, string folderId, Tokens tokens);
        Task<IEnumerable<dynamic>> GetVersions(string hubId, string projectId, string itemId, Tokens tokens);
    }

    public partial class ForgeService : IForgeService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _callbackUri;
        private readonly Scope[] InternalTokenScopes = new Scope[] { Scope.DataRead, Scope.ViewablesRead };
        private readonly Scope[] PublicTokenScopes = new Scope[] { Scope.ViewablesRead };

        public ForgeService(string clientId, string clientSecret, string callbackUri)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _callbackUri = callbackUri;
        }
    }
}