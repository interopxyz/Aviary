using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Graphics.Effects
{
    
    public class hFilter
    {

        public StringBuilder Value = new StringBuilder();
        public StringBuilder Content = new StringBuilder();
        public string ID = null;

        public hFilter(string Name)
        {
            ID = Name;
            Content.Clear();
        }

        public void AddEffect(string Effect)
        {
            Content.Append(Effect + Environment.NewLine);

        }

        public string ApplyFilter()
        {
            return "filter=\"url(#f" + ID + ")\" /> ";
        }

        public void Build()
        {
            Value.Append("<filter id=\"f" + ID + "\"> " + Environment.NewLine);
            Value.Append(Content);
            Value.Append("</filter> " + Environment.NewLine);
        }
    }
}
