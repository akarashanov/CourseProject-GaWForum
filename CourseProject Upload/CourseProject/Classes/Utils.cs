
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Classes
{
    public class Utils
    {
        public static string CutText (string text, int maxLenght)
        {
            if (text == null || text.Length<=maxLenght)
            {
                return text;
            }
            var shortText = text.Substring(0, maxLenght-3) + "...";
            return shortText;

        }


    }
}