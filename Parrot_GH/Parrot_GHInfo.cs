using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Parrot_GH
{
    public class Parrot_GHInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "ParrotGH";
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
                return new Guid("ea73c3b0-b625-46b1-89e5-6f18bb9616cd");
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
