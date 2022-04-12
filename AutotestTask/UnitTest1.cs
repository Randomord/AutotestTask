using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System;
using System.Threading;
using NLog;


namespace AutotestTask
{
            
    public class Tests
    {
        // Организация логгирования 
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // Используемые URL
        IWebDriver driver;
        string[] Url_all = new string[] {"https://habr.com/", "https://habr.com/ru/flows/develop/", "https://habr.com/ru/flows/admin/",
           "https://habr.com/ru/flows/design/", "https://habr.com/ru/flows/management/",
           "https://habr.com/ru/flows/marketing/", "https://habr.com/ru/flows/popsci/", "https://habr.com/ru/search/"};
        

        [SetUp]
        public void Setup()
        {
                 
        }


        [Test]
        public void Test_Chrome()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://habr.com/"); // Переход на главную сраницу Хабр
            // driver.Manage().Window.Maximize();  // Максимальный размер окна браузера
            logger.Info("Start test in chrome browser");
            

            string url, header, article_title;


            // Пролистывание всех пунктов меню
            for (int i = 1; i <= 6; i++)
            {
                url = Url_all[i];
                driver.Navigate().GoToUrl(url);
                header = driver.Title;
                Console.WriteLine("Header: " + header);
                logger.Info("Header: " + header);
            }


            // Возвращение на главную страницу
            url = Url_all[0];
            driver.Navigate().GoToUrl(url);
            header = driver.Title;
            Console.WriteLine("Header: " + header);
            logger.Info("Header: " + header);
            Thread.Sleep(5000); 
            

            // Вывод заголовков всех статей, находящихся на странице
            IList<IWebElement> articles_list_01 = driver.FindElements(By.XPath("//h2[@class='tm-article-snippet__title tm-article-snippet__title_h2']"));     
            foreach (IWebElement title in articles_list_01)
            {
                Console.WriteLine("Article title: " + title.Text);
                logger.Info("Article title: " + title.Text);
            }
            

            // Поиск по сайту 
            url = Url_all[7];
            driver.Navigate().GoToUrl(url);
            header = driver.Title;
            Console.WriteLine("Header: " + header);
            logger.Info("Header: " + header);
            IWebElement input_search = driver.FindElement(By.XPath("//input[@class = 'tm-input-text-decorated__input']"));
            input_search.SendKeys("selenium");
            input_search.SendKeys(Keys.Enter);
            Thread.Sleep(5000);


            // Сортировка выдачи поиска по времени 
            driver.Navigate().Refresh();
            IWebElement button1 = driver.FindElement(By.XPath("//button[@class='tm-navigation-dropdown__button']"));
            button1.Click();
            Thread.Sleep(5000); 
            IWebElement button2 = driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[2]/main/div/div/div/div/div/div[2]/div[2]/ul/li[2]/button"));
            button2.Click();
            Thread.Sleep(2000);


            // Вывод: хабов первой статьи, заголовка и текста кнопки третьей статьи. Открытие третьей статьи, нажатием на кнопку.
            IList<IWebElement> articles_list_02 = driver.FindElements(By.XPath("//article[@class = 'tm-articles-list__item']"));
            int counter = 0;
            foreach (IWebElement article in articles_list_02)
            {
                if (counter == 0)
                {
                    IList<IWebElement> hubs_list = article.FindElements(By.ClassName("tm-article-snippet__hubs"));
                    foreach (IWebElement hubs in hubs_list)
                    {                  
                        Console.WriteLine("First article Hubs: " + hubs.Text);
                        logger.Info("First article Hubs: " + hubs.Text);
                    }                    
                }
                if (counter == 2)
                {
                    IWebElement title = article.FindElement(By.ClassName("tm-article-snippet__title-link"));
                    IWebElement button = article.FindElement(By.ClassName("tm-article-snippet__readmore"));
                    Console.WriteLine("Title of the third article: " + title.Text);
                    Console.WriteLine("Button text: " + button.Text);
                    logger.Info("Title of the third article: " + title.Text);
                    logger.Info("Button text: " + button.Text);
                    Thread.Sleep(5000);
                    button.Click();
                    Thread.Sleep(5000);
                    break;
                }
                else
                {
                    counter += 1;    
                }               
            }


