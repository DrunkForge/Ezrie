import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'TenantService',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44328',
    redirectUri: baseUrl,
    clientId: 'TenantService_App',
    responseType: 'code',
    scope: 'offline_access TenantService',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44328',
      rootNamespace: 'Ezrie.TenantService',
    },
    TenantService: {
      url: 'https://localhost:44315',
      rootNamespace: 'Ezrie.TenantService',
    },
  },
} as Environment;
