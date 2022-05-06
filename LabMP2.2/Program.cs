using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
namespace LabMP22
{
    class Program
    {
        static double a=1,b=-5,c=6;
        static double _b, b2, ac4, d, dsqrt, a2, x1, x2;
        static EventWaitHandle wh1_b = new AutoResetEvent(false);
        static EventWaitHandle wh2_b = new AutoResetEvent(false);
        static EventWaitHandle whb2 = new AutoResetEvent(false);
        static EventWaitHandle whd = new AutoResetEvent(false);
        static EventWaitHandle wh1_dsqrt = new AutoResetEvent(false);
        static EventWaitHandle wh2_dsqrt = new AutoResetEvent(false);
        static EventWaitHandle wh1_a2 = new AutoResetEvent(false);
        static EventWaitHandle wh2_a2 = new AutoResetEvent(false);
        static EventWaitHandle whac4 = new AutoResetEvent(false);
        static EventWaitHandle whx1 = new AutoResetEvent(false);
        static EventWaitHandle whx2 = new AutoResetEvent(false);

        static void Main()
        {
        Thread t1 = new Thread(MinusB);
        Thread t2 = new Thread(B2);
        Thread t3 = new Thread(AC4);
        Thread t4 = new Thread(D);
        Thread t5 = new Thread(Dsqrt);
        Thread t6 = new Thread(A2);
        Thread t7 = new Thread(X1);
        Thread t8 = new Thread(X2);
        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();
        t5.Start();
        t6.Start();
        t7.Start();
        t8.Start();
        whx2.WaitOne();
        whx1.WaitOne();
        Console.WriteLine("x1={0},x2={1}",x1,x2);
        }
        static void B2()
        {
            b2 =  b*b;
            whb2.Set();
            Console.WriteLine("b2 done");
        }
        static void AC4()
        {
            ac4 = a * c * 4;
            whac4.Set();
            Console.WriteLine("ac4 done");
        }
        static void MinusB()
        {
            _b = -b;
            wh1_b.Set();
            wh2_b.Set();
            Console.WriteLine("_b done");

        }
        static void D()
        {
            whac4.WaitOne();
            whb2.WaitOne();
            d = b2-ac4;
            whd.Set();
            Console.WriteLine("d done");
        }
        static void Dsqrt()
        {
            whd.WaitOne();
            dsqrt=Math.Sqrt(d);
            wh1_dsqrt.Set();
            wh2_dsqrt.Set();
            Console.WriteLine("dsqrt done");
        }
        static void A2()
        {
            a2 =2 * a;
            wh1_a2.Set();
            wh2_a2.Set();
            Console.WriteLine("a2 done");
        }
        static void X1()
        {
            wh1_a2.WaitOne(); 
            wh1_b.WaitOne();
            wh1_dsqrt.WaitOne();
            x1 = (_b + dsqrt)/a2;
            Console.WriteLine("x1 done");
            whx1.Set();    
        }
        static void X2()
        {
            wh2_a2.WaitOne();
            wh2_b.WaitOne();
            wh2_dsqrt.WaitOne();
            x2 = (_b - dsqrt) / a2;
            Console.WriteLine("x2 done");
            whx2.Set();    
        }
    }
}
