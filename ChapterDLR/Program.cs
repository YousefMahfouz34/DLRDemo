using System.Dynamic;
using System.Xml.Linq;

namespace ChapterDLR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XElement x = XElement.Parse(@"<Label Text=""Hello"" Id=""5""/>");
            dynamic da = x.DynamicAttributes();
            Console.WriteLine(da.Id); 
            da.Text = "Foo";
            Console.WriteLine(x.ToString());

        }
    }

    #region Dynamic Objects
    static class XExtensions
    {
        public static dynamic DynamicAttributes(this XElement e)
        => new XWrapper(e);
        class XWrapper : DynamicObject
        {
            XElement _element;
            public XWrapper(XElement e) { _element = e; }
            public override bool TryGetMember(GetMemberBinder binder,
            out object result)
            {
                result = _element.Attribute(binder.Name).Value;
                return true;
            }
            public override bool TrySetMember(SetMemberBinder binder,
            object value)
            {
                _element.SetAttributeValue(binder.Name, value);
                return true;
            }
        }
    }

    #endregion

    public class Foo<T>
    {
        public T value;
        //This method won’t compile: you can’t invoke members of
        //  unbound generic types.
        //static void Write(object obj)
        //{
        //    if (obj is Foo<>) // Illegal
        //        Console.WriteLine((Foo<>)obj).Value); // Illegal
        //}

        //----------------------------------------------------------------------
        //Dynamic binding offers two means by which we can work
       // around this.
        static void Write(dynamic obj)
        {
            try { Console.WriteLine(obj.Value); }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { }
        }
        
    }
   

  

}
