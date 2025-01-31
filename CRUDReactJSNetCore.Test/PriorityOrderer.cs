using Xunit.Abstractions;
using Xunit.Sdk;

namespace CRUDReactJSNetCore.Test
{
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(testCase =>
            {
                var priorityAttribute = testCase.TestMethod.Method.GetCustomAttributes(typeof(TestPriorityAttribute)).FirstOrDefault();

                return priorityAttribute == null ? int.MaxValue : ((TestPriorityAttribute)priorityAttribute).Priority;

            });
        }
    }
}
