using AlexisCorePro.Business.Common.Model.Search;
using AlexisCorePro.Business.Ships;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain
{
    public static class DatabaseContextExtensions
    {
        public static IQueryable<ShipMonthReport> ToShipMonthReport(this DatabaseContext ctx, DateTime date)
        {
            var query = from s in ctx.Ships
                        join eq in ctx.Equipments on s.Id equals eq.ShipId
                        select new ShipMonthReport
                        {
                            Id = s.Id,
                            NewEquipmentNum = s.NewEquipmentNum,
                            UpdatedEquipmentNum = s.UpdatedEquipmentNum(date)
                        };

            return query;
        }

        /// <summary>
        /// Adds pagination to IQueryable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageNumber">Starts from 1 when sending from FE</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<SearchResponse<TResult>> ToPaginated<TResult>(this IQueryable<TResult> query, int pageNumber, int pageSize)
        {
            query = query.Skip(pageNumber - 1).Take(pageSize);

            return new SearchResponse<TResult>
            {
                PageSize = pageSize,
                CurrentPage = pageNumber,
                EntriesCount = await query.DecompileAsync().CountAsync(),
                Result = await query.DecompileAsync().ToListAsync()
            };
        }
    }
}
