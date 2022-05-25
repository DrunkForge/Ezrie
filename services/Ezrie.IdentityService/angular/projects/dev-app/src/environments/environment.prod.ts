import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'IdentityService',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44318',
    redirectUri: baseUrl,
    clientId: 'IdentityService_App',
    responseType: 'code',
    scope: 'offline_access IdentityService',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44318',
      rootNamespace: 'Ezrie.IdentityService',
    },
    IdentityService: {
      url: 'https://localhost:44350',
      rootNamespace: 'Ezrie.IdentityService',
    },
  },
} as Environment;