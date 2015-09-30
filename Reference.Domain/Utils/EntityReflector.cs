using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reference.Common.Contracts.Data;
using Reference.Common.Extensions;
using Reference.Common.Utils;
using Reference.Domain.Attributes;

namespace Reference.Domain.Utils
{
    /// <summary>
    /// Used by T4 template to generate DbSets
    /// </summary>
    public class EntityReflector
    {
        private EntityReflector(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            EntityType = type;
        }

        public Type EntityType { get; }
        public string FullName => EntityType.FullName;
        public string Name => EntityType.Name;
        public bool HasId => EntityType.Implements<IEntityWithId>();
        public bool HasVersion => EntityType.Implements<IVersioned>();
        public bool SkipTable => EntityType.HasAttribute<SkipTableAttribute>();

        public string SetName
        {
            get
            {
                if (EntityType.HasAttribute<NoPluralAttribute>())
                    return Name;
                if (EntityType.HasAttribute<PluralAttribute>())
                    return EntityType.GetCustomAttribute<PluralAttribute>().PluralForm;
                return Name.Pluralize();
            }
        }

        public IEnumerable<ISetProperty> Sets
        {
            get
            {
                var listType = typeof (ISet<>);

                return EntityType.GetProperties()
                    .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == listType)
                    .OrderBy(p => p.Name)
                    .Select(property => new SetProperty(this, property));
            }
        }

        public IEnumerable<IProperty> Entities
        {
            get
            {
                return EntityType.GetProperties()
                    .Where(p => p.PropertyType.IsClass && p.PropertyType.Implements<IEntity>())
                    .OrderBy(p => p.Name)
                    .Select(e => new EntityProperty(e));
            }
        }

        public static IEnumerable<EntityReflector> All
        {
            get
            {
                var ass = Assembly.GetExecutingAssembly();
                return ass.GetTypes()
                    .Where(type => type.IsClass && type.Implements<IEntity>() && !type.IsAbstract)
                    .OrderBy(t => t.Name)
                    .Select(t => new EntityReflector(t));
            }
        }

        public EntityReflector Parent => GetParent(EntityType);

        private static EntityReflector GetParent(Type type)
        {
            while (true) {
                var parentType = type.BaseType;
                if (parentType == null) return null;
                if (parentType.Implements<IEntity>()) return new EntityReflector(parentType);
                type = parentType;
            }
        }

        private class EntityProperty : IProperty
        {
            private readonly PropertyInfo _property;

            public EntityProperty(PropertyInfo property)
            {
                if (property == null) throw new ArgumentNullException(nameof(property));
                _property = property;
            }

            public Type EntityType => _property.PropertyType;
            public string Name => _property.Name;
            public EntityReflector Reflector => new EntityReflector(EntityType);

            public PropertyInfo IdProperty
            {
                get
                {
                    if (typeof (IEntityWithId).IsAssignableFrom(EntityType)) {
                        var prop = _property.ReflectedType?.GetProperty(Name + "Id");
                        if (prop != null && (prop.PropertyType == typeof (int) || prop.PropertyType == typeof (int?))) {
                            return prop;
                        }
                    }
                    return null;
                }
            }
        }

        private class SetProperty : ISetProperty
        {
            private readonly EntityReflector _parent;
            private readonly PropertyInfo _property;

            public SetProperty(EntityReflector parent, PropertyInfo property)
            {
                _parent = parent;
                _property = property;
            }

            public Type EntityType => _property.PropertyType.GetGenericArguments().First();
            public string Name => _property.Name;

            public IProperty ReverseProperty
            {
                get
                {
                    var parentName = _parent.EntityType.Name;
                    var prop = EntityType.GetProperty(parentName);
                    if (prop != null && prop.PropertyType == _parent.EntityType) {
                        return new EntityProperty(prop);
                    }
                    return null;
                }
            }
        }

        public interface IPropertyBase
        {
            Type EntityType { get; }
            string Name { get; }
        }

        public interface IProperty : IPropertyBase
        {
            EntityReflector Reflector { get; }
            PropertyInfo IdProperty { get; }
        }

        public interface ISetProperty : IPropertyBase
        {
            IProperty ReverseProperty { get; }
        }
    }
}