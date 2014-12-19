using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POC.EF.ManyToMany.DAL.EF;

namespace POC.EF.ManyToMany.Tests
{
    /// <summary>
    /// Summary description for DbInitializerTests
    /// </summary>
    [TestClass]
    public class DbInitializerTests
    {
        //internal static MovieDbContext dbContext = new MovieDbContext("TestMovieDBConStr");

        public DbInitializerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void DBInitializeTest()
        {
            //Initialisze db
            //if(!dbContext.Database.Exists())
            //{
            //    dbContext.Database.Initialize(false);
            //}
            MovieDbContext context = new MovieDbContext();
            var actors = context.Actors.ToList();
            var tables = context.Database.SqlQuery<string>("SELECT TABLE_NAME FROM information_schema.tables").ToList<string>();
            Assert.IsNotNull(tables, "tables cannot be null");
        }
    }
}
