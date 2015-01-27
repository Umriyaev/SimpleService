using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace SimpleService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public void InsertData(Student student)
        {
            XDocument doc = XDocument.Load(HttpContext.Current.Server.MapPath("StudentDB.xml"));
            doc.Element("Students").Add(new XElement("student",
                new XAttribute("name", student.Name),
                new XAttribute("number", student.Number)));
            doc.Save(HttpContext.Current.Server.MapPath("StudentDB.xml"));
        }

        public List<Student> GetAllStudents()
        {
            List<Student> result = XDocument.Load(HttpContext.Current.Server.MapPath("StudentDB.xml")).Root.Elements("student").Select(
            x => new Student
            {
                Name = x.Attribute("name").ToString(),
                Number = x.Attribute("number").ToString()
            }
                ).ToList();
            return result;
        }
    }
}
