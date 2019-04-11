using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class ClassRoom
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public Int64 Code { get; set; }
        public Int64 Number_Of_Students { get; set; }
        public Int64 Number_Of_Vip { get; set; }
        public Int64 Age_Average { get; set; }
        public string Most_Popular_City { get; set; }
        public Int64 Oldest_Vip { get; set; }
        public Int64 Youngest_Vip { get; set; }

        public static bool operator ==(ClassRoom class1, ClassRoom class2)
        {
            if (ReferenceEquals(class1, null) && ReferenceEquals(class2, null))
                return true;
            if (ReferenceEquals(class1, null) || ReferenceEquals(class2, null))
                return false;

            return (class1.Id == class2.Id);
        }
        public static bool operator !=(ClassRoom class1, ClassRoom class2)
        {
            return !(class1 == class2);
        }

        public override bool Equals(object ob)
        {
            if (ReferenceEquals(ob, null))
                return false;
            ClassRoom c = ob as ClassRoom;
            if (ReferenceEquals(c, null))
                return false;

            return this.Id == c.Id;
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(this.Id);
        }

        public override string ToString()
        {
            return $"Country Id is {Id}, Name is {Name}, Code is {Code}, Number Of Students is {Number_Of_Students}, Number  OfVip {Number_Of_Vip}, Age Average {Age_Average}, Most Popular City {Most_Popular_City}, Oldest Vip {Oldest_Vip}, Youngest Vip {Youngest_Vip}";
        }
    }
}
