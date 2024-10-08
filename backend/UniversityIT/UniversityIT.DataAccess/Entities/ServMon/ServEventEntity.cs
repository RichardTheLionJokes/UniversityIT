﻿using UniversityIT.DataAccess.Entities.Common;

namespace UniversityIT.DataAccess.Entities.ServMon
{
    public class ServEventEntity
    {
        public Guid Id { get; set; }
        public DateTime HappenedAt { get; set; }
        public int ServStatusId { get; set; }
        public NetStatusEntity? ServStatus { get; set; }
        public Guid ServerId { get; set; }
        public ServerEntity? Server { get; set; }
    }
}