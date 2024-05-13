using System.Xml.Linq;

namespace DLRDemo
{
    public class Program
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
             string res =new Print().DynamicPrintMethod(cust);
            Console.WriteLine(res);
            Console.WriteLine("*****************************");
            var emp = new Empolyee
            {
                Fname = "Ahmed",
                Lname = "Ali",
                Salary = 18000
            };
            cust.Frindes.Add(
            new Empolyee { Fname = "Hosam", Lname = "Gamal", Salary = 20000 }
            );
            string res2 = new Print().DynamicPrintMethod(emp);
            Console.WriteLine(res2);

        }
    }
    public class Person
    {
      
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public readonly IList<Person> Frindes= new List<Person>();
    }
    public class Empolyee:Person
    {
        public decimal Salary { get; set; } 

    }
    public  class Customer : Person 
    {
        public decimal CreditLimit { get; set; }
    }
    public class Print
    {
        public String DynamicPrintMethod(Person obj) => PrintMethod((dynamic) obj);
        public String PrintMethod(Person p)
        {
            return  $"First name: {p.Fname} ,Last name: {p.Lname}";
        }
        public string PrintMethod(Customer p)
        {
            string s = PrintMethod((Person)p);
            string s2 = $" , CreditLimit : {p.CreditLimit}";
            return s +s2;
        }
        public string PrintMethod(Empolyee p)
        {
            string s = PrintMethod((Person)p);
            string s2 = $" , Salary : {p.Salary}";
            return s + s2;
        }

    }
}
