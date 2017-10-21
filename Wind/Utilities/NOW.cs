using System;

namespace Wind.Utilities
{
    public class NOW
    {
        public string Number;

        public NOW()
        {
            Number =
            DateTime.Now.Year.ToString() + "x" +
            DateTime.Now.Month.ToString() + "x" +
            DateTime.Now.Day.ToString() + "x" +
            DateTime.Now.Hour.ToString() + "x" +
            DateTime.Now.Minute.ToString() + "x" +
            DateTime.Now.Second.ToString() + "x" +
            DateTime.Now.Millisecond.ToString();
        }
    }
}
