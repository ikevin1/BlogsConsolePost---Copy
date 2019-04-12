using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            string choice = "";
            do
            {
                try
                {
                    DisplayMenu();
                    var dbContext = new BloggingContext();
                    
                    logger.Info("User choice: {Choice}", choice);

                    if (choice == "3") //add post
                    {                       
                        Console.Write("Enter a Blog name for a new Post: ");
                        var Blogname = Console.ReadLine();

                        Blog MyBlog = dbContext.Blogs.Where(id => id.Name == Blogname).FirstOrDefault();

                        Console.WriteLine("Enter Title: ");
                        var myTitle = Console.ReadLine();
                        Console.WriteLine("\nEnter Content: ");
                        var myContent = Console.ReadLine();

                        //add post
                        Post post = new Post { Title = myTitle, Content = myContent, BlogId = MyBlog.BlogId };
                        dbContext.AddPost(post);
                    }
                  
                    else if (choice == "2") //add blog
                    {
                        
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };
                        dbContext.AddBlog(blog);
                    }
                    else if (choice == "1") //display all blogs
                    {
                        Console.WriteLine("All blogs in the database:");

                        var query = dbContext.Blogs.OrderBy(b => b.Name);
                        foreach (var item in query)
                        {
                            Console.WriteLine("BlogID: {0}\nBlogName: {1}", item.BlogId, item.Name);
                        }
                    }
                    
                }
                
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadLine();

                    logger.Error(ex.Message);
                    Console.ReadLine();
                }
            } while (choice == "1" || choice == "2" || choice == "3");
            Console.WriteLine("Press enter to quit");
            string x = Console.ReadLine();

            logger.Info("Program ended");
            void DisplayMenu()
            {
                Console.WriteLine("1) Display All Blogs");
                Console.WriteLine("2) Add Blogs");
                Console.WriteLine("3) Create Post");                
                Console.WriteLine("Enter to quit");
                // input selection
                choice = Console.ReadLine();
            }
        }
    }
}
