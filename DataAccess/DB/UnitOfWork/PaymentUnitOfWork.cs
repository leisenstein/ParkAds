using DataAccess.DB.Contexts;
using DataAccess.DB.Interfaces;
using DataAccess.DB.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DB.UnitOfWork
{
    public class PaymentUnitOfWork : IUnitOfWork<IPaymentRepository>
    {
        private readonly PaymentContext context;

        public PaymentUnitOfWork(PaymentContext context)
        {
            this.context = context;
            Repository = new PaymentRepository(this.context);
        }

        public IPaymentRepository Repository { get; private set; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
