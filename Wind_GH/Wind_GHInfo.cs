using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Wind_GH
{
    public class Wind_GHInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Wind";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("9e737a8d-1a41-4939-ae24-3797d9079424");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
