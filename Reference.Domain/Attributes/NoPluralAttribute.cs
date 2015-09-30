using System;

namespace Reference.Domain.Attributes
{
    /// <summary>
    /// Entities marked with this attribute will not have pluralized DbSet names.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class NoPluralAttribute : Attribute {}
}