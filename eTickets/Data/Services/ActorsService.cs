﻿using eTickets.Data.Base;
using eTickets.Data.IServices;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
    {
    public class ActorsService : EntityBaseRepository<Actor>, IActorService
        {
        public ActorsService(AppDbContext context) : base(context) { }
        }
    }
