using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }

        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Student))
            {
                return false;
            }
            var newStudnet = (Student)obj;
            return Name == newStudnet.Name && Jmbag == newStudnet.Jmbag && Gender == newStudnet.Gender;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Jmbag != null ? Jmbag.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Gender;
                return hashCode;
            }
        }

        public static bool operator ==(Student s1, Student s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator !=(Student s1, Student s2)
        {
            return !(s1 == s2);
        }

    }

}
