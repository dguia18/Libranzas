﻿using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IEmpleadoRepository EmpleadoRepository { get; }
        ICreditoRepository CreditoRepository { get; }
        int Commit();
    }
}