            // Вывод заголовка и количества комментариев открытой статьи 
            article_title = driver.Title;
            Console.WriteLine("Article title: " + article_title);
            logger.Info("Article title: " + article_title);                
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            IWebElement comments_counter = driver.FindElement(By.ClassName("tm-article-comments-counter-link__value"));
            Console.WriteLine("Comments: " + comments_counter.Text);
            logger.Info("Comments: " + comments_counter.Text);
            Thread.Sleep(5000);
            Assert.Pass();
            
        }

        [Test]
        public void Test_FireFox()
        {
            driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            driver.Navigate().GoToUrl("https://habr.com/");  // Переход на главную сраницу Хабр
            // driver.Manage().Window.Maximize();  // Максимальный размер окна браузера
            logger.Info("Start test in firefox browser");


            string url, header, article_title;


            // Пролистывание всех пунктов меню
            for (int i = 1; i <= 6; i++)
            {
                url = Url_all[i];
                driver.Navigate().GoToUrl(url);
                header = driver.Title;
                Console.WriteLine("Header: " + header);
                logger.Info("Header: " + header);
            }


            // Возвращение на главную страницу
            url = Url_all[0];
            driver.Navigate().GoToUrl(url);
            header = driver.Title;
            Console.WriteLine("Header: " + header);
            logger.Info("Header: " + header);
            Thread.Sleep(5000);


            // Вывод заголовков всех статей, находящихся на странице
            IList<IWebElement> articles_list_01 = driver.FindElements(By.XPath("//h2[@class='tm-article-snippet__title tm-article-snippet__title_h2']"));
            foreach (IWebElement title in articles_list_01)
            {
                Console.WriteLine("Article title: " + title.Text);
                logger.Info("Article title: " + title.Text);
            }


            // Поиск по сайту 
            url = Url_all[7];
            driver.Navigate().GoToUrl(url);
            header = driver.Title;
            Console.WriteLine("Header: " + header);
            logger.Info("Header: " + header);
            IWebElement input_search = driver.FindElement(By.XPath("//input[@class = 'tm-input-text-decorated__input']"));
            input_search.SendKeys("selenium");
            input_search.SendKeys(Keys.Enter);
            Thread.Sleep(5000);


            // Сортировка выдачи поиска по времени 
            driver.Navigate().Refresh();
            IWebElement button1 = driver.FindElement(By.XPath("//button[@class='tm-navigation-dropdown__button']"));
            button1.Click();
            Thread.Sleep(5000);
            IWebElement button2 = driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[2]/main/div/div/div/div/div/div[2]/div[2]/ul/li[2]/button"));
            button2.Click();
            Thread.Sleep(2000);


            // Вывод: хабов первой статьи, заголовка и текста кнопки третьей статьи. Открытие третьей статьи, нажатием на кнопку.
            IList<IWebElement> articles_list_02 = driver.FindElements(By.XPath("//article[@class = 'tm-articles-list__item']"));
            int counter = 0;
            foreach (IWebElement article in articles_list_02)
            {
                if (counter == 0)
                {
                    IList<IWebElement> hubs_list = article.FindElements(By.ClassName("tm-article-snippet__hubs"));
                    foreach (IWebElement hubs in hubs_list)
                    {
                        Console.WriteLine("First article Hubs: " + hubs.Text);
                        logger.Info("First article Hubs: " + hubs.Text);
                    }
                }
                if (counter == 2)
                {
                    IWebElement title = article.FindElement(By.ClassName("tm-article-snippet__title-link"));
                    IWebElement button = article.FindElement(By.ClassName("tm-article-snippet__readmore"));
                    Console.WriteLine("Title of the third article: " + title.Text);
                    Console.WriteLine("Button text: " + button.Text);
                    logger.Info("Title of the third article: " + title.Text);
                    logger.Info("Button text: " + button.Text);
                    Thread.Sleep(5000);
                    button.Click();
                    Thread.Sleep(5000);
                    break;
                }
                else
                {
                    counter += 1;
                }
            }


            // Вывод заголовка и количества комментариев открытой статьи 
            article_title = driver.Title;
            Console.WriteLine("Article title: " + article_title);
            logger.Info("Article title: " + article_title);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            IWebElement comments_counter = driver.FindElement(By.ClassName("tm-article-comments-counter-link__value"));
            Console.WriteLine("Comments: " + comments_counter.Text);
            logger.Info("Comments: " + comments_counter.Text);
            Thread.Sleep(5000);
            Assert.Pass();
        }


        [TearDown]
        public void TearDown()
        {

            driver.Close();
            driver.Quit();
        }


    }
}