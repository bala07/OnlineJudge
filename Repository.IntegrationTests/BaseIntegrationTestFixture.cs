using System.Transactions;

using DataAccessLayer;

using NUnit.Framework;

namespace Repository.IntegrationTests
{
    [TestFixture]
    public class BaseIntegrationTestFixture
    {
        protected OnlineJudgeContext OnlineJudgeContext;

        protected TransactionScope TransactionScope;

        [TestFixtureSetUp]
        public void InitialiseDbContext()
        {
            this.OnlineJudgeContext = new OnlineJudgeContext();
            this.OnlineJudgeContext.Database.CreateIfNotExists();
        }

        [SetUp]
        public void BeginTransaction()
        {
            this.TransactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);
        }

        [TearDown]
        public void EndTransaction()
        {
            this.TransactionScope.Dispose();
        }
    }
}
