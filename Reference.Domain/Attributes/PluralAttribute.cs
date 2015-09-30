using System;

namespace Reference.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class PluralAttribute : Attribute
    {
        public string PluralForm { get; set; }

        /// <summary>
        /// Use this attribute to manually specify a plural form for the generated DbSet.
        /// </summary>
        public PluralAttribute(string pluralForm)
        {
            PluralForm = pluralForm;
        }
    }
}