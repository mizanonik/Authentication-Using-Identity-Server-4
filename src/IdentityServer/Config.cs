// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };  

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[] 
            { 
                new ApiResource("api1", "My API")
            };
        
        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client{
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientId = "client",
                    ClientSecrets = {new Secret("secret".Sha256())},

                    AllowedScopes = {"api1"}
                },
                new Client{
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris = { "http://localhost:5002/signin-oidc" }, 

                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                },
                new Client{
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientId = "js",
                    ClientName = "JavaScript.Client",
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    
                    AllowedCorsOrigins = { "http://localhost:5003" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                }
            };
        
    }
}