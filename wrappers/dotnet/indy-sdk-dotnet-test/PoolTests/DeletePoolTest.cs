﻿using Hyperledger.Indy.Sdk.PoolApi;
using Hyperledger.Indy.Sdk.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Hyperledger.Indy.Sdk.Test.PoolTests
{
    [TestClass]
    public class DeletePoolTest : IndyIntegrationTestBase
    {
        [TestMethod]
        public async Task TestDeletePoolWorks()
        {
            var poolName = PoolUtils.CreatePoolLedgerConfig();
            await Pool.DeletePoolLedgerConfigAsync(poolName);
        }

        [TestMethod]
        public async Task TestDeletePoolWorksForOpened()
        {
            var poolName = PoolUtils.CreatePoolLedgerConfig();
            var pool = await Pool.OpenPoolLedgerAsync(poolName, null);

            Assert.IsNotNull(pool);
            _openedPools.Add(pool);

            var ex = await Assert.ThrowsExceptionAsync<IndyException>(() =>
                Pool.DeletePoolLedgerConfigAsync(poolName)
            );

            Assert.AreEqual(ErrorCode.CommonInvalidState, ex.ErrorCode);
        }        
    }
}
