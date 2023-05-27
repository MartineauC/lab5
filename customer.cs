using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CustomerProductClasses
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Customer()
        {
        }

        public Customer(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} - {Email}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Customer other = (Customer)obj;
            return Email == other.Email;
        }

        public override int GetHashCode()
        {
            return Email.GetHashCode();
        }
    }

    public class CustomerList
    {
        private List<Customer> customers;

        public CustomerList()
        {
            customers = new List<Customer>();
        }

        public int Count
        {
            get { return customers.Count; }
        }

        public Customer this[int index]
        {
            get { return customers[index]; }
            set { customers[index] = value; }
        }

        public Customer GetCustomerByEmail(string email)
        {
            return customers.Find(c => c.Email == email);
        }

        public void Add(Customer customer)
        {
            customers.Add(customer);
        }

        public void Add(string firstName, string lastName, string email)
        {
            Customer customer = new Customer(firstName, lastName, email);
            customers.Add(customer);
        }

        public void ChangeCustomer(int index, Customer customer)
        {
            customers[index] = customer;
        }

        public void Remove(Customer customer)
        {
            customers.Remove(customer);
        }

        public static CustomerList operator +(CustomerList list, Customer customer)
        {
            list.Add(customer);
            return list;
        }

        public static CustomerList operator -(CustomerList list, Customer customer)
        {
            list.Remove(customer);
            return list;
        }

        public void SaveToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, customers);
            }
        }

        public void FillFromXml(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<Customer> loadedCustomers = (List<Customer>)serializer.Deserialize(reader);
                customers.Clear();
                customers.AddRange(loadedCustomers);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Customer customer in customers)
            {
                sb.AppendLine(customer.ToString());
            }
            return sb.ToString();
        }
    }
}