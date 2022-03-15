using System;
using System.Collections.Generic;
using Givt.OnlineCheckout.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Givt.OnlineCheckout.Infrastructure.DbContexts
{
    /// <summary>
    /// Contains a set of automatic operations against Database
    /// </summary>
    internal static class DbContextUpdateOperations
    {
        /// <summary>
        /// Automatically updates the BaseEntity properties without any developer action
        /// </summary>
        /// <param name="changes">The collection of BaseEntity entities for save to DB</param>
        public static void UpdateDates(IEnumerable<EntityEntry<AuditableEntity>> changes)
        {
            DateTime now = DateTime.UtcNow;
            foreach (var change in changes)
            {
                switch (change.State)
                {
                    case EntityState.Added:
                        change.Entity.DateCreated = now;
                        change.Entity.DateModified = now;
                        break;

                    case EntityState.Modified:
                        change.Entity.DateModified = now;
                        break;
                }
            }
        }
    }
}
