﻿
//This file is generated by a T4 template.
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Reference.Domain.Entities;
using Reference.Common.Contracts.Data;

namespace Reference.Data
{
	[System.CodeDom.Compiler.GeneratedCode("T4",null)]
    public abstract class EntityContextBase : DbContext
	{

	protected EntityContextBase() {}
	protected EntityContextBase(string name) : base(name) {}
	protected EntityContextBase(DbConnection connection) : base(connection, true) {} 
 
		#region Reference.Domain.Entities.Kjøp
		    
        /// <summary>
        /// The db set for <see cref="Reference.Domain.Entities.Kjøp"/>.
        /// </summary>
        public virtual DbSet<Kjøp> Kjøp => Set<Kjøp>();

        /// <summary>
        /// Maps the type <see cref="Reference.Domain.Entities.Kjøp"/> to the table defined by <seealso cref="TableAttribute.Name"/>.
        /// </summary>
		protected virtual void MapKjøp(EntityTypeConfiguration<Kjøp> config){
			config.ToTable("Kjøp");
			config.Property(v=>v.Version).IsConcurrencyToken().IsRowVersion(); 
			config.HasKey(e=>e.Id); 
		}

		#endregion
 
		#region Reference.Domain.Entities.Person
		    
        /// <summary>
        /// The db set for <see cref="Reference.Domain.Entities.Person"/>.
        /// </summary>
        public virtual DbSet<Person> Personer => Set<Person>();

        /// <summary>
        /// Maps the type <see cref="Reference.Domain.Entities.Person"/> to the table defined by <seealso cref="TableAttribute.Name"/>.
        /// </summary>
		protected virtual void MapPerson(EntityTypeConfiguration<Person> config){
			config.ToTable("Person");
			config.Property(v=>v.Version).IsConcurrencyToken().IsRowVersion(); 
			config.HasKey(e=>e.Id); 
		}

		#endregion
 
		#region Reference.Domain.Entities.Vare
		    
        /// <summary>
        /// The db set for <see cref="Reference.Domain.Entities.Vare"/>.
        /// </summary>
        public virtual DbSet<Vare> Varer => Set<Vare>();

        /// <summary>
        /// Maps the type <see cref="Reference.Domain.Entities.Vare"/> to the table defined by <seealso cref="TableAttribute.Name"/>.
        /// </summary>
		protected virtual void MapVare(EntityTypeConfiguration<Vare> config){
			config.ToTable("Vare");
			config.Property(v=>v.Version).IsConcurrencyToken().IsRowVersion(); 
			config.HasKey(e=>e.Id); 
		}

		#endregion
 
		#region Reference.Domain.Entities.VareLinje
		    
        /// <summary>
        /// The db set for <see cref="Reference.Domain.Entities.VareLinje"/>.
        /// </summary>
        public virtual DbSet<VareLinje> VareLinjer => Set<VareLinje>();

        /// <summary>
        /// Maps the type <see cref="Reference.Domain.Entities.VareLinje"/> to the table defined by <seealso cref="TableAttribute.Name"/>.
        /// </summary>
		protected virtual void MapVareLinje(EntityTypeConfiguration<VareLinje> config){
			config.ToTable("VareLinje");
			config.Property(v=>v.Version).IsConcurrencyToken().IsRowVersion(); 
			config.HasKey(e=>e.Id); 
		}

		#endregion

        /// <summary>
        /// Maps all entity classes to database tables.
        /// </summary>
		protected void MapTypesToTable(DbModelBuilder modelBuilder){
			MapKjøp(modelBuilder.Entity<Kjøp>());
			MapPerson(modelBuilder.Entity<Person>());
			MapVare(modelBuilder.Entity<Vare>());
			MapVareLinje(modelBuilder.Entity<VareLinje>());
		}
	}
}