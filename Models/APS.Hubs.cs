using System.Collections.Generic;
using System.Threading.Tasks;
// using Autodesk.Forge;
// using Autodesk.Forge.Model;
using Autodesk.DataManagement;
using Autodesk.DataManagement.Model;
using System;

public partial class APS
{
    public async Task<IEnumerable<dynamic>> GetHubs(Tokens tokens)
    {
        DataManagementClient dataManagementClient = new DataManagementClient(_SDKManager);
        var getHubs = await dataManagementClient.GetHubsAsync(accessToken: tokens.InternalToken);
        List<HubsData> hubsData = getHubs.Data;

        return hubsData;
    }

    public async Task<IEnumerable<dynamic>> GetProjects(string hubId, Tokens tokens)
    {
        DataManagementClient dataManagementClient = new DataManagementClient(_SDKManager);
        var getHubProjects = await dataManagementClient.GetHubProjectsAsync(hubId: hubId, accessToken: tokens.InternalToken);
        List<ProjectsData> projectsData = getHubProjects.Data;

        return projectsData;
    }

    public async Task<IEnumerable<dynamic>> GetContents(string hubId, string projectId, string folderId, Tokens tokens)
    {
        DataManagementClient dataManagementClient = new DataManagementClient(_SDKManager);
        if (string.IsNullOrEmpty(folderId))
        {
            var projectTopFolders = await dataManagementClient.GetProjectTopFoldersAsync(hubId: hubId, projectId: projectId, accessToken: tokens.InternalToken);
            List<TopFoldersData> topFoldersData =  projectTopFolders.Data;

            return topFoldersData;
        }
        
        FolderContents folderContents = await dataManagementClient.GetFolderContentsAsync(projectId: projectId, folderId: folderId, accessToken: tokens.InternalToken);
        List<FolderContentsData> folderContentsData = folderContents.Data;

        return folderContentsData;
    }

    public async Task<IEnumerable<dynamic>> GetVersions(string hubId, string projectId, string itemId, Tokens tokens)
    {
        DataManagementClient dataManagementClient = new DataManagementClient(_SDKManager);
        Versions versions = await dataManagementClient.GetItemVersionsAsync(projectId, itemId, accessToken: tokens.InternalToken);

        return versions.Data;
    }
}
