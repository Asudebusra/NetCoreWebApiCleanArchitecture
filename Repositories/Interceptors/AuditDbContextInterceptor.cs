using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Repositories.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviors = new()
        {
            {EntityState.Added,AddBehavior },
            {EntityState.Modified, ModifiedBehavior }
        };

        private static void AddBehavior(DbContext context,IAuditEntity auditEntity)
        {
            auditEntity.Created = DateTime.Now;
            context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;//update etmemek için yazdık ef core a dedik
        }

        private static void ModifiedBehavior(DbContext context, IAuditEntity auditEntity)
        {
            context.Entry(auditEntity).Property(x => x.Created).IsModified = false;//create etmemek için yazdık ef core a dedik
            auditEntity.Updated = DateTime.Now;
        }


        //Delegeler methodları işaret eder. Dönüş tipi aynı, aldıkları parametreler aynı
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {

            foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                //state ve modified da 2 tane if de de aynısını yazacağım için best practices kullandık

                if (entityEntry.Entity is not IAuditEntity auditEntity) continue;

                //if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
                //{
                //    Behaviors[entityEntry.State](eventData.Context, auditEntity);
                //}

                if (entityEntry.State is not (EntityState.Added or EntityState.Modified)) continue;

                Behaviors[entityEntry.State](eventData.Context, auditEntity);


                //switch (entityEntry.State)
                //{
                //    case EntityState.Added:

                //        AddBehavior(eventData.Context,auditEntity);
                //        break;

                //    case EntityState.Modified:

                //        ModifiedBehavior(eventData.Context, auditEntity);
                //        break;
                //}



            }



            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
