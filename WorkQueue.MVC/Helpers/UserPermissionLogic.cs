using System.Collections.Generic;
using System.Threading.Tasks;
using WorkQueue.MVC.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.DirectoryServices;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using WorkQueue.MVC.Helpers;

public class UserPermissionLogic : IUserPermissionLogic
{
    private readonly IConfigurationSection _AD_Domain;
    private readonly IConfigurationSection _Config;
    
    
    public UserPermissionLogic(IConfiguration config)
    {
        _AD_Domain = config.GetSection("AD_Domain");
        _Config = config.GetSection("UserPermission");
    }
    private string CreateUser
    {
        get => _Config["CreateUser"];
    }

    private string WorkQUser
    {
        get => _Config["WorkQUser"];
    }

    private string Admin
    {
        get => _Config["Admin"];
    }

    private string AdDomain
    {
        get => _AD_Domain.Value;
    }
    
    public List<string> GetGroupNames(string fullyQualifiedName)
    {
        if (string.IsNullOrWhiteSpace(fullyQualifiedName)) throw new ArgumentException("fullyQualifiedName is null or empty", fullyQualifiedName);

        string[] output = null;

        using (var ctx = new PrincipalContext(ContextType.Domain, AdDomain))
        using (var user = UserPrincipal.FindByIdentity(ctx, fullyQualifiedName))
        {
            if (user != null)
            {
                output = user.GetGroups() //this returns a collection of principal objects
                    .Select(x => x.SamAccountName) // select the name.  you may change this to choose the display name or whatever you want
                    .ToArray(); // convert to string array
            }
        }
       
        return output.ToList();
    }

    public UserPermissions GetUserPermissions(string activeDirectoryUserName)
    {
        var ActiveDirectory = GetGroupNames(activeDirectoryUserName);

        if (ActiveDirectory.Contains(Admin))
        {
            return UserPermissions.Admin;
        }
        else if (ActiveDirectory.Contains(WorkQUser))
        {
            return UserPermissions.WorkQUser;
        }
        else if (ActiveDirectory.Contains(CreateUser))
        {
            return UserPermissions.CreateUser;
        }
        else
        {
            return UserPermissions.Public;
        }
    }

    public string GetDisplayName(string userlogin)
    {
        using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain))
        using (UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, userlogin))
        {

            if (userPrincipal != null)
                return string.Format("{0} {1}", userPrincipal.GivenName, userPrincipal.Surname).Trim();
            else
                return string.Empty;
        }
    }



}

