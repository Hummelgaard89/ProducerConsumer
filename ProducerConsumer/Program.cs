using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Secrets secrets = new Secrets();
            
            Thread China = new Thread(secrets.China);
            China.Start();
            //China.Join();

            Thread USA = new Thread(secrets.USA);
            USA.Start();
            //USA.Join();
            Console.ReadLine();

        }

        
       
    }
   

}
