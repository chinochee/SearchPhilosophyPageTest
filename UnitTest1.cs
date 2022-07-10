using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SearchPhilosophyPageTest.Loggers;

namespace SearchPhilosophyPageTest;

public class Tests
{   
    private const int MaxStep = 16;
    
    private const string WikiLink = "https://ru.wikipedia.org";
    private const string SearchWordString = "философии";
    
    private readonly By _searchWord = By.XPath($"//p[contains(text(),'{SearchWordString}')]");
    private readonly By _randomArticle = By.XPath("//span[text()='Случайная статья']");
    private readonly By _pageTitle = By.XPath("//h1[@id='firstHeading']");
    private readonly By _firstUrlOnPage = By.XPath("//div/p[1]/a[1]");
    
    private IWebDriver _driver;
    private LoggerSystem _loggerSystem;
    
    [SetUp]
    public void Setup()
    {
        _loggerSystem = new LoggerSystem();
        _driver = new ChromeDriver();
        _driver.Navigate().GoToUrl(WikiLink);
        _driver.Manage().Window.Maximize();
    }

    [Test]
    public void RunTest()
    {
        GoToRandom();
        var firstTitle = GetFirstPageTitle();


        var step = 0;

        while (step < MaxStep)
        {
            step++;
            LogPage(step);
            GotToFirstUrl();
            var exist = Search();
            
            if (!exist) continue;
            LogFinalResult(true,firstTitle,step);
            return;
        }

        LogFinalResult(false,firstTitle,step);
    }

    private void GoToRandom()
    {
        var randomArticle = _driver.FindElement(_randomArticle);
        randomArticle.Click();
        Thread.Sleep(400);
    }
    
    private string GetFirstPageTitle()
    {
        return _driver.FindElement(_pageTitle).Text;
    }
    
    private void LogPage(int step)
    {
        var title = _driver.FindElement(_pageTitle);
        _loggerSystem.Write($"Шаг {step} - {title.Text} - {_driver.Url}");
    }

    private void GotToFirstUrl()
    {
        var firstNameArticle = _driver.FindElement(_firstUrlOnPage);
        firstNameArticle.Click();
        Thread.Sleep(400);
    }

    private bool Search()
    {
        var placeHolderContent = _driver.FindElements(_searchWord);
        Thread.Sleep(400);
        var exist = placeHolderContent.Count > 0;
        return exist;
    }

    private void LogFinalResult(bool exist, string firstTitle, int currentStep)
    {
        _loggerSystem.Write(
            exist
                ? $"Чтобы попасть с {firstTitle} на слово {SearchWordString} необходимо сделать {currentStep} переходов"
                : $"Начиная с  {firstTitle}, слово {SearchWordString} не найдено за {MaxStep} шагов");
    }
    
    [TearDown]
    public void TearDown()
    {
        _loggerSystem.Close();
        _driver.Quit();
    }
}