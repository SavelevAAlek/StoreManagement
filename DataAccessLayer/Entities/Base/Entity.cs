﻿using Interfaces;

namespace DataAccessLayer.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
