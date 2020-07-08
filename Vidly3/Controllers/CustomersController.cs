using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly3.Models;
using System.Web.Mvc.Html;
using System.Data.Entity;
using Vidly3.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Vidly3.Controllers
{
    public class CustomersController : Controller
    {

        private readonly DataContext _context;

        public CustomersController()
        {
            _context = new DataContext();
        }
        // GET: Customers
        public ActionResult Index()
        {
            IEnumerable<Customer> customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult New()
        {
            IEnumerable<MembershipType> membershipTypes = _context.MembershipTypes.ToList();
            CustomerFormViewModel viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {

            if (!ModelState.IsValid)
            {
                CustomerFormViewModel viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                Customer customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                //TryUpdateModel(customer);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            CustomerFormViewModel viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
    }
}