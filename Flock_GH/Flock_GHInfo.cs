using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Flock_GH
{
    public class Flock_GHInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "FlockGH";
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
                return new Guid("dbd2a9d3-ce73-4cdc-b497-959b79f4254b");
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
