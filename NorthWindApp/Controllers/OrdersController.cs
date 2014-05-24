using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NorthWindApp.Models;

// Test  [added at github.com]
// Test
// added on local pc
// one more on github.com
namespace NorthWindApp.Controllers
{

    // ViewModels  (DTOs)

    public class FlatVM
    {
        // orh - order header
        public int orderid { get; set; }
        public string shipcity { get; set; }
        public string companynamea { get; set; }
        public int numoflineitems { get; set; }
        // ord
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        // prd
        public string ProductName { get; set; }
    }

    public class OrhVM
    {
        public int orderid { get; set; }
        public string shipcity { get; set; }
        public string companynamea { get; set; }
        public int numoflineitems { get; set; }
        public ICollection<OrdVM> ord { get; set; }
    }
    public class OrdVM
    {
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public ProductVM prd { get; set; }
    }
    public class ProductVM
    {
        public string ProductName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
    }
    public class OrdersController : ApiController
    {
        private NorthwindContext db = new NorthwindContext();

        // GET api/Orders
        public HttpResponseMessage GetHakunnah()
        {
            var results = db.Orders                            // orh       
                            .Include(o => o.Order_Details)     // ord       
                            .ToList()                          // CRITICAL
                            .Select(o => o);

            return (Request.CreateResponse(HttpStatusCode.OK, CreateFlat(results)));
        }
        private IEnumerable<FlatVM> CreateFlat(IEnumerable<Order> orh)
        {

            List<FlatVM> lflatvm = new List<FlatVM>();

            foreach (var item in orh)
            {

                foreach (var ord in item.Order_Details)
                {
                    var flatvm = new FlatVM();
                    flatvm.orderid = item.OrderID;
                    flatvm.shipcity = item.ShipCity;
                    flatvm.companynamea = item.Shipper.CompanyName;          //shp
                    flatvm.numoflineitems = item.Order_Details.Count();

                    flatvm.ProductID = ord.ProductID;
                    flatvm.Quantity = ord.Quantity;
                    flatvm.UnitPrice = ord.UnitPrice;
                    flatvm.Discount = ord.Discount;

                    flatvm.ProductName = ord.Product.ProductName;           // prd

                    lflatvm.Add(flatvm);
                }
            }
            return (lflatvm);
        }

        public ICollection<OrdVM> StuffOrd(ICollection<Order_Detail> ord)
        {
            ICollection<OrdVM> ordvm = new List<OrdVM>();
            foreach (var item in ord)
            {
                ordvm.Add(new OrdVM
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    prd = StuffPrd(item.Product)
                });
            }
            return (ordvm);
        }

        private ProductVM StuffPrd(Product prd)
        {
            ProductVM prdvm = new ProductVM
            {
                ProductName = prd.ProductName,
                UnitPrice = prd.UnitPrice
            };

            return (prdvm);
        }

        // GET api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT api/Orders/5
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderID)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.OrderID }, order);
        }

        // DELETE api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.OrderID == id) > 0;
        }
    }




}
