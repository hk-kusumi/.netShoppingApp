using ShoppingApplication.EfContext;
using ShoppingApplication.EfCore;
using System.Data;

namespace ShoppingApplication.Models
{
    public class DbHelper
    {
        private EF_DataContext _context; 
        public DbHelper(EF_DataContext context)
        {
            _context = context;
        }

        // Get All
        public List<ProductModel> GetProducts()
        {
            List<ProductModel> response = new List<ProductModel>();
           var dataList= _context.Products.ToList();
            dataList.ForEach(row => response.Add(new ProductModel()
            {
                brand = row.brand,
                id= row.id,
                name=row.name,
                price=row.price,
                size=row.size
            })) ;
            return response;
        }

        //Get {id}
        public ProductModel getProductById(int id)
        {
            var row= _context.Products.Where(o => o.id.Equals(id)).FirstOrDefault();
            return new ProductModel()
            {
                brand = row.brand,
                id = row.id,
                name = row.name,
                price = row.price,
                size = row.size
            };
        }

        
        //Serve Post/Put /Patch
        public void SaveOrder( OrderModel orderModel)
        {
            Order orderRow=new Order();
            if(orderRow.id > 0)
            {
                //put
                orderRow = _context.Orders.Where(o=> o.id.Equals(orderModel.id)).FirstOrDefault();
                if(orderRow != null)
                {
                    orderRow.address = orderModel.address;
                    orderRow.phone=orderModel.phone;
                }   
            }
            //post
            else
            {
                orderRow.phone = orderModel.phone;
                orderRow.address = orderModel.address;
                orderRow.name = orderModel.name;
                orderRow.Product = _context.Products.Where(p=> p.id.Equals(orderModel.product_id)).FirstOrDefault();
                _context.Orders.Add(orderRow);
            }
            _context.SaveChanges();
        }

        //delete
        public void DeleteOrder( int orderId )
        {
            var order= _context.Orders.Where(o=> o.id.Equals(orderId)).FirstOrDefault();
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }            
        }
    }
}
