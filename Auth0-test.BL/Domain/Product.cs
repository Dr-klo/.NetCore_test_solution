
using System.Data;

namespace Auth0_test_solution.BL.Domain
{
    public class Product
    {
        public struct Errors
        {
            public const string InvalidArticle = "Product has invalid article.";
            public const string CountMoreThanExists = "Products exists less than requested.";
        }
        public Product(string name, int article, int count, int price)
        {
            this.Name = name;
            this.Article = article;
            this.Count = count;
            this.Price = price;
        }
        public string Name { get; }
        public int Article { get; }
        public int Count { get; set; }
        public int Price { get; private set; }

        public void Update(int count, int price)
        {
            this.Count = count;
            this.Price = price;    
        }
    }
}    