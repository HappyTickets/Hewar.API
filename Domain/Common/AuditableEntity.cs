﻿namespace Domain.Common
{
    public abstract class AuditableEntity: BaseEntity
    {
        public string? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
