using System;
using Zenject;
using NUnit.Framework;
using TapTapTap.DateTimeProvider;
using UnityEngine;

[TestFixture]
public class DateTimeProviderTests : ZenjectUnitTestFixture
{
    private IDateTimeProvider dateTimeProvider;

    private DateTimeProviderModel dateTimeProviderModelCopy;

    [SetUp]
    public void CommonInstall()
    {
        var dateTimeProviderModel = DateTimeProviderModel.LoadModel();
        dateTimeProviderModelCopy = new DateTimeProviderModel(dateTimeProviderModel);
        dateTimeProviderModel.isEnabled = true;
        dateTimeProviderModel.deltaTime = 1.0f;
        dateTimeProviderModel.minutesOffset = 60;
        DateTimeProviderModel.SaveModel(dateTimeProviderModel);

        DateTimeProviderInstaller.Install(Container);
        dateTimeProvider = Container.Resolve<IDateTimeProvider>();
    }

    [TearDown]
    public void CleanUp()
    {
        DateTimeProviderModel.SaveModel(dateTimeProviderModelCopy);
    }

    [Test]
    public void TestDateTimeProvider()
    {
        Assert.IsTrue(Math.Abs(dateTimeProvider.DeltaTime - (Time.deltaTime + 1.0f)) < 0.005f);
        Assert.IsTrue(Math.Abs((dateTimeProvider.CurrentDateTime - DateTime.Now.AddMinutes(60)).TotalSeconds) < 1);
        Assert.IsTrue(Math.Abs((dateTimeProvider.CurrentUtcDateTime - DateTime.UtcNow.AddMinutes(60)).TotalSeconds) < 1);
    }
}