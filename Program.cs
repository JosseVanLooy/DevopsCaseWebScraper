using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using File = System.IO.File;

namespace Devops_project
{
    class Program
    {
        static void Main(string[] args)
        {

 
            string choice;

         
            Console.WriteLine("Which website do you want to scrape:");
            Console.WriteLine("Youtube => press(y)");
            Console.WriteLine("Ictjob => press(i)");
            Console.WriteLine("CoinMarketCap => press(c)");
            Console.WriteLine("Pressing another key than y,i or c exits the program!");
            Console.WriteLine();


            System.Text.StringBuilder csvBuilder = new StringBuilder();

            choice = Console.ReadLine();

            if (choice == "y")
            {
                string FilePath = "C:\\schoolwerk\\devops\\CaseStudyYT.csv";

                Console.WriteLine("Fill in your searchterm: ");
                string searchTerm = Console.ReadLine();

                IWebDriver driver = new ChromeDriver(@"C:\schoolwerk\AI project\AI\Scraping");
                driver.Navigate().GoToUrl("https://www.youtube.com/");
                Thread.Sleep(1500);
                driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]/div[6]/div[1]/ytd-button-renderer[1]/yt-button-shape/button/yt-touch-feedback-shape/div/div[2]")).Click();
                

                var searchInput = driver.FindElement(By.XPath("/html/body/ytd-app/div/div/ytd-masthead/div[3]/div[2]/ytd-searchbox/form/div[1]/div[1]/input"));
                searchInput.Click();
                searchInput.SendKeys(searchTerm);
                searchInput.Submit();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//*[@id=\"container\"]/ytd-toggle-button-renderer/yt-button-shape/button/yt-touch-feedback-shape/div/div[2]")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div[2]/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/iron-collapse/div/ytd-search-filter-group-renderer[5]/ytd-search-filter-renderer[2]/a/div/yt-formatted-string")).Click();
                Thread.Sleep(3000);

                List<string> titleList = new List<string>();
                List<string> viewsList = new List<string>();
                List<string> uploaderList = new List<string>();
                List<string> linkList = new List<string>();

                var titles = driver.FindElements(By.XPath("//*[@id=\"video-title\"]/yt-formatted-string"));

                foreach (var title in titles)
                {
                    titleList.Add(title.Text);
                }

                var views = driver.FindElements(By.XPath("//*[@id=\"metadata-line\"]/span[1]"));
                foreach (var view in views)
                {
                    viewsList.Add(view.Text);
                }
                
                var uploaders = driver.FindElements(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div[2]/div/ytd-section-list-renderer/div[2]/ytd-item-section-renderer/div[3]/ytd-video-renderer/div[1]/div/div[2]/ytd-channel-name/div/div/yt-formatted-string/a"));
                foreach (var uploader in uploaders)
                {
                    uploaderList.Add(uploader.Text);
                }
                var links = driver.FindElements(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div[2]/div/ytd-section-list-renderer/div[2]/ytd-item-section-renderer/div[3]/ytd-video-renderer/div[1]/ytd-thumbnail/a"));
                foreach (var link in links)
                {
                    string url = link.GetAttribute("href");
                    linkList.Add(url);
                }
                Console.Clear();
                Thread.Sleep(5000);
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Title: " + titleList[i]);
                    Console.WriteLine("Views: " + viewsList[i]);
                    Console.WriteLine("Channel: " + uploaderList[i]);
                    Console.WriteLine("Url: " + linkList[i]);
                    Console.WriteLine();

                    csvBuilder.Append("\"" + "SearchTerm: " + searchTerm + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + "Title: " + titleList[i] + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + "Views: " + viewsList[i] + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + "Channel: " + uploaderList[i] + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + "Url: " + linkList[i] + "\"");
                    csvBuilder.Append("\n");
                }
                File.AppendAllText(FilePath, csvBuilder.ToString());

            }
            else if (choice == "i")
            {

                string FilePath = "C:\\schoolwerk\\devops\\CaseStudyJobs.csv";

                Console.WriteLine("From which jobs do you want information: ");
                string searchIctjob = Console.ReadLine();
                IWebDriver driver = new ChromeDriver(@"C:\schoolwerk\AI project\AI\Scraping");
                driver.Navigate().GoToUrl("https://www.ictjob.be/");
                var searchInput = driver.FindElement(By.XPath("//*[@id=\"keywords-input\"]"));
                searchInput.Click();
                searchInput.SendKeys(searchIctjob);   
                searchInput.Submit();
                Thread.Sleep(15000);
                driver.FindElement(By.XPath("/html/body/section/div[1]/div/div[2]/div/div/form/div[2]/div/div/div[2]/section/div/div[1]/div[2]/div/div[2]/span[2]/a")).Click();
                Thread.Sleep(2000);

                List<string> jobList = new List<string>();
                List<string> companyList = new List<string>();
                List<string> locationList = new List<string>();
                List<string> keywordList = new List<string>();
                List<string> linkList = new List<string>();

                var titles = driver.FindElements(By.CssSelector("h2.job-title"));

                foreach (var title in titles)
                {
                    jobList.Add(title.Text);
                }

                var companies = driver.FindElements(By.CssSelector("span.job-company"));

                foreach (var company in companies)
                {
                    companyList.Add(company.Text);
                }

                var locations = driver.FindElements(By.CssSelector("span.job-location"));

                foreach (var location in locations)
                {
                    locationList.Add(location.Text);
                }

                
                var keywords = driver.FindElements(By.CssSelector("span.job-keywords"));

                foreach (var keyword in keywords)
                {
                    keywordList.Add(keyword.Text);
                }

                var links = driver.FindElements(By.ClassName("search-item-link"));


                foreach (var link in links)
                {
                    string url = link.GetAttribute("href");
                    linkList.Add(url);
                }

                Console.Clear();
                Thread.Sleep(5000);

                for (int i = 0; i < 5; i++)
                {
                    

                    Console.WriteLine("Job name: " + jobList[i]);
                    Console.WriteLine("Company name: " + companyList[i]);
                    Console.WriteLine("Location: " + locationList[i]);
                    Console.WriteLine("Keyword: " + keywordList[i]);
                    Console.WriteLine("link: " + linkList[i]);
                    Console.WriteLine();

                    csvBuilder.Append("\"" + "SearchTerm: " + searchIctjob + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + "Job name: " + jobList[i] + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + "Company name: " + companyList[i] + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + "Location: " + locationList[i] + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + "Keyword: " + keywordList[i] + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + "link: " + linkList[i] + "\"");
                    csvBuilder.Append("\n");
                }

                File.AppendAllText(FilePath, csvBuilder.ToString());

            }
            else if (choice == "c")
            {
                
                string FilePath = "C:\\schoolwerk\\devops\\CaseStudyMarket.csv";

                Console.WriteLine("Which coin do you want information from: ");
                string searchCoin = Console.ReadLine();
                

              
                IWebDriver driver = new ChromeDriver(@"C:\schoolwerk\AI project\AI\Scraping");
                driver.Navigate().GoToUrl("https://coinmarketcap.com/");
               

          
                driver.Manage().Window.Maximize();
                Thread.Sleep(5000);

                var nextButton = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[4]/button"));
                nextButton.Click();
                Thread.Sleep(5000);

                var gotitButton = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[4]/button"));
                gotitButton.Click();

              
                var searchInputCoin = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[1]/div[1]/div[1]/div/div[2]/div[4]/div"));
                searchInputCoin.Click();
                Thread.Sleep(5000);

                var searchInputCoin2 = driver.FindElement(By.XPath("//*[@id=\"tippy-1\"]/div/div/div/div/div[1]/div[1]/div[1]/input"));
                
                
              
                searchInputCoin2.Click();
                searchInputCoin2.SendKeys(searchCoin);
                Thread.Sleep(8000);

                
                var searchInputCoin3 = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[1]/div[1]/div/div[2]/div[5]/div/div/div/div/div[2]/div[1]/a[1]/div"));
                searchInputCoin3.Click();

                Thread.Sleep(8000);
                Console.Clear();
               


                var coinName = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div[1]/div[2]/div/div[1]/div[1]/h2/span/span")).Text;
                
                var pricePerCoin = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div[1]/div[2]/div/div[2]/div[1]/div/span")).Text;
                
                var marketCap = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[1]/div[2]/div")).Text;
                
                var coinCirculating = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div[1]/div[2]/div/div[3]/div[1]/div[4]/div[2]/div[1]")).Text;

                var coinVolume = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div[1]/div[2]/div/div[3]/div[1]/div[3]/div[1]/div[2]/div")).Text;

                
                Console.WriteLine("Name Coin: " + coinName);
                Console.WriteLine("Cost of 1 coin: " + pricePerCoin);
                Console.WriteLine("Market cap: " + marketCap);
                Console.WriteLine("How much circulating: " + coinCirculating);
                Console.WriteLine("Volume amount: " + coinVolume);
                Console.WriteLine();


                csvBuilder.Append("\"" + "SearchTerm: " + searchCoin + "\"");
                csvBuilder.Append(";");
                csvBuilder.Append("\"" + "Name Coin: " + coinName + "\"");
                csvBuilder.Append(";");
                csvBuilder.Append("\"" + "Cost of 1 coin: " + pricePerCoin + "\"");
                csvBuilder.Append(";");
                csvBuilder.Append("\"" + "Market cap: " + marketCap + "\"");
                csvBuilder.Append(";");
                csvBuilder.Append("\"" + "How much circulating: " + coinCirculating + "\"");
                csvBuilder.Append(";");
                csvBuilder.Append("\"" + "Volume amount: " + coinVolume + "\"");
                csvBuilder.Append("\n");

                File.AppendAllText(FilePath, csvBuilder.ToString());


            }
            
        }
    }
}