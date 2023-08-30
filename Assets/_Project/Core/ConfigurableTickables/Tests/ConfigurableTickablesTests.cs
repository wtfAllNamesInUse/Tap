using Zenject;
using NUnit.Framework;
using TapTapTap.ConfigurableTickables;
using TapTapTap.DateTimeProvider;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class ConfigurableTickablesTests : ZenjectUnitTestFixture
{
    private EverySecondTickable everySecondTickable;
    private ConfigurableTickablesManager configurableTickablesManager;

    private DateTimeProviderModel dateTimeProviderModelCopy;

    [SetUp]
    public void CommonInstall()
    {
        var dateTimeProviderModel = DateTimeProviderModel.LoadModel();
        dateTimeProviderModelCopy = new DateTimeProviderModel(dateTimeProviderModel);
        dateTimeProviderModel.isEnabled = true;
        dateTimeProviderModel.deltaTime = 1.0f;
        DateTimeProviderModel.SaveModel(dateTimeProviderModel);

        DateTimeProviderInstaller.Install(Container);

        ConfigurableTickablesInstaller.Install(Container);
        configurableTickablesManager = Container.Resolve<ConfigurableTickablesManager>();
        everySecondTickable = Container.Resolve<EverySecondTickable>();
    }

    [TearDown]
    public void CleanUp()
    {
        DateTimeProviderModel.SaveModel(dateTimeProviderModelCopy);
    }

    [Test]
    public void TestTick()
    {
        everySecondTickable.Initialize();
        everySecondTickable.Tick += OnTick;

        configurableTickablesManager.Tick();
        LogAssert.Expect(LogType.Log, "OnTick Received");

        everySecondTickable.Tick -= OnTick;
        everySecondTickable.Dispose();

        DateTimeProviderModel.SaveModel(dateTimeProviderModelCopy);

        void OnTick()
        {
            Debug.Log("OnTick Received");
        }
    }
}