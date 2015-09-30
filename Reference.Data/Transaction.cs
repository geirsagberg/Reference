using System;
using System.Transactions;
using Reference.Common.Contracts.Data;

namespace Reference.Data
{
    internal class Transaction : ITransaction
    {
        private readonly TransactionScope transactionScope;

        public Transaction()
        {
            transactionScope = new TransactionScope();
        }

        public Transaction(TimeSpan timeout)
        {
            transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = timeout
            });
        }

        public void Dispose()
        {
            transactionScope?.Dispose();
        }

        public void Complete()
        {
            transactionScope.Complete();
        }
    }
}