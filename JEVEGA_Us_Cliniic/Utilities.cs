using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JEVEGA_Us_Cliniic
{
    public class Utilities
    {
        public string developerName;

        public enum WeekDays { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }; 

        public string Func1()
        {
            return ("This is function 1 ...");
        }

        public void setWidth(int w)
        {
            width = w;
        }
        public void setHeight(int h)
        {
            height = h;
        }
        protected int width;
        protected int height;

        public int getArea()
        {
            return width * height;
        }

        public int Sunday()
        {
            return (int)WeekDays.Sunday;
        }
    }

    //public class Rectangle : Utilities
    //{
    //    public int getArea()
    //    {
    //        return (width * height);
    //    }
    //}

}