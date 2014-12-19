using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POC.EF.ManyToMany.DAL.EF;
using POC.EF.ManyToMany.DAL.Models;

namespace POC.EF.ManyToMany.Tests
{
    /// <summary>
    /// Summary description for ManyToManyTests
    /// </summary>
    [TestClass]
    public class ManyToManyTests
    {
        internal static MovieDbContext dbContext = new MovieDbContext();
        public ManyToManyTests()
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        {
            DeleteAllRecords();
        }
        //
        // Use ClassCleanup to run code after all tests in a class have run
         [ClassCleanup()]
        public static void MyClassCleanup() 
         {
             DeleteAllRecords();
         }
        //
        // Use TestInitialize to run code before running each test 
         [TestInitialize()]
         public void MyTestInitialize() 
         {
             DeleteAllRecords();
         }
        //
        // Use TestCleanup to run code after each test has run
         [TestCleanup()]
         public void MyTestCleanup() 
         {
             DeleteAllRecords();
         }
        //
        #endregion

        [TestMethod]
        public void SimpleInsertTest()
        {
           //Prep
            Scenerio01();

            //Test
            var actors = dbContext.Actors.ToList();
            var films = dbContext.Films.ToList();

            Assert.IsTrue(actors.Count == 2, "Wrong no of Actors");
            Assert.IsTrue(films.Count == 2, "Wrong no of Films");


            //dbContext.Database.ExecuteSqlCommand("SELECT * FROM information_schema.tables");
            //var tables = dbContext.Database.SqlQuery<string>("SELECT TABLE_NAME FROM information_schema.tables").ToList<string>();
            //Assert.IsNotNull(tables, "tables cannot be null");
        }

        [TestMethod]
        public void ManyToManySimpleInsertTest()
        {
            Scenerio02();

            var actors = dbContext.Actors.ToList();
            var films = dbContext.Films.ToList();

            Assert.IsTrue(actors.Count == 2, "Wrong no of Actors");
            Assert.IsTrue(films.Count == 2, "Wrong no of Films");

            var count = dbContext.Database.SqlQuery<int>("SELECT count(1) FROM ACtor_Film_Map").ToList<int>();
            Assert.IsTrue(count[0] == 3, "Wrong no of Films");

            
        }

        [TestMethod]
        public void Scenerio03InsertTest()
        {
            Scenerio03();

            var actors = dbContext.Actors.ToList();
            var films = dbContext.Films.ToList();

            int count = GetRecCountFromMap();
            Assert.IsTrue(count == 5);


        }

        [TestMethod]
        public void ManyToManyInsertTest()
        {
            Scenerio02();

            //add actor a2 to film f1
            var actor = dbContext.Actors.Where(p => p.Name == "Actor02").FirstOrDefault();
            var film = dbContext.Films.Where(f => f.Name == "Film01").FirstOrDefault();

            Assert.IsNotNull(actor);
            Assert.IsNotNull(film);

            int count = GetRecCountFromMap();
            Assert.IsTrue(count == 3);

            film.Actors.Add(actor);

            dbContext.SaveChanges();


             count = GetRecCountFromMap();
            Assert.IsTrue(count == 4);


        }

        [TestMethod]
        public void SimpleDeleteTest()
        {
            Scenerio02();
            var film = dbContext.Films.Where(p => p.Actors.Count == 1).FirstOrDefault();
            Assert.IsNotNull(film);
            Assert.IsTrue(film.Actors.Count == 1);
            film.Actors.Remove(film.Actors.Take(1).First());
            dbContext.SaveChanges();

            int count = GetRecCountFromMap();
            Assert.IsTrue(count == 2);

            film = dbContext.Films.Where(p => p.Actors.Count == 2).FirstOrDefault();
            Assert.IsNotNull(film);
            Assert.IsTrue(film.Actors.Count == 2);
            film.Actors.Remove(film.Actors.Take(1).First());
            dbContext.SaveChanges();

            count = GetRecCountFromMap();
            Assert.IsTrue(count == 1);

        }


        private static void DeleteAllRecords()
        {
            var actors = dbContext.Actors.ToList();
            var films = dbContext.Films.ToList();

            if (actors != null)
            {
                actors.ForEach(x => dbContext.Actors.Remove(x));
            }
            if (films != null)
            {
                films.ForEach(x => dbContext.Films.Remove(x));
            }
            dbContext.SaveChanges();

        }

        private static void Scenerio01()
        {
            Actor a1 = new Actor() { Name = "Actor01" };
            Actor a2 = new Actor() { Name = "Actor02" };

            Film f1 = new Film() { Name = "Film01" };
            Film f2 = new Film() { Name = "Film02" };

            dbContext.Actors.Add(a1);
            dbContext.Actors.Add(a2);
            dbContext.Films.Add(f1);
            dbContext.Films.Add(f2);
            dbContext.SaveChanges();
        }

        private static void Scenerio02()
        {
            /*
             * a1 -> f1, f2
             * f2 -> a2
             */
            Actor a1 = new Actor() { Name = "Actor01" };
            Actor a2 = new Actor() { Name = "Actor02" };

            Film f1 = new Film() { Name = "Film01" };
            Film f2 = new Film() { Name = "Film02" };


            a1.Films.Add(f1);
            a1.Films.Add(f2);

            f2.Actors.Add(a2);

            dbContext.Actors.Add(a1);
            dbContext.Actors.Add(a2);
            dbContext.Films.Add(f1);
            dbContext.Films.Add(f2);
            dbContext.SaveChanges();
        }

        private static void Scenerio03()
        {
            /*
             * a1 -> f1, f2
             * a2 -> f2
             * a3 -> f2, f3
             */
            Actor a1 = new Actor() { Name = "Actor01" };
            Actor a2 = new Actor() { Name = "Actor02" };
            Actor a3 = new Actor() { Name = "Actor03" };

            Film f1 = new Film() { Name = "Film01" };
            Film f2 = new Film() { Name = "Film02" };
            Film f3 = new Film() { Name = "Film03" };


            a1.Films.Add(f1);
            a1.Films.Add(f2);

            a2.Films.Add(f2);

            a3.Films.Add(f2);
            a3.Films.Add(f3);

            dbContext.Actors.Add(a1);
            dbContext.Actors.Add(a2);
            dbContext.Actors.Add(a3);
            dbContext.Films.Add(f1);
            dbContext.Films.Add(f2);
            dbContext.Films.Add(f3);
            dbContext.SaveChanges();
        }

        private static int GetRecCountFromMap()
        {
            return dbContext.Database.SqlQuery<int>("SELECT count(1) FROM ACtor_Film_Map").FirstOrDefault();
        }
    }
}
