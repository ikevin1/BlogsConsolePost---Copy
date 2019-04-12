using System.Data.Entity;
using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;


namespace BlogsConsole.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("name=BlogContext") { }
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public void AddBlog(Blog blog)
        {
            bool duplicateName = false;

            foreach(Blog b in Blogs)
            {
                if(blog.Name == b.Name)
                {
                    duplicateName = true;
                }
            }

            if(!duplicateName)
            {
                this.Blogs.Add(blog);
                this.SaveChanges();
                logger.Info("Success - " + blog.Name + " blog added!");
            }
            else
            {
                //rejected
                logger.Info("Rejected - dupilicate blog name!");
            }
        }

        public void AddPost(Post post)
        {
            this.Posts.Add(post);
            this.SaveChanges();
            logger.Info("Success - " + post.Title + " post added!");
        }

       
    }
}
