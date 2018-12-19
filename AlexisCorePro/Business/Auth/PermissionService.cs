using AlexisCorePro.Business.Users;
using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Enums;
using DelegateDecompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Business.Auth
{
    public class PermissionService
    {
        private readonly DatabaseContext ctx;

        public PermissionService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public bool IsUserAuthorized(int? resourceId, ResourceType resourceType)
        {
            var userId = ctx.CurrentUser?.Id;

            if (userId == null || resourceId == null)
            {
                return false;
            }

            switch (resourceType)
            {
                case ResourceType.Ship:
                    return IsUserShipOwner(userId, resourceId);
                default:
                    return false;
            }
        }

        private bool IsUserShipOwner(int? userId, int? shipId)
        {
            return ctx.Ships.Where(s => s.HasAccess(userId)).Decompile().Count() > 0;
        }
    }
}
