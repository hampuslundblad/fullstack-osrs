/* eslint-disable */

// @ts-nocheck

// noinspection JSUnusedGlobalSymbols

// This file was automatically generated by TanStack Router.
// You should NOT make any changes in this file as it will be overwritten.
// Additionally, you should also exclude this file from your linter and/or formatter to prevent it from being checked or modified.

import { createFileRoute } from '@tanstack/react-router'

// Import Routes

import { Route as rootRoute } from './routes/__root'
import { Route as LoginImport } from './routes/login'
import { Route as AuthImport } from './routes/_auth'
import { Route as AuthMygroupsIndexImport } from './routes/_auth.mygroups.index'
import { Route as AuthMygroupsGroupNameImport } from './routes/_auth.mygroups.$groupName'

// Create Virtual Routes

const HiscoreLazyImport = createFileRoute('/hiscore')()
const IndexLazyImport = createFileRoute('/')()

// Create/Update Routes

const HiscoreLazyRoute = HiscoreLazyImport.update({
  id: '/hiscore',
  path: '/hiscore',
  getParentRoute: () => rootRoute,
} as any).lazy(() => import('./routes/hiscore.lazy').then((d) => d.Route))

const LoginRoute = LoginImport.update({
  id: '/login',
  path: '/login',
  getParentRoute: () => rootRoute,
} as any)

const AuthRoute = AuthImport.update({
  id: '/_auth',
  getParentRoute: () => rootRoute,
} as any)

const IndexLazyRoute = IndexLazyImport.update({
  id: '/',
  path: '/',
  getParentRoute: () => rootRoute,
} as any).lazy(() => import('./routes/index.lazy').then((d) => d.Route))

const AuthMygroupsIndexRoute = AuthMygroupsIndexImport.update({
  id: '/mygroups/',
  path: '/mygroups/',
  getParentRoute: () => AuthRoute,
} as any)

const AuthMygroupsGroupNameRoute = AuthMygroupsGroupNameImport.update({
  id: '/mygroups/$groupName',
  path: '/mygroups/$groupName',
  getParentRoute: () => AuthRoute,
} as any)

// Populate the FileRoutesByPath interface

declare module '@tanstack/react-router' {
  interface FileRoutesByPath {
    '/': {
      id: '/'
      path: '/'
      fullPath: '/'
      preLoaderRoute: typeof IndexLazyImport
      parentRoute: typeof rootRoute
    }
    '/_auth': {
      id: '/_auth'
      path: ''
      fullPath: ''
      preLoaderRoute: typeof AuthImport
      parentRoute: typeof rootRoute
    }
    '/login': {
      id: '/login'
      path: '/login'
      fullPath: '/login'
      preLoaderRoute: typeof LoginImport
      parentRoute: typeof rootRoute
    }
    '/hiscore': {
      id: '/hiscore'
      path: '/hiscore'
      fullPath: '/hiscore'
      preLoaderRoute: typeof HiscoreLazyImport
      parentRoute: typeof rootRoute
    }
    '/_auth/mygroups/$groupName': {
      id: '/_auth/mygroups/$groupName'
      path: '/mygroups/$groupName'
      fullPath: '/mygroups/$groupName'
      preLoaderRoute: typeof AuthMygroupsGroupNameImport
      parentRoute: typeof AuthImport
    }
    '/_auth/mygroups/': {
      id: '/_auth/mygroups/'
      path: '/mygroups'
      fullPath: '/mygroups'
      preLoaderRoute: typeof AuthMygroupsIndexImport
      parentRoute: typeof AuthImport
    }
  }
}

// Create and export the route tree

interface AuthRouteChildren {
  AuthMygroupsGroupNameRoute: typeof AuthMygroupsGroupNameRoute
  AuthMygroupsIndexRoute: typeof AuthMygroupsIndexRoute
}

const AuthRouteChildren: AuthRouteChildren = {
  AuthMygroupsGroupNameRoute: AuthMygroupsGroupNameRoute,
  AuthMygroupsIndexRoute: AuthMygroupsIndexRoute,
}

const AuthRouteWithChildren = AuthRoute._addFileChildren(AuthRouteChildren)

export interface FileRoutesByFullPath {
  '/': typeof IndexLazyRoute
  '': typeof AuthRouteWithChildren
  '/login': typeof LoginRoute
  '/hiscore': typeof HiscoreLazyRoute
  '/mygroups/$groupName': typeof AuthMygroupsGroupNameRoute
  '/mygroups': typeof AuthMygroupsIndexRoute
}

export interface FileRoutesByTo {
  '/': typeof IndexLazyRoute
  '': typeof AuthRouteWithChildren
  '/login': typeof LoginRoute
  '/hiscore': typeof HiscoreLazyRoute
  '/mygroups/$groupName': typeof AuthMygroupsGroupNameRoute
  '/mygroups': typeof AuthMygroupsIndexRoute
}

export interface FileRoutesById {
  __root__: typeof rootRoute
  '/': typeof IndexLazyRoute
  '/_auth': typeof AuthRouteWithChildren
  '/login': typeof LoginRoute
  '/hiscore': typeof HiscoreLazyRoute
  '/_auth/mygroups/$groupName': typeof AuthMygroupsGroupNameRoute
  '/_auth/mygroups/': typeof AuthMygroupsIndexRoute
}

export interface FileRouteTypes {
  fileRoutesByFullPath: FileRoutesByFullPath
  fullPaths:
    | '/'
    | ''
    | '/login'
    | '/hiscore'
    | '/mygroups/$groupName'
    | '/mygroups'
  fileRoutesByTo: FileRoutesByTo
  to: '/' | '' | '/login' | '/hiscore' | '/mygroups/$groupName' | '/mygroups'
  id:
    | '__root__'
    | '/'
    | '/_auth'
    | '/login'
    | '/hiscore'
    | '/_auth/mygroups/$groupName'
    | '/_auth/mygroups/'
  fileRoutesById: FileRoutesById
}

export interface RootRouteChildren {
  IndexLazyRoute: typeof IndexLazyRoute
  AuthRoute: typeof AuthRouteWithChildren
  LoginRoute: typeof LoginRoute
  HiscoreLazyRoute: typeof HiscoreLazyRoute
}

const rootRouteChildren: RootRouteChildren = {
  IndexLazyRoute: IndexLazyRoute,
  AuthRoute: AuthRouteWithChildren,
  LoginRoute: LoginRoute,
  HiscoreLazyRoute: HiscoreLazyRoute,
}

export const routeTree = rootRoute
  ._addFileChildren(rootRouteChildren)
  ._addFileTypes<FileRouteTypes>()

/* ROUTE_MANIFEST_START
{
  "routes": {
    "__root__": {
      "filePath": "__root.tsx",
      "children": [
        "/",
        "/_auth",
        "/login",
        "/hiscore"
      ]
    },
    "/": {
      "filePath": "index.lazy.tsx"
    },
    "/_auth": {
      "filePath": "_auth.tsx",
      "children": [
        "/_auth/mygroups/$groupName",
        "/_auth/mygroups/"
      ]
    },
    "/login": {
      "filePath": "login.tsx"
    },
    "/hiscore": {
      "filePath": "hiscore.lazy.tsx"
    },
    "/_auth/mygroups/$groupName": {
      "filePath": "_auth.mygroups.$groupName.tsx",
      "parent": "/_auth"
    },
    "/_auth/mygroups/": {
      "filePath": "_auth.mygroups.index.tsx",
      "parent": "/_auth"
    }
  }
}
ROUTE_MANIFEST_END */
