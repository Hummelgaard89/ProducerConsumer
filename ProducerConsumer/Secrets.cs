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


        //China is the Consumer. Whenever USA makes a new secret and enqueues it, this method will write it to console with peek, and then dequeue it to clear up the queue.
        //This is in a for loop that runs the amount of counts in the queue. When it has cleared the queue, the thread will sleep for a random amount of time.
        //Furthermore, if the queue is empty, it will say so, and then await new numbers in the queue.
        public void China()
        {
            do
            {
                try
                {
                    Monitor.Enter(StateSecrets);
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
                        Monitor.PulseAll(StateSecrets);
                    }

                }
                finally
                {
                    Monitor.Exit(StateSecrets);
                    Thread.Sleep(random.Next(1000, 5000));
                }
            } while (true);
        }

        //USA is the producer, if the queue is not empty, it will say so.
        //If the queue is empty, it will create 1-5 random numbers and enqueue it, when the numbers have been created, it will pulseall and release the thread.
        public void USA()
        {
            do
            {
                try
                {
                    Monitor.Enter(StateSecrets);
                    lock (StateSecrets)
                    {
                        while (StateSecrets.Count != 0)
                        {
                            Monitor.Wait(StateSecrets);
                            Console.WriteLine("USA has no more new exiting secret nubers at the moment.");
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
                    Monitor.Exit(StateSecrets);
                    Thread.Sleep(random.Next(1000, 5000));

                }
            } while (true);

        }
    }
    
}
