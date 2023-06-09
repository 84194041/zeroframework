﻿using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace ZeroFramework.DeviceCenter.Application.Services.Permissions
{
    public class PermissionDefinitionManager : IPermissionDefinitionManager
    {
        private readonly Lazy<Dictionary<string, PermissionGroupDefinition>> _lazyPermissionGroupDefinitions;

        protected IDictionary<string, PermissionGroupDefinition> PermissionGroupDefinitions => _lazyPermissionGroupDefinitions.Value;

        private readonly Lazy<Dictionary<string, PermissionDefinition>> _lazyPermissionDefinitions;

        protected IDictionary<string, PermissionDefinition> PermissionDefinitions => _lazyPermissionDefinitions.Value;

        private readonly IServiceProvider _serviceProvider;

        public PermissionDefinitionManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _lazyPermissionDefinitions = new Lazy<Dictionary<string, PermissionDefinition>>(CreatePermissionDefinitions, isThreadSafe: true);
            _lazyPermissionGroupDefinitions = new Lazy<Dictionary<string, PermissionGroupDefinition>>(CreatePermissionGroupDefinitions, isThreadSafe: true);
        }

        public PermissionDefinition Get(string name)
        {
            var permission = GetOrNull(name);

            if (permission == null)
            {
                throw new InvalidOperationException($"Undefined permission {name}");
            }

            return permission;
        }

        public IReadOnlyList<PermissionGroupDefinition> GetGroups() => PermissionGroupDefinitions.Values.ToImmutableList();

        public PermissionDefinition? GetOrNull(string name) => PermissionDefinitions.TryGetValue(name, out var obj) ? obj : default;

        public IReadOnlyList<PermissionDefinition> GetPermissions() => PermissionDefinitions.Values.ToImmutableList();

        protected virtual Dictionary<string, PermissionDefinition> CreatePermissionDefinitions()
        {
            var permissions = new Dictionary<string, PermissionDefinition>();

            foreach (var groupDefinition in PermissionGroupDefinitions.Values)
            {
                foreach (var permission in groupDefinition.Permissions)
                {
                    AddPermissionToDictionaryRecursively(permissions, permission);
                }
            }

            return permissions;
        }

        protected virtual void AddPermissionToDictionaryRecursively(Dictionary<string, PermissionDefinition> permissions, PermissionDefinition permission)
        {
            if (permissions.ContainsKey(permission.Name))
            {
                throw new InvalidOperationException($"Duplicate permission name {permission.Name}");
            }

            permissions[permission.Name] = permission;

            foreach (var child in permission.Children)
            {
                AddPermissionToDictionaryRecursively(permissions, child);
            }
        }

        protected virtual Dictionary<string, PermissionGroupDefinition> CreatePermissionGroupDefinitions()
        {
            using var scope = _serviceProvider.CreateScope();

            var context = new PermissionDefinitionContext(scope.ServiceProvider);

            var providers = _serviceProvider.GetServices<IPermissionDefinitionProvider>();

            foreach (IPermissionDefinitionProvider provider in providers)
            {
                provider.Define(context);
            }

            return context.Groups;
        }
    }
}