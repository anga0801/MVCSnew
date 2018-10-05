using CRUDSuppliers.Mapping;
using CRUDSuppliers.Models;
using CRUDSuppliers_DAL;
using CRUDSuppliers_DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDSuppliers
{
    class Program
    {

        private static string connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
        private static SupplierDAO dao = new SupplierDAO(connectionString);

        public static int GetId()
        {
            int supplierId = 0;
            bool validInput = true;
            Console.WriteLine("Please enter Supplier ID");
            while (validInput)
            {
                if (int.TryParse(Console.ReadLine(), out supplierId))
                {
                    validInput = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Entry, please try again");
                    Console.ResetColor();
                }
            }
            return supplierId;
        }

        public static SupplierPO UserInput(SupplierPO supplier)
        {
            Console.WriteLine("Enter Company Name: ");
            supplier.CompanyName = Console.ReadLine();
            Console.WriteLine("Enter Contact Name: ");
            supplier.ContactName = Console.ReadLine();
            Console.WriteLine("Enter Contact Title: ");
            supplier.ContactTitle = Console.ReadLine();
            Console.WriteLine("Enter Address: ");
            supplier.Address = Console.ReadLine();
            Console.WriteLine("Enter City: ");
            supplier.City = Console.ReadLine();
            Console.WriteLine("Enter Region: ");
            supplier.Region = Console.ReadLine();
            Console.WriteLine("Enter Postal Code: ");
            supplier.PostalCode = Console.ReadLine();
            Console.WriteLine("Enter Country: ");
            supplier.Country = Console.ReadLine();
            Console.WriteLine("Enter Phone: ");
            supplier.Phone = Console.ReadLine();
            Console.WriteLine("Enter Fax: ");
            supplier.Fax = Console.ReadLine();
            Console.WriteLine("Enter Home Page: ");
            supplier.HomePage = Console.ReadLine();

            return supplier;
        }

        public static void ViewAllSuppliers(SupplierPO allSupps)
        {
            Console.WriteLine("Supplier Id : {0}", allSupps.SupplierID);
            Console.WriteLine("Company Name : {0}", allSupps.CompanyName);
            Console.WriteLine("Contact Name : {0}", allSupps.ContactName);
            Console.WriteLine("Contact Title : {0}", allSupps.ContactTitle);
            Console.WriteLine("Address : {0}", allSupps.Address);
            Console.WriteLine("City : {0}", allSupps.City);
            Console.WriteLine("Region : {0}", allSupps.Region);
            Console.WriteLine("Postal Code : {0}", allSupps.PostalCode);
            Console.WriteLine("Country : {0}", allSupps.Country);
            Console.WriteLine("Phone : {0}", allSupps.Phone);
            Console.WriteLine("Fax : {0}", allSupps.Fax);
            Console.WriteLine("Home Page : {0}\n", allSupps.HomePage);
        }

        public static void UpdateInformation(SupplierPO updateSupplier)
        {
            bool changesMade = true;
            bool continueUpdate = true;
            int ID = GetId();
            ID = updateSupplier.SupplierID;
            dao.ObtainSupplierById(ID);
            SupplierDO suppliers = new SupplierDO();
            updateSupplier = Mapper.DoToPo(suppliers);
            updateSupplier.SupplierID = ID;

            while (continueUpdate)
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("What would you like to update");
                Console.WriteLine("\ta) Company Name");
                Console.WriteLine("\tb) Contact Name");
                Console.WriteLine("\tc) Contact Title");
                Console.WriteLine("\td) Address");
                Console.WriteLine("\te) City");
                Console.WriteLine("\tf) Region");
                Console.WriteLine("\tg) Postal Code");
                Console.WriteLine("\th) Country");
                Console.WriteLine("\ti) Phone");
                Console.WriteLine("\tj) Fax");
                Console.WriteLine("\tk) Home Page");
                Console.WriteLine("\tl) Exit");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A:
                        Console.Clear();
                        Console.WriteLine("Please Enter New Company Name");
                        updateSupplier.CompanyName = Console.ReadLine();
                        changesMade = true;
                        break;

                    case ConsoleKey.B:
                        Console.Clear();
                        Console.WriteLine("Please Enter New Contact Name");
                        updateSupplier.ContactName = Console.ReadLine();
                        changesMade = true;
                        break;

                    case ConsoleKey.C:
                        Console.Clear();
                        Console.WriteLine("Please Enter New Contact Title");
                        updateSupplier.ContactTitle = Console.ReadLine();
                        changesMade = true;
                        break;

                    case ConsoleKey.D:
                        Console.Clear();
                        Console.WriteLine("Please Enter New Address");
                        updateSupplier.Address = Console.ReadLine();
                        changesMade = true;

                        break;

                    case ConsoleKey.E:
                        Console.Clear();
                        Console.WriteLine("Please Enter New City");
                        updateSupplier.City = Console.ReadLine();
                        changesMade = true;
                        break;

                    case ConsoleKey.F:
                        Console.Clear();
                        Console.WriteLine("Please Enter New Region");
                        updateSupplier.Region = Console.ReadLine();
                        changesMade = true;
                        break;

                    case ConsoleKey.G:
                        Console.Clear();
                        Console.WriteLine("Please Enter New Postal Code");
                        updateSupplier.PostalCode = Console.ReadLine();
                        changesMade = true;
                        break;

                    case ConsoleKey.H:
                        Console.Clear();
                        Console.WriteLine("Please Enter New Country");
                        updateSupplier.Country = Console.ReadLine();
                        changesMade = true;
                        break;

                    case ConsoleKey.I:
                        Console.Clear();
                        Console.WriteLine("Please Enter New Phone Number");
                        updateSupplier.Phone = Console.ReadLine();
                        changesMade = true;
                        break;

                    case ConsoleKey.J:
                        Console.Clear();
                        Console.WriteLine("Please Enter New Fax Number");
                        updateSupplier.Fax = Console.ReadLine();
                        changesMade = true;
                        break;

                    case ConsoleKey.K:
                        Console.Clear();
                        Console.WriteLine("Please Enter New Home Page");
                        updateSupplier.HomePage = Console.ReadLine();
                        changesMade = true;
                        break;

                    case ConsoleKey.L:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Returning to the Main menu");
                        continueUpdate = false;
                        Thread.Sleep(1800);
                        break;
                }
                if (changesMade)
                {
                    SupplierDO supplier = Mapper.PoToDo(updateSupplier);
                    dao.UpdateSupplier(supplier);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Update Successful");
                    Thread.Sleep(500);
                }
            }
        }

        static void Main(string[] args)
        {
            bool continueProgram = true;

            while (continueProgram)
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("What would you like to do?\n");
                Console.WriteLine("\t" + "1) View all Suppliers");
                Console.WriteLine("\t" + "2) Add a new Supplier");
                Console.WriteLine("\t" + "3) Update Supplier Information");
                Console.WriteLine("\t" + "4) Delete a Supplier");
                Console.WriteLine("\t" + "5) Exit Application");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        List<SupplierDO> allSuppliers = dao.ObtainAllSuppliers();
                        List<SupplierPO> supplierInfo = Mapper.FromDoToPo(allSuppliers);
                        for (int i = 0; i < supplierInfo.Count; i++)
                        {
                            ViewAllSuppliers(supplierInfo[i]);
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        Console.Clear();
                        SupplierPO supplier = new SupplierPO();
                        UserInput(supplier);
                        SupplierDO newSupp = Mapper.PoToDo(supplier);
                        dao.CreateSupplier(newSupp);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Data Stored");
                        Thread.Sleep(1800);
                        break;

                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        Console.Clear();
                        SupplierPO udatedSupplier = new SupplierPO();
                        UpdateInformation(udatedSupplier);
                        break;

                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        Console.Clear();
                        int suppId = GetId();
                        dao.DeleteSupplier(suppId);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Entry Successfully deleted");
                        Thread.Sleep(1800);
                        break;

                    case ConsoleKey.NumPad5:
                    case ConsoleKey.D5:
                        continueProgram = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Shutting down program");
                        Thread.Sleep(1800);
                        break;
                }
            }
        }
    }
}
