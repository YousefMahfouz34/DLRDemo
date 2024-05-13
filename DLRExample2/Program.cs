using DLRExample2;
using System.Xml.Linq;

namespace DLRExample2
{
    public  class Program
    {
        static void Main(string[] args)
        {
            var cust = new Customer
            {
                Fname = "Yousef",
                Lname = "Mahfouz",
                CreditLimit = 2500
            };
            cust.Frindes.Add(
            new Empolyee { Fname = "Atef", Lname = "Mohamed", Salary = 20000 }
            );
            var res= new ToXElementPersonVisitor().DynamicVisit( cust );
            Console.WriteLine( res );

        }
    }
    public class Person
    {

        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public readonly IList<Person> Frindes = new List<Person>();
    }
    public class Empolyee : Person
    {
        public decimal Salary { get; set; }

    }
    public class Customer : Person
    {
        public decimal CreditLimit { get; set; }
    }
    abstract class PersonVisitor<T>
    {
        public T DynamicVisit(Person p) { return Visit((dynamic)p); }
        protected abstract T Visit(Person p);
        protected virtual T Visit(Customer c) { return Visit((Person)c); }
        protected virtual T Visit(Empolyee e) { return Visit((Person)e); }
    }
}
 class ToXElementPersonVisitor : PersonVisitor<XElement>
 {
    protected override XElement Visit(Person p)
    {
        return new XElement("Person",
        new XAttribute("Type", p.GetType().Name),
        new XElement("FirstName", p.Fname),
        new XElement("LastName", p.Lname),
        p.Frindes.Select(f => DynamicVisit(f))
        );
    }
    protected override XElement Visit(Customer c)
    {
        XElement xe = base.Visit(c);
        xe.Add(new XElement("CreditLimit", c.CreditLimit));
        return xe;
    }
    protected override XElement Visit(Empolyee e)
    {
        XElement xe = base.Visit(e);
        xe.Add(new XElement("Salary", e.Salary));
        return xe;
    }
}
