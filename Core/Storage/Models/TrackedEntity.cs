using System;

namespace ShopsRUs.Service.Core.Storage.Models
{
    public abstract class TrackedEntity
    {
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public ShopRusUser CreatedBy { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public string ModifiedById { get; set; }
        public ShopRusUser ModifiedBy { get; set; }
    }
}