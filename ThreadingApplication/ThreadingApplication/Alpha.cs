using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadingApplication
{
    
    class Alpha
    {
        public void Beta()
        {
            while (true)
            {
                Console.WriteLine("Alpha.Beta is running in its own thread.");
            }
        }
    }
}
