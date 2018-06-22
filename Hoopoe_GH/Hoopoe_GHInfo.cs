using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Hoopoe_GH
{
    public class Hoopoe_GHInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "HoopoeGH";
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
                return new Guid("d59e93bc-4dcf-44b8-8977-f692eb768f4f");
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
