﻿#region License

// Copyright (c) 2011 Effort Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System.Linq;
using Effort.Test.Data;
using Effort.Test.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Effort.Test.Data.Northwind;

namespace Effort.Test
{
    [TestClass]
    public class BasicFixture
    {
        private QueryTestRuntime<NorthwindObjectContext> runtime;

        [TestInitialize]
        public void Initialize()
        {
            this.runtime = new QueryTestRuntime<NorthwindObjectContext>(NorthwindObjectContext.DefaultConnectionString);
        
        }

        [TestMethod]
        public void CheckDataMatch()
        {
            Assert.IsTrue(this.runtime.Execute(c => c.Categories.AsQueryable()));
            Assert.IsTrue(this.runtime.Execute(c => c.CustomerDemographics.AsQueryable()));
            Assert.IsTrue(this.runtime.Execute(c => c.Customers.AsQueryable()));
            Assert.IsTrue(this.runtime.Execute(c => c.Employees.AsQueryable()));
            Assert.IsTrue(this.runtime.Execute(c => c.OrderDetails.AsQueryable()));
            Assert.IsTrue(this.runtime.Execute(c => c.Orders.AsQueryable()));
            Assert.IsTrue(this.runtime.Execute(c => c.Products.AsQueryable()));
            Assert.IsTrue(this.runtime.Execute(c => c.Regions.AsQueryable()));
            Assert.IsTrue(this.runtime.Execute(c => c.Shippers.AsQueryable()));
            Assert.IsTrue(this.runtime.Execute(c => c.Suppliers.AsQueryable()));
            Assert.IsTrue(this.runtime.Execute(c => c.Territories.AsQueryable()));
        }


        [TestMethod]
        public void FullTableScan()
        {
            bool result = this.runtime.Execute(

                context =>
                    from emp in context.Employees
                    select emp

            );

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FindById()
        {
            bool result = this.runtime.Execute(

                context =>
                    from emp in context.Employees
                    where emp.EmployeeID == 1
                    select emp
                    
            );

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OrderBy()
        {
            bool result = this.runtime.Execute(

                context =>
                    from emp in context.Employees
                    orderby emp.LastName
                    select emp


                ,true //Strict order
                    
            );

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Projection()
        {
            bool result = this.runtime.Execute(

                context =>
                    from emp in context.Employees
                    select new { firstName = emp.FirstName, lastName = emp.LastName }

                );

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void QueryParameter()
        {
            int id = 1;

            bool result = this.runtime.Execute(

                context =>
                    from emp in context.Employees
                    where emp.EmployeeID == id
                    select emp

            );

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void QueryParameterChange()
        {

            for (int id = 1; id < 3; id++)
            {
                bool result = this.runtime.Execute(

                      context =>
                          from emp in context.Employees
                          where emp.EmployeeID == id
                          select emp

                  );

                Assert.IsTrue(result);
            }


        }



        [TestMethod]
        public void UnionAll()
        {

            bool result = this.runtime.Execute(

                context =>
                    context.Employees.Concat(context.Employees)
            );

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UnionAll2()
        {
            bool result = this.runtime.Execute(

                context =>
                    context.Employees.Where(e => e.EmployeeID < 5)
                    
                    .Concat(
                    
                    context.Employees.Where(e => e.EmployeeID > 10))
            );

            Assert.IsTrue(result);
        }



    }
}
