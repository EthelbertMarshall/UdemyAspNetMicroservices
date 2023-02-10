using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        
        private readonly IConfiguration _configuration;


        public DiscountRepository(IConfiguration configuration)
        {            
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var con = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

           // con.Open();

            var result = await con.QueryFirstOrDefaultAsync<Coupon>
                ("Select * From Coupon Where ProductName = @ProductName", new { ProductName = productName });

            if (result == null)
            {
                return new Coupon { ProductName= "No Disocunt", Amount=0, Description="No Discount Desc" };
            }
            else
                return result;

        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var con = new NpgsqlConnection
                            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var result = await con.ExecuteAsync
                ("Insert into Coupon (ProductName, Description, Amount) values (@ProductName,@Description,@Amount) ",
                new Coupon { ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Description}
                );

            if (result == 0)
                return false;
            
            else 
                return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var con = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var result = await con.ExecuteAsync
                ("Update Coupon Set ProductName =@ProductName , Amount =@Amount, Description = @Description Where Id=@Id",
                 new Coupon { Id=coupon.Id, Amount=coupon.Amount, Description=coupon.Description, ProductName=coupon.ProductName}
                );

            if (result == 0)
                return false;
            else
                return true;
                    
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var con = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var result = await con.ExecuteAsync("Delete from Coupon where ProductName = @ProductName", new { ProductName = productName });

            if (result == 0)
                return false;
            else
                return true;
        }

    }
}
