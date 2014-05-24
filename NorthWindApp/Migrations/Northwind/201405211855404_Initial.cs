namespace NorthWindApp.Migrations.Northwind
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alphabetical list of products",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 40),
                        Discontinued = c.Boolean(nullable: false),
                        CategoryName = c.String(nullable: false, maxLength: 15),
                        SupplierID = c.Int(),
                        CategoryID = c.Int(),
                        QuantityPerUnit = c.String(maxLength: 20),
                        UnitPrice = c.Decimal(precision: 18, scale: 2),
                        UnitsInStock = c.Short(),
                        UnitsOnOrder = c.Short(),
                        ReorderLevel = c.Short(),
                    })
                .PrimaryKey(t => new { t.ProductID, t.ProductName, t.Discontinued, t.CategoryName });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 15),
                        Description = c.String(),
                        Picture = c.Binary(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 40),
                        SupplierID = c.Int(),
                        CategoryID = c.Int(),
                        QuantityPerUnit = c.String(maxLength: 20),
                        UnitPrice = c.Decimal(precision: 18, scale: 2),
                        UnitsInStock = c.Short(),
                        UnitsOnOrder = c.Short(),
                        ReorderLevel = c.Short(),
                        Discontinued = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID)
                .Index(t => t.SupplierID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Order Details",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Short(nullable: false),
                        Discount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ProductID })
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CustomerID = c.String(maxLength: 5, fixedLength: true),
                        EmployeeID = c.Int(),
                        OrderDate = c.DateTime(),
                        RequiredDate = c.DateTime(),
                        ShippedDate = c.DateTime(),
                        ShipVia = c.Int(),
                        Freight = c.Decimal(precision: 18, scale: 2),
                        ShipName = c.String(maxLength: 40),
                        ShipAddress = c.String(maxLength: 60),
                        ShipCity = c.String(maxLength: 15),
                        ShipRegion = c.String(maxLength: 15),
                        ShipPostalCode = c.String(maxLength: 10),
                        ShipCountry = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.Shippers", t => t.ShipVia)
                .Index(t => t.CustomerID)
                .Index(t => t.EmployeeID)
                .Index(t => t.ShipVia);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 5, fixedLength: true),
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        ContactName = c.String(maxLength: 30),
                        ContactTitle = c.String(maxLength: 30),
                        Address = c.String(maxLength: 60),
                        City = c.String(maxLength: 15),
                        Region = c.String(maxLength: 15),
                        PostalCode = c.String(maxLength: 10),
                        Country = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 24),
                        Fax = c.String(maxLength: 24),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.CustomerDemographics",
                c => new
                    {
                        CustomerTypeID = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        CustomerDesc = c.String(),
                    })
                .PrimaryKey(t => t.CustomerTypeID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(nullable: false, maxLength: 10),
                        Title = c.String(maxLength: 30),
                        TitleOfCourtesy = c.String(maxLength: 25),
                        BirthDate = c.DateTime(),
                        HireDate = c.DateTime(),
                        Address = c.String(maxLength: 60),
                        City = c.String(maxLength: 15),
                        Region = c.String(maxLength: 15),
                        PostalCode = c.String(maxLength: 10),
                        Country = c.String(maxLength: 15),
                        HomePhone = c.String(maxLength: 24),
                        Extension = c.String(maxLength: 4),
                        Photo = c.Binary(),
                        Notes = c.String(),
                        ReportsTo = c.Int(),
                        PhotoPath = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Employees", t => t.ReportsTo)
                .Index(t => t.ReportsTo);
            
            CreateTable(
                "dbo.Territories",
                c => new
                    {
                        TerritoryID = c.String(nullable: false, maxLength: 20),
                        TerritoryDescription = c.String(nullable: false, maxLength: 50, fixedLength: true),
                        RegionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TerritoryID)
                .ForeignKey("dbo.Region", t => t.RegionID, cascadeDelete: true)
                .Index(t => t.RegionID);
            
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        RegionID = c.Int(nullable: false),
                        RegionDescription = c.String(nullable: false, maxLength: 50, fixedLength: true),
                    })
                .PrimaryKey(t => t.RegionID);
            
            CreateTable(
                "dbo.Shippers",
                c => new
                    {
                        ShipperID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        Phone = c.String(maxLength: 24),
                    })
                .PrimaryKey(t => t.ShipperID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        ContactName = c.String(maxLength: 30),
                        ContactTitle = c.String(maxLength: 30),
                        Address = c.String(maxLength: 60),
                        City = c.String(maxLength: 15),
                        Region = c.String(maxLength: 15),
                        PostalCode = c.String(maxLength: 10),
                        Country = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 24),
                        Fax = c.String(maxLength: 24),
                        HomePage = c.String(),
                    })
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "dbo.Category Sales for 1997",
                c => new
                    {
                        CategoryName = c.String(nullable: false, maxLength: 15),
                        CategorySales = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CategoryName);
            
            CreateTable(
                "dbo.Current Product List",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => new { t.ProductID, t.ProductName });
            
            CreateTable(
                "dbo.Customer and Suppliers by City",
                c => new
                    {
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        Relationship = c.String(nullable: false, maxLength: 9),
                        City = c.String(maxLength: 15),
                        ContactName = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => new { t.CompanyName, t.Relationship });
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        CustomerName = c.String(nullable: false, maxLength: 40),
                        Salesperson = c.String(nullable: false, maxLength: 31),
                        OrderID = c.Int(nullable: false),
                        ShipperName = c.String(nullable: false, maxLength: 40),
                        ProductID = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 40),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Short(nullable: false),
                        Discount = c.Single(nullable: false),
                        ShipName = c.String(maxLength: 40),
                        ShipAddress = c.String(maxLength: 60),
                        ShipCity = c.String(maxLength: 15),
                        ShipRegion = c.String(maxLength: 15),
                        ShipPostalCode = c.String(maxLength: 10),
                        ShipCountry = c.String(maxLength: 15),
                        CustomerID = c.String(maxLength: 5, fixedLength: true),
                        Address = c.String(maxLength: 60),
                        City = c.String(maxLength: 15),
                        Region = c.String(maxLength: 15),
                        PostalCode = c.String(maxLength: 10),
                        Country = c.String(maxLength: 15),
                        OrderDate = c.DateTime(),
                        RequiredDate = c.DateTime(),
                        ShippedDate = c.DateTime(),
                        ExtendedPrice = c.Decimal(precision: 18, scale: 2),
                        Freight = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.CustomerName, t.Salesperson, t.OrderID, t.ShipperName, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount });
            
            CreateTable(
                "dbo.Order Details Extended",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 40),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Short(nullable: false),
                        Discount = c.Single(nullable: false),
                        ExtendedPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount });
            
            CreateTable(
                "dbo.Order Subtotals",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        Subtotal = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.Orders Qry",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        CustomerID = c.String(maxLength: 5, fixedLength: true),
                        EmployeeID = c.Int(),
                        OrderDate = c.DateTime(),
                        RequiredDate = c.DateTime(),
                        ShippedDate = c.DateTime(),
                        ShipVia = c.Int(),
                        Freight = c.Decimal(precision: 18, scale: 2),
                        ShipName = c.String(maxLength: 40),
                        ShipAddress = c.String(maxLength: 60),
                        ShipCity = c.String(maxLength: 15),
                        ShipRegion = c.String(maxLength: 15),
                        ShipPostalCode = c.String(maxLength: 10),
                        ShipCountry = c.String(maxLength: 15),
                        Address = c.String(maxLength: 60),
                        City = c.String(maxLength: 15),
                        Region = c.String(maxLength: 15),
                        PostalCode = c.String(maxLength: 10),
                        Country = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => new { t.OrderID, t.CompanyName });
            
            CreateTable(
                "dbo.Product Sales for 1997",
                c => new
                    {
                        CategoryName = c.String(nullable: false, maxLength: 15),
                        ProductName = c.String(nullable: false, maxLength: 40),
                        ProductSales = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.CategoryName, t.ProductName });
            
            CreateTable(
                "dbo.Products Above Average Price",
                c => new
                    {
                        ProductName = c.String(nullable: false, maxLength: 40),
                        UnitPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductName);
            
            CreateTable(
                "dbo.Products by Category",
                c => new
                    {
                        CategoryName = c.String(nullable: false, maxLength: 15),
                        ProductName = c.String(nullable: false, maxLength: 40),
                        Discontinued = c.Boolean(nullable: false),
                        QuantityPerUnit = c.String(maxLength: 20),
                        UnitsInStock = c.Short(),
                    })
                .PrimaryKey(t => new { t.CategoryName, t.ProductName, t.Discontinued });
            
            CreateTable(
                "dbo.Sales by Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false),
                        CategoryName = c.String(nullable: false, maxLength: 15),
                        ProductName = c.String(nullable: false, maxLength: 40),
                        ProductSales = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.CategoryID, t.CategoryName, t.ProductName });
            
            CreateTable(
                "dbo.Sales Totals by Amount",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        SaleAmount = c.Decimal(precision: 18, scale: 2),
                        ShippedDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.OrderID, t.CompanyName });
            
            CreateTable(
                "dbo.Summary of Sales by Quarter",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ShippedDate = c.DateTime(),
                        Subtotal = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.Summary of Sales by Year",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ShippedDate = c.DateTime(),
                        Subtotal = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomerCustomerDemo",
                c => new
                    {
                        CustomerTypeID = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        CustomerID = c.String(nullable: false, maxLength: 5, fixedLength: true),
                    })
                .PrimaryKey(t => new { t.CustomerTypeID, t.CustomerID })
                .ForeignKey("dbo.CustomerDemographics", t => t.CustomerTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerTypeID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.EmployeeTerritories",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        TerritoryID = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.TerritoryID })
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Territories", t => t.TerritoryID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.TerritoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.Order Details", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Order Details", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ShipVia", "dbo.Shippers");
            DropForeignKey("dbo.Orders", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.EmployeeTerritories", "TerritoryID", "dbo.Territories");
            DropForeignKey("dbo.EmployeeTerritories", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Territories", "RegionID", "dbo.Region");
            DropForeignKey("dbo.Employees", "ReportsTo", "dbo.Employees");
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.CustomerCustomerDemo", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.CustomerCustomerDemo", "CustomerTypeID", "dbo.CustomerDemographics");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.EmployeeTerritories", new[] { "TerritoryID" });
            DropIndex("dbo.EmployeeTerritories", new[] { "EmployeeID" });
            DropIndex("dbo.CustomerCustomerDemo", new[] { "CustomerID" });
            DropIndex("dbo.CustomerCustomerDemo", new[] { "CustomerTypeID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.Territories", new[] { "RegionID" });
            DropIndex("dbo.Employees", new[] { "ReportsTo" });
            DropIndex("dbo.Orders", new[] { "ShipVia" });
            DropIndex("dbo.Orders", new[] { "EmployeeID" });
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropIndex("dbo.Order Details", new[] { "ProductID" });
            DropIndex("dbo.Order Details", new[] { "OrderID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "SupplierID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropTable("dbo.EmployeeTerritories");
            DropTable("dbo.CustomerCustomerDemo");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Summary of Sales by Year");
            DropTable("dbo.Summary of Sales by Quarter");
            DropTable("dbo.Sales Totals by Amount");
            DropTable("dbo.Sales by Category");
            DropTable("dbo.Products by Category");
            DropTable("dbo.Products Above Average Price");
            DropTable("dbo.Product Sales for 1997");
            DropTable("dbo.Orders Qry");
            DropTable("dbo.Order Subtotals");
            DropTable("dbo.Order Details Extended");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customer and Suppliers by City");
            DropTable("dbo.Current Product List");
            DropTable("dbo.Category Sales for 1997");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Shippers");
            DropTable("dbo.Region");
            DropTable("dbo.Territories");
            DropTable("dbo.Employees");
            DropTable("dbo.CustomerDemographics");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.Order Details");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Alphabetical list of products");
        }
    }
}
