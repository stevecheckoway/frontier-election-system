// -----------------------------------------------------------------------------
// <copyright file="ObjectMD5Tests.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ObjectMD5Tests test class.
// </summary>
// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>  
// -----------------------------------------------------------------------------

namespace UtilityTests
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    using Sequoia.Utilities;

    #endregion

    /// <summary>
    ///	    ObjectMD5Tests is a test fixture for running tests 
    ///     against Encryption
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/19/2009 1:48:23 PM" version="1.0.8.0201">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class ObjectMD5Tests
    {
        #region Fixture Setup
        /// <summary>
        ///     This method runs once for the entire test fixture. Place any 
        ///     logic that needs to happen before this test fixture is run 
        ///     in this method.
        /// </summary>
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
        }

        #endregion

        #region Fixture Teardown
        /// <summary>
        ///     This method runs once for the entire test fixture. Place any 
        ///     logic that needs to happen when this test fixture is unloaded.
        /// </summary>
        [TestFixtureTearDown]
        public void FixtureTeardown()
        {
        }
        #endregion

        #region Test Setup
        /// <summary>
        ///     This method runs once for every test in the entire test fixture. 
        ///     Place any logic that needs to happen before every test is loaded 
        ///     in this method.
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
        }
        #endregion

        #region Test Teardown
        /// <summary>
        ///     This method runs once for every test in the entire test fixture. 
        ///     Place any logic that needs to happen when every test is unloaded 
        ///     in this method.
        /// </summary>
        [TearDown]
        public void TestTeardown()
        {
        }
        #endregion

        #region Tests

        /// <summary>
        ///     Tests <see cref="ObjectMD5.Compare"/> method using 
        ///     simple instances.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
        ///     Member Created
        /// </revision>
        [Test]
        public void CompareSimpleTest()
        {
            EngineType engine1 = new EngineType(4, 408.5),
                       engine2 = new EngineType(6, 456.2);

            Car car1_0 = new Car("Car1", engine1, 5),
                car2_0 = new Car("Car2", engine2, 4),
                car1_1 = new Car("Car1", engine1, 5),
                car3_0 = new Car("Car3", engine2, 5);

            // compare different instances, same values
            Assert.IsTrue(ObjectMD5.Compare(car1_0, car1_1));

            // compare different instances, different values
            Assert.IsFalse(ObjectMD5.Compare(car1_0, car2_0));

            // compare same instances
            Assert.IsTrue(ObjectMD5.Compare(car2_0.Engine, car3_0.Engine));
        }

        /// <summary>
        ///     Tests <see cref="ObjectMD5.Compare"/> method using collections 
        ///     of objects.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
        ///     Member Created
        /// </revision>
        [Test]
        public void CompareAdvancedTest()
        {
            EngineType engine1 = new EngineType(4, 408.5),
                       engine2 = new EngineType(6, 456.2);

            List<Car> cars1 = new List<Car>(),
                      cars2 = new List<Car>();
            
            cars1.Add(new Car("Car1", engine1, 5));
            cars1.Add(new Car("Car2", engine2, 4));
            
            cars2.Add(new Car("Car1", engine1, 5));
            cars2.Add(new Car("Car2", engine2, 4));

            // same values, same order, different instances
            Assert.IsTrue(ObjectMD5.Compare(cars1, cars2));

            Car car1 = cars2[0];
            cars2[0] = cars2[1];
            cars2[1] = car1;

            // same values, different order, different instances
            Assert.IsFalse(ObjectMD5.Compare(cars1, cars2));

            cars2.Add(new Car("Car3", engine1, 5));

            // different values
            Assert.IsFalse(ObjectMD5.Compare(cars1, cars2));
        }

        #endregion
    }

    /// <summary>
    ///     Engine type class for testing
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
    ///     Member Created
    /// </revision>
    [Serializable]
    public class EngineType
    {
        #region Fields

        /// <summary>
        ///     cylinder displacement
        /// </summary>
        private double cylDisplacement;

        /// <summary>
        ///     number of cylinders
        /// </summary>
        private int cylinders;

        #endregion
        
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="EngineType"/> class.
        /// </summary>
        /// <param name="cylinders">The cylinders.</param>
        /// <param name="cylDisplacement">The cyl displacement.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public EngineType(int cylinders, double cylDisplacement)
        {
            this.cylinders = cylinders;
            this.cylDisplacement = cylDisplacement;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the displacement.
        /// </summary>
        /// <value>The displacement.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public double Displacement
        {
            get
            {
                return this.cylDisplacement * this.cylinders;
            }
        }

        /// <summary>
        ///     Gets the cylinders.
        /// </summary>
        /// <value>The cylinders.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public int Cylinders
        {
            get
            {
                return this.cylinders;
            }
        }

        #endregion
    }

    /// <summary>
    ///     Car class for testing
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
    /// Member Created</revision>
    [Serializable]
    public class Car
    {
        #region Fields

        /// <summary>
        ///     engine type
        /// </summary>
        private EngineType engine;

        /// <summary>
        ///     the name of the car
        /// </summary>
        private string name;

        /// <summary>
        ///     number of seats
        /// </summary>
        private int seats;

        #endregion
        
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        /// <param name="name">The car name.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="seats">The seats.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public Car(string name, EngineType engine, int seats)
        {
            this.name = name;
            this.engine = engine;
            this.seats = seats;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the engine.
        /// </summary>
        /// <value>The engine.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public EngineType Engine
        {
            get
            {
                return this.engine;
            }
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name of the car.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        ///     Gets the seats.
        /// </summary>
        /// <value>The seats.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public int Seats
        {
            get
            {
                return this.seats;
            }
        }
        
        #endregion
    }
}
