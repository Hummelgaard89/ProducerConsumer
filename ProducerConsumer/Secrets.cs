using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerConsumer
{
    class Secrets
    {
        Queue<int> StateSecrets = new Queue<int>();
        Random random = new Random();
        int secretnumber = 0;


        public void China()
        {
            do
            {
                try
                {
                    lock (StateSecrets)
                    {
                        if (StateSecrets.Count == 0)
                        {
                            Monitor.Wait(StateSecrets);
                            Console.WriteLine("China awaits new secrets from USA.");
                            Console.WriteLine();
                        }
                        else if (StateSecrets.Count != 0)
                        {
                            for (int i = 0; i < StateSecrets.Count; i++)
                            {
                                Console.WriteLine("China's spy just got a new secret number from USA: " + StateSecrets.Peek());
                                StateSecrets.Dequeue();
                                Console.WriteLine();
                            }
                        }
                        //while (StateSecrets.Count == 0)
                        //{
                        //    Monitor.Wait(StateSecrets);
                        //    Console.WriteLine("China awaits new state secrets from USA.");
                        //}
                        //for (int i = 0; i < StateSecrets.Count; i++)
                        //{
                        //    Console.WriteLine("China's spy just got a new state secret from USA." + StateSecrets.Peek());
                        //    StateSecrets.Dequeue();
                        //}

                        Monitor.PulseAll(StateSecrets);
                    }

                }
                finally
                {
                    Thread.Sleep(random.Next(1000, 5000));
                }
            } while (true);
        }

        public void USA()
        {
            do
            {
                try
                {
                    lock (StateSecrets)
                    {
                        while (StateSecrets.Count != 0)
                        {
                            Monitor.Wait(StateSecrets);
                            Console.WriteLine("USA has no new exiting secret nubers at the moment.");
                            Console.WriteLine();
                        }
                        for (int i = 0; i < random.Next(1, 5); i++)
                        {
                            secretnumber = random.Next(1, 100);
                            StateSecrets.Enqueue(secretnumber);
                            Console.WriteLine("USA just created a secret number: " + secretnumber);
                            Console.WriteLine();
                        }
                        Monitor.PulseAll(StateSecrets);
                    }

                }
                finally
                {
                    Thread.Sleep(random.Next(1000, 5000));

                }
            } while (true);

        }
    }
    
}
